Imports System.Drawing
Imports System.Windows.Media.Imaging

Namespace Models
    Public Class FaceDetect

        Private _id As Integer
        Private _baseImage As Byte()
        Private _detectedImage As Byte()
        Private _operationTime As String

        Public Property Id As Integer
            Get
                Return _id
            End Get
            Set(value As Integer)
                _id = value
            End Set
        End Property

        Public Property BaseImage As Byte()
            Get
                Return _baseImage
            End Get
            Set(value As Byte())
                _baseImage = value
            End Set
        End Property

        Public Property DetectedImage As Byte()
            Get
                Return _detectedImage
            End Get
            Set(value As Byte())
                _detectedImage = value
            End Set
        End Property

        Public Property OperationTime As String
            Get
                Return _operationTime
            End Get
            Set(value As String)
                _operationTime = value
            End Set
        End Property
    End Class
End Namespace

