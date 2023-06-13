Namespace Models
    Public Class DetectOperation

        Private _id As Integer
        Private _baseImage As String
        Private _detectedImage As String
        Private _operationTime As Long

        Public Property Id As Integer
            Get
                Return _id
            End Get
            Set(value As Integer)
                _id = value
            End Set
        End Property

        Public Property BaseImage As String
            Get
                Return _baseImage
            End Get
            Set(value As String)
                _baseImage = value
            End Set
        End Property

        Public Property DetectedImage As String
            Get
                Return _detectedImage
            End Get
            Set(value As String)
                _detectedImage = value
            End Set
        End Property

        Public Property OperationTime As Long
            Get
                Return _operationTime
            End Get
            Set(value As Long)
                _operationTime = value
            End Set
        End Property
    End Class
End Namespace


