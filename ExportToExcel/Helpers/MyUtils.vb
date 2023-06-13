Imports System.Windows.Input

Namespace Helpers
    Public Class MyUtils

        Public Sub New()

        End Sub

        Public Function RemoveBase64Header(base64String As String) As String
            ' Find the index of the comma character
            Dim commaIndex As Integer = base64String.IndexOf(",")

            ' Check if a comma was found and extract the Base64 portion accordingly
            If commaIndex >= 0 Then
                Return base64String.Substring(commaIndex + 1).Trim()
            Else
                Return base64String.Trim()
            End If
        End Function

        Public Function GenerateUnixTime() As Long
            Dim currentTime As DateTimeOffset = DateTimeOffset.Now
            Dim unixTime As Long = currentTime.ToUnixTimeSeconds()

            Return unixTime
        End Function

        Public Function UnixTimeToDateTime(unixTime As Long)
            Dim dateTimeOffset As DateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(unixTime)


            ' Define the GMT+3 offset (3 hours ahead of UTC)
            Dim gmtPlus3Offset As New TimeSpan(3, 0, 0)

            Dim convertedTime As DateTimeOffset = dateTimeOffset.ToOffset(gmtPlus3Offset)


            ' Apply 

            ' Get day, month, year, hour, minute, and second
            Dim day As String = convertedTime.Day.ToString().PadLeft(2, "0"c)
            Dim month As String = convertedTime.Month.ToString().PadLeft(2, "0"c)
            Dim year As String = convertedTime.Year
            Dim hour As String = convertedTime.Hour.ToString().PadLeft(2, "0"c)
            Dim minute As String = convertedTime.Minute.ToString().PadLeft(2, "0"c)
            Dim second As String = convertedTime.Second.ToString().PadLeft(2, "0"c)

            ' Display the components

            Return $"{day}.{month}.{year} {hour}:{minute}:{second}"
        End Function

    End Class
End Namespace

