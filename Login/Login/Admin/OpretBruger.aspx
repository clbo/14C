<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OpretBruger.aspx.cs" Inherits="Admin_OpretBruger" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h1>Admin - OpretBruger.aspx</h1>
    <a href="Default.aspx">Default.aspx</a>
    <a href="OpretBruger.aspx">OpretBruger.aspx</a>
        <br />
        <br />
        <asp:Button ID="Button_logud" runat="server" Text="logud" 
            onclick="Button_logud_Click" />
        <br />
        <br />
        <h2>
            Opret bruger</h2>
        Navn:&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="TextBox_Brugernavn" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ControlToValidate="TextBox_Brugernavn" ErrorMessage="Indtast navn" 
            ValidationGroup="opretBruger"></asp:RequiredFieldValidator>
        <br />
        Email:&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="TextBox_Email" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
            ControlToValidate="TextBox_Email" ErrorMessage="Email skal udfyldes" 
            ValidationGroup="opretBruger"></asp:RequiredFieldValidator>
&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
            ControlToValidate="TextBox_Email" ErrorMessage="Email ikke gyldig" 
            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
            ValidationGroup="opretBruger"></asp:RegularExpressionValidator>
        <br />
        Pass:&nbsp;&nbsp;&nbsp;&nbsp; 
        <asp:TextBox ID="TextBox_Password" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
            ControlToValidate="TextBox_Password" ErrorMessage="Password må ikke være tomt" 
            ValidationGroup="opretBruger"></asp:RequiredFieldValidator>
        <br />
        <br />
        <asp:Button ID="Button_opret_normal" runat="server" 
            onclick="Button_opret_normal_Click" Text="Opret normal" 
            ValidationGroup="opretBruger" />
&nbsp;<asp:Button ID="krypteret_password" runat="server" 
            onclick="krypteret_password_Click" Text="Opret med krypteret password" 
            ValidationGroup="opretBruger" />
        <br />
        <br />
        <asp:Label ID="Label_bruger_msg" runat="server" BackColor="Yellow" 
            BorderColor="#CC9900" BorderStyle="Solid"></asp:Label>
    </div>
    </form>
</body>
</html>
