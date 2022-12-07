Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls

Namespace UnboundColumnForCommenting
	Partial Public Class _Default
		Inherits System.Web.UI.Page
		Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
			If (Not IsPostBack) AndAlso (Not IsCallback) Then
				' predefine one comment
				MyComments.SetComment(2, "It's me")
			End If
		End Sub

		' Fetch unbound data here
		Protected Sub ASPxGridView1_CustomUnboundColumnData(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewColumnDataEventArgs)
			If e.IsGetData AndAlso e.Column.FieldName = "Comment" Then
				Dim key As Object = e.GetListSourceFieldValue(e.ListSourceRowIndex, ASPxGridView1.KeyFieldName)
				e.Value = MyComments.GetComment(key)
			End If
		End Sub

		' Persist unbound data here
		Protected Sub ASPxGridView1_RowUpdating(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs)
			Dim key As Object = e.Keys(0)
			Dim newComment As String = CStr(e.NewValues("Comment"))
			MyComments.SetComment(key, newComment)
		End Sub

		Private pendingComment As String

		' Get unbound data from a new row
		Protected Sub ASPxGridView1_RowInserting(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInsertingEventArgs)
			pendingComment = CStr(e.NewValues("Comment"))
		End Sub

		' And save it
		Protected Sub AccessDataSource1_Inserted(ByVal sender As Object, ByVal e As SqlDataSourceStatusEventArgs)
			If pendingComment IsNot Nothing AndAlso e.AffectedRows = 1 Then
				Dim cmd As New System.Data.OleDb.OleDbCommand("SELECT @@IDENTITY")
				cmd.Connection = CType(e.Command.Connection, System.Data.OleDb.OleDbConnection)
				Dim newID As Object = cmd.ExecuteScalar()
				MyComments.SetComment(newID, pendingComment)
				pendingComment = Nothing
			End If
		End Sub
	End Class
End Namespace
