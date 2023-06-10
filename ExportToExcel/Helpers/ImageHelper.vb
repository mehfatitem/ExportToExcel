Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Windows.Media.Imaging

Namespace Helpers
    Public Class ImageHelper

        Public Sub New()

        End Sub

        Public Function BitmapToUrl(bitmap As Bitmap) As String
            ' Create a unique file name for the temporary image
            Dim fileName As String = Path.GetRandomFileName() & ".png"

            ' Save the bitmap to a temporary file
            Dim filePath As String = Path.Combine(Path.GetTempPath(), fileName)
            bitmap.Save(filePath, System.Drawing.Imaging.ImageFormat.Png)

            ' Get the URL of the temporary file
            Dim url As String = New Uri(filePath).AbsoluteUri

            Return url
        End Function

        Public Function Base64ToByte(base64String As String) As Byte()
            Return Convert.FromBase64String(base64String)
        End Function

        Public Function Base64ToBitmapImage(base64String As String) As BitmapImage
            Dim bytes As Byte() = Convert.FromBase64String(base64String)

            Dim bitmapImage As New BitmapImage()
            Using stream As New MemoryStream(bytes)
                stream.Position = 0
                bitmapImage.BeginInit()
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad
                bitmapImage.StreamSource = stream
                bitmapImage.EndInit()
            End Using

            Return bitmapImage
        End Function

        Public Function BitmapToStream(bitmap As Bitmap) As Stream
            Dim stream As New MemoryStream()
            bitmap.Save(stream, ImageFormat.Png)
            stream.Position = 0
            Return stream
        End Function

        Public Function Base64ToBitmap(base64String As String) As Bitmap
            Dim imageBytes As Byte() = Convert.FromBase64String(base64String)
            Using ms As New MemoryStream(imageBytes)
                Dim bitmap As New Bitmap(ms)
                Return bitmap
            End Using
        End Function

    End Class
End Namespace

