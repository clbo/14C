<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SletBilleder.aspx.cs" Inherits="SletBilleder" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="js/jquery-ui-1.8.18.custom.min.js" type="text/javascript"></script>
    <script src="js/jquery.smooth-scroll.min.js" type="text/javascript"></script>
    <script src="js/lightbox.js" type="text/javascript"></script>
    <link href="css/lightbox.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>
            Slet billeder i databasen og i imagemappen</h1>
        <p>
            <asp:Label ID="Label_slet_msg" runat="server" Text="" BackColor="Yellow" BorderColor="#CC9900"
                BorderStyle="Solid" BorderWidth="1px"></asp:Label>
        </p>
        <p>Klik på billederne for at åbne "Resized_Original" (768px bredde) i LightBox</p>
        <p>
            <asp:Repeater ID="Repeater_images" runat="server" 
            DataSourceID="SqlDataSource1" onitemcommand="Repeater_images_ItemCommand">
                <ItemTemplate>
                    <div style="float:left">
                    <a href='Images/Upload/Resized_Original/<%#Eval("ImageFileName") %>' rel="lightbox[galleri]"><img src='Images/Upload/Thumbs/<%#Eval("ImageFileName") %>' /></a>
                        
                        <br />  <asp:LinkButton ID="LinkButton_slet" runat="server" CommandArgument='<%# Eval("ImageFileName") %>'
                            OnClientClick='confirm("Er du sikker på at du vil slette billedet?");' CommandName="Slet">Slet</asp:LinkButton>
                        </div>
                </ItemTemplate>
            </asp:Repeater>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnStr %>"
                SelectCommand="SELECT * FROM [Media]"></asp:SqlDataSource>
        </p>
    </div>
    </form>
</body>
</html>
