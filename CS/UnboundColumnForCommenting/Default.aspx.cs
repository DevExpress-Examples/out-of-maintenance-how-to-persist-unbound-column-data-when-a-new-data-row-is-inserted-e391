using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace UnboundColumnForCommenting {
    public partial class _Default : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if(!IsPostBack && !IsCallback) {
                // predefine one comment
                MyComments.SetComment(2, "It's me");
            }
        }

        // Fetch unbound data here
        protected void ASPxGridView1_CustomUnboundColumnData(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewColumnDataEventArgs e) {
            if(e.IsGetData && e.Column.FieldName == "Comment") {
                object key = e.GetListSourceFieldValue(e.ListSourceRowIndex, ASPxGridView1.KeyFieldName);
                e.Value = MyComments.GetComment(key);
            }
        }

        // Persist unbound data here
        protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e) {
            object key = e.Keys[0];
            string newComment = (string)e.NewValues["Comment"];
            MyComments.SetComment(key, newComment);
        }

        string pendingComment;

        // Get unbound data from a new row
        protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e) {
            pendingComment = (string)e.NewValues["Comment"];
        }

        // And save it
        protected void AccessDataSource1_Inserted(object sender, SqlDataSourceStatusEventArgs e) {
            if(pendingComment != null && e.AffectedRows == 1) {
                System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand("SELECT @@IDENTITY");
                cmd.Connection = (System.Data.OleDb.OleDbConnection)e.Command.Connection;
                object newID = cmd.ExecuteScalar();
                MyComments.SetComment(newID, pendingComment);
                pendingComment = null;
            }
        }
    }
}
