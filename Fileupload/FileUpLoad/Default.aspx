<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <h1>Billedupload</h1>
    <asp:FileUpload ID="FileUpload_img" runat="server" />
    <br />
    <br />
    <asp:Label ID="Label_besked" runat="server"></asp:Label>
    <br />
    <br />
    <asp:Button ID="Button_upload" runat="server" onclick="Button_upload_Click" 
        Text="Gem med basis upload" />
&nbsp;
    <asp:Button ID="Button_Dynamisk_filenavn" runat="server" 
        onclick="Button_Dynamisk_filenavn_Click" Text="Gem med dynamisk GUID filnavn" />
&nbsp;
    <asp:Button ID="Button1" runat="server" onclick="Button_upload_dato_Click" 
        Text="Gem med dynamisk dato filnavn" />
    </form>
</body>
</html>
