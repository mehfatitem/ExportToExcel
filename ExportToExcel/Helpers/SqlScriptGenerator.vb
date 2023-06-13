Imports System.Data.Entity.Core.Common.CommandTrees

Namespace Helpers
    Public Class SqlScriptGenerator
        Public Function GenerateInsert(table As String, data As Dictionary(Of String, Object)) As String
            Dim columns As String = String.Join(", ", data.Keys)
            Dim values As String = String.Join(", ", data.Values.Select(Function(value) QuoteValue(value)))
            Return $"INSERT INTO {table} ({columns}) VALUES ({values})"
        End Function

        Public Function GenerateUpdate(table As String, data As Dictionary(Of String, Object), condition As String) As String
            Dim setClause As String = String.Join(", ", data.Select(Function(pair) $"{pair.Key} = {QuoteValue(pair.Value)}"))
            If condition Is Nothing Then
                Return $"UPDATE {table} SET {setClause}"
            End If
            Return $"UPDATE {table} SET {setClause} WHERE {condition}"
        End Function

        Public Function GenerateDelete(table As String, condition As String) As String
            If condition Is Nothing Then
                Return $"DELETE FROM {table}"
            End If
            Return $"DELETE FROM {table} WHERE {condition}"
        End Function

        Public Function GenerateSelect(table As String, columns As IEnumerable(Of String), Optional extraScript As String = "", Optional condition As String = Nothing) As String
            Dim selectClause As String = If(columns IsNot Nothing AndAlso columns.Any(), String.Join(", ", columns), "*")

            If condition Is Nothing Then
                Return $"SELECT {selectClause} FROM {table} {extraScript}"
            End If

            Return $"SELECT {selectClause} FROM {table} WHERE {condition} {extraScript}"
        End Function

        Public Function GenerateSelectWithJoin(mainTable As String, joinTable As String, joinCondition As String, columns As IEnumerable(Of String), joinType As String, Optional extraScript As String = "", Optional condition As String = Nothing) As String
            Dim selectClause As String = If(columns IsNot Nothing AndAlso columns.Any(), String.Join(", ", columns), "*")
            Dim joinStatement As String = $"{joinType} JOIN {joinTable} ON {joinCondition}"

            If condition Is Nothing Then
                Return $"SELECT {selectClause} FROM {mainTable} {joinStatement} {extraScript}"
            End If

            Return $"SELECT {selectClause} FROM {mainTable} {joinStatement} WHERE {condition} {extraScript}"
        End Function

        Public Function GenerateSelectWithJoins(mainTable As String, joinClauses As List(Of String), columns As IEnumerable(Of String), Optional extraScript As String = "", Optional condition As String = Nothing) As String
            Dim selectClause As String = If(columns IsNot Nothing AndAlso columns.Any(), String.Join(", ", columns), "*")
            Dim joinStatement As String = String.Join(" ", joinClauses)


            If condition Is Nothing Then
                Return $"SELECT {selectClause} FROM {mainTable} {joinStatement} {extraScript}"
            End If

            Return $"SELECT {selectClause} FROM {mainTable} {joinStatement} WHERE {condition} {extraScript}"
        End Function

        Private Function QuoteValue(value As Object) As String
            If TypeOf value Is String Then
                Return $"'{value.ToString().Replace("'", "''")}'"
            Else
                Return value.ToString()
            End If
        End Function
    End Class
End Namespace

