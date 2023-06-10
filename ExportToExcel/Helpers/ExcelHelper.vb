Imports System.Reflection
Imports Microsoft.Office.Interop
Imports System.Windows.Media.Imaging
Imports System.Security
Imports System.Drawing
Imports Microsoft.Office.Core
Imports Microsoft.Office.Interop.Excel
Imports System.IO
Imports OfficeOpenXml
Imports OfficeOpenXml.Drawing
Imports System.Drawing.Imaging
Imports System.Net
Imports System.Threading

Namespace Helpers
    Public Class ExcelHelper(Of T)

        Private _baseDirectory As String
        Private _filePath As String
        Private _list As List(Of T)
        Private _imgHelper As New ImageHelper()

        Private _utils As New MyUtils()
        Public Property Utils As MyUtils
            Get
                Return _utils
            End Get
            Set(value As MyUtils)
                _utils = value
            End Set
        End Property

        Public Property FilePath As String
            Get
                Return _filePath
            End Get
            Set(value As String)
                _filePath = value
            End Set
        End Property

        Public Property List As List(Of T)
            Get
                Return _list
            End Get
            Set(value As List(Of T))
                _list = value
            End Set
        End Property

        Public Property BaseDirectory As String
            Get
                Return AppDomain.CurrentDomain.BaseDirectory
            End Get
            Set(value As String)
                _baseDirectory = value
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

        Public Sub New(list As List(Of T))
            Me.List = list
        End Sub

        Public Sub ExportListToExcel()
            Dim excelApp As New Application()
            Dim workbook As Workbook = excelApp.Workbooks.Add()
            Dim worksheet As Worksheet = workbook.ActiveSheet

            ' Get the type of the list items
            Dim itemType As Type = GetType(T)

            ' Get the list properties using reflection
            Dim properties As PropertyInfo() = itemType.GetProperties()

            ' Write the header row
            Dim columnIndex As Integer = 1
            For Each prop As PropertyInfo In properties
                worksheet.Cells(1, columnIndex) = prop.Name
                columnIndex += 1
            Next

            ' Write the data rows
            Dim rowIndex As Integer = 2
            For Each item As T In List
                columnIndex = 1
                For Each prop As PropertyInfo In properties
                    Dim value As Object = prop.GetValue(item)

                    ' If the property is a Bitmap, insert it as a picture
                    If prop.PropertyType Is GetType(Byte()) AndAlso value IsNot Nothing Then


                        'Dim url = ImgHelper.BitmapToUrl(value)

                        'Dim webClient As New WebClient()
                        'Dim imageBytes As Byte() = webClient.DownloadData(url)

                        Dim tempImagePath As String = Path.GetTempFileName() & ".jpg"
                        File.WriteAllBytes(tempImagePath, value)

                        Dim pictureRange As Range = worksheet.Cells(rowIndex, columnIndex)
                        pictureRange.ColumnWidth = 15
                        pictureRange.RowHeight = 90
                        Dim picture As Picture = worksheet.Pictures().Insert(tempImagePath)
                        With picture
                            .ShapeRange.LockAspectRatio = MsoTriState.msoFalse
                            .Left = pictureRange.Left
                            .Top = pictureRange.Top
                            .Width = pictureRange.Width
                            .Height = pictureRange.Height
                        End With
                    Else
                        worksheet.Cells(rowIndex, columnIndex) = value
                    End If

                    columnIndex += 1
                Next

                rowIndex += 1
            Next

            Dim uTime As Long = Utils.GenerateUnixTime()
            FilePath = $"C:\ImageExcel\{uTime}_data.xlsx"
            ' Save the workbook and close Excel
            workbook.SaveAs(FilePath)
            workbook.Close()
            excelApp.Quit()
            Process.Start(FilePath)
        End Sub
    End Class
End Namespace