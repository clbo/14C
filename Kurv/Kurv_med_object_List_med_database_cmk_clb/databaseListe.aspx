<%@ Page Language="C#" AutoEventWireup="true" CodeFile="databaseListe.aspx.cs" Inherits="databaseListe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table class="style1">
        <tr>
            <td>
    <asp:Repeater ID="Repeater_mobler" runat="server" DataSourceID="SqlDataSource1" 
        onitemcommand="Repeater_mobler_ItemCommand">
    <ItemTemplate>
    <h3><%#Eval("M_Navn") %></h3>
    <p>Pris: <%#Eval("M_Pris") %>  -  
        <asp:Button ID="Button_I_Kurv" runat="server" Text="Læg i kurv" CommandArgument='<%#Eval("M_id") %>' /></p>
    </ItemTemplate>
    </asp:Repeater>
            </td>
            <td>
    <asp:GridView ID="GridView_kurv" runat="server">
    </asp:GridView>
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        SelectCommand="SELECT * FROM [Mobler]"></asp:SqlDataSource>
    </form>
</body>
</html>
