Imports System.Net.Http
Imports ExportToExcel.Models
Imports Newtonsoft.Json

Namespace Helpers
    Public Class ProgramHelper

        Private _db As New Mysql("localhost", "facedetect", "root", "")
        Private _imgHelper As New ImageHelper()
        Private _utils As New MyUtils()
        Private _list As New List(Of FaceDetect)
        Private _sqlScriptGenerator As New SqlScriptGenerator()
        Private _httpClient = New HttpClient()

        Private _faceDetectServiceUrl As String = "http://localhost:5000/api/operations"

        Public Sub New()
            RunProcess()
        End Sub

        Public Property Db As Mysql
            Get
                Return _db
            End Get
            Set(value As Mysql)
                _db = value
            End Set
        End Property

        Public Property ImgHelper As ImageHelper
            Get
                Return _imgHelper
            End Get
            Set(value As ImageHelper)
                _imgHelper = value
            End Set
        End Property

        Public Property Utils As MyUtils
            Get
                Return _utils
            End Get
            Set(value As MyUtils)
                _utils = value
            End Set
        End Property

        Public Property List As List(Of FaceDetect)
            Get
                Return _list
            End Get
            Set(value As List(Of FaceDetect))
                _list = value
            End Set
        End Property

        Public Property SqlScriptGenerator As SqlScriptGenerator
            Get
                Return _sqlScriptGenerator
            End Get
            Set(value As SqlScriptGenerator)
                _sqlScriptGenerator = value
            End Set
        End Property

        Public Property HttpClient As Object
            Get
                Return _httpClient
            End Get
            Set(value As Object)
                _httpClient = value
            End Set
        End Property

        Public Property FaceDetectServiceUrl As String
            Get
                Return _faceDetectServiceUrl
            End Get
            Set(value As String)
                _faceDetectServiceUrl = value
            End Set
        End Property

        Public Async Function GetOperations() As Task(Of String)
            Dim response As HttpResponseMessage = Await HttpClient.GetAsync($"{FaceDetectServiceUrl}/getOperations")

            If response.IsSuccessStatusCode Then
                Dim content As String = Await response.Content.ReadAsStringAsync()
                Return content
            Else
                Dim errorMessage As String = Await response.Content.ReadAsStringAsync()
                Throw New Exception(errorMessage)
            End If
        End Function

        Public Sub RunProcess()

            Dim operations = GetOperations().Result


            Dim operationList = JsonConvert.DeserializeObject(Of List(Of Models.DetectOperation))(operations)


            operationList.ForEach(Sub(item)
                                      Dim tmpFaceDetect As New FaceDetect With {
                                          .BaseImage = ImgHelper.Base64ToByte(Utils.RemoveBase64Header(item.BaseImage)),
                                          .DetectedImage = ImgHelper.Base64ToByte(Utils.RemoveBase64Header(item.DetectedImage)),
                                          .Id = item.Id,
                                          .OperationTime = Utils.UnixTimeToDateTime(item.OperationTime)
                                      }
                                      List.Add(tmpFaceDetect)
                                  End Sub)
            Dim excel As New ExcelHelper(Of FaceDetect)()
            excel.ExportListToExcel(List)
            Console.WriteLine($"Export to Excel Successfully => {excel.FilePath}")

            'Using context As New facedetectEntities
            '    Dim operations = context.detectoperation.OrderByDescending(Function(p) p.operationTime).ToList()

            '    For Each operation In operations
            '        Dim tmpFaceDetect As New FaceDetect With {
            '            .BaseImage = ImgHelper.Base64ToByte(Utils.RemoveBase64Header(operation.baseImage)),
            '            .DetectedImage = ImgHelper.Base64ToByte(Utils.RemoveBase64Header(operation.detectedImage)),
            '            .Id = operation.id,
            '            .OperationTime = Utils.UnixTimeToDateTime(operation.operationTime)
            '        }
            '        List.Add(tmpFaceDetect)
            '    Next


            '    Dim excel As New ExcelHelper(Of FaceDetect)(List)
            '    excel.ExportListToExcel()
            '    Console.WriteLine($"Export to Excel Successfully => {excel.FilePath}")

            'End Using

        End Sub

    End Class
End Namespace

