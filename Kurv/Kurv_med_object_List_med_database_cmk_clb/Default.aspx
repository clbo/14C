<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 150px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    
    <table class="style1">
        <tr>
            <td>
                ID:</td>
            <td>
                <asp:TextBox ID="TextBox_id" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Navn: 
            </td>
            <td>
                <asp:TextBox ID="TextBox_navn" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Pris:</td>
            <td>
                <asp:TextBox ID="TextBox_pris" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Antal</td>
            <td>
                <asp:TextBox ID="TextBox_antal" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td style="text-align: right">
                <asp:Button ID="Button_put_i_kurv" runat="server" Text="Put i kurv" 
                    onclick="Button_put_i_kurv_Click" />
            </td>
        </tr>
    </table>
    
    <asp:GridView ID="GridView_kurv" runat="server">
    </asp:GridView>
    
    </form>
</body>
</html>
