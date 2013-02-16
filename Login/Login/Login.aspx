<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <h1>Login</h1>
    <p>Email: <asp:TextBox ID="TextBox_email" runat="server" ValidationGroup="Login"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ControlToValidate="TextBox_email" ErrorMessage="Skriv din Email" 
            ValidationGroup="Login"></asp:RequiredFieldValidator>
    </p>
    <p>Password: 
        <asp:TextBox ID="TextBox_pass"
        runat="server" ValidationGroup="Login" TextMode="Password"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
            ControlToValidate="TextBox_pass" ErrorMessage="Skriv dit password" 
            ValidationGroup="Login"></asp:RequiredFieldValidator>
    </p>
        <p><asp:Button ID="Button_login" runat="server" Text="Login (normal)" 
                onclick="Button_login_Click" ValidationGroup="Login" />&nbsp;
            <asp:Button ID="Button_Kryptering" runat="server" 
                onclick="Button_Kryptering_Click" Text="Login med kryptering" />
    </p>
    <p>
        <asp:Label ID="Label_error_msg" runat="server" BackColor="Yellow" 
            BorderColor="#990000" BorderStyle="Dotted" BorderWidth="1px" Font-Bold="True"></asp:Label>
    </p>
    </form>
</body>
</html>
