<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h1>Admin - Default.aspx</h1>
    <a href="Default.aspx">Default.aspx</a>
    <a href="OpretBruger.aspx">OpretBruger.aspx</a>
        <br />
        <br />
        <asp:Button ID="Button_logud" runat="server" Text="logud" 
            onclick="Button_logud_Click" />

    </div>
    </form>
</body>
</html>
