<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="UnboundColumnForCommenting._Default" %>

<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.14.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>

<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.14.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dxwgv:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" KeyFieldName="ID" OnCustomUnboundColumnData="ASPxGridView1_CustomUnboundColumnData" OnRowUpdating="ASPxGridView1_RowUpdating" Width="307px" DataSourceID="AccessDataSource1" OnRowInserting="ASPxGridView1_RowInserting">
            <Columns>
                <dxwgv:GridViewDataTextColumn FieldName="Name" VisibleIndex="0">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="Comment" UnboundType="String" VisibleIndex="1">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewCommandColumn VisibleIndex="2">
                    <EditButton Visible="True">
                    </EditButton>
                    <CancelButton Visible="True">
                    </CancelButton>
                    <UpdateButton Visible="True">
                    </UpdateButton>
                    <ClearFilterButton Visible="True">
                    </ClearFilterButton>
                    <NewButton Visible="True">
                    </NewButton>
                </dxwgv:GridViewCommandColumn>
            </Columns>
        </dxwgv:ASPxGridView>
        <asp:AccessDataSource ID="AccessDataSource1" runat="server" DataFile="~/App_Data/data.mdb"
            DeleteCommand="DELETE FROM [Employee] WHERE [ID] = ?" InsertCommand="INSERT INTO [Employee] ([Name]) VALUES (?)"
            SelectCommand="SELECT * FROM [Employee]" UpdateCommand="UPDATE [Employee] SET [Name] = ? WHERE [ID] = ?" OnInserted="AccessDataSource1_Inserted">
            <DeleteParameters>
                <asp:Parameter Name="ID" Type="Int32" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="Name" Type="String" />
                <asp:Parameter Name="ID" Type="Int32" />
            </UpdateParameters>
            <InsertParameters>
                <asp:Parameter Name="Name" Type="String" />
            </InsertParameters>
        </asp:AccessDataSource>
    </div>
    </form>
</body>
</html>
