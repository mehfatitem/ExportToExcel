Imports ExportToExcel.Models

Namespace Helpers
    Public Class ProgramHelper

        Private _db As New Mysql("localhost", "facedetect", "root", "")
        Private _imgHelper As New ImageHelper()
        Private _utils As New MyUtils()
        Private _list As New List(Of FaceDetect)

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

        Public Sub RunProcess()
            Try
                Dim reader = Db.ExecuteReader("SELECT * from detectoperation order by operationtime desc")

                While reader.Read()
                    Try
                        Dim tmpFaceDetect As New FaceDetect With {
                            .BaseImage = ImgHelper.Base64ToByte(Utils.RemoveBase64Header(reader.GetString("baseImage"))),
                            .DetectedImage = ImgHelper.Base64ToByte(Utils.RemoveBase64Header(reader.GetString("detectedImage"))),
                            .Id = reader.GetInt32("id"),
                            .OperationTime = Utils.UnixTimeToDateTime(reader.GetInt64("operationtime"))
                        }
                        List.Add(tmpFaceDetect)
                    Catch ex As Exception
                    End Try
                End While



                Dim excel As New ExcelHelper(Of FaceDetect)(List)
                excel.ExportListToExcel()
                Console.WriteLine($"Export to Excel Successfully => {excel.FilePath}")
            Catch ex As Exception
                Console.WriteLine(ex.Message)
            End Try
        End Sub
    End Class
End Namespace

