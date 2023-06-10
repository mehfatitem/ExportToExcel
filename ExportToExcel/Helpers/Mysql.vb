Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient

Namespace Helpers
    Public Class Mysql

        Private _connectionString As String

        Private _server As String
        Private _database As String
        Private _username As String
        Private _password As String

        Public Property ConnectionString As String
            Get
                Return $"server={Server};database={Database};user={Username};password={Password};"
            End Get
            Set(value As String)
                _connectionString = value
            End Set
        End Property

        Public Property Server As String
            Get
                Return _server
            End Get
            Set(value As String)
                _server = value
            End Set
        End Property

        Public Property Database As String
            Get
                Return _database
            End Get
            Set(value As String)
                _database = value
            End Set
        End Property

        Public Property Username As String
            Get
                Return _username
            End Get
            Set(value As String)
                _username = value
            End Set
        End Property

        Public Property Password As String
            Get
                Return _password
            End Get
            Set(value As String)
                _password = value
            End Set
        End Property

        Public Sub New(server As String, database As String, username As String, password As String)
            Me.Username = username
            Me.Database = database
            Me.Server = server
            Me.Password = password
        End Sub


        Public Function ExecuteNonQuery(query As String) As Integer
            Using connection As New MySqlConnection(ConnectionString)
                connection.Open()
                Using command As New MySqlCommand(query, connection)
                    Return command.ExecuteNonQuery()
                End Using
            End Using
        End Function

        Public Function ExecuteScalar(query As String) As Object
            Using connection As New MySqlConnection(ConnectionString)
                connection.Open()
                Using command As New MySqlCommand(query, connection)
                    Return command.ExecuteScalar()
                End Using
            End Using
        End Function

        Public Function ExecuteReader(query As String) As MySqlDataReader
            Dim connection As New MySqlConnection(ConnectionString)
            connection.Open()
            Dim command As New MySqlCommand(query, connection)
            Return command.ExecuteReader(CommandBehavior.CloseConnection)
        End Function
    End Class
End Namespace









