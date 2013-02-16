<%@ Page Language="C#" AutoEventWireup="true" CodeFile="dropdownliste.aspx.cs" Inherits="dropdownliste" %>

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
    <h1>
        Søg bocher</h1>
    <table class="style1">
        <tr>
            <td>
                <asp:DropDownList ID="DropDownList_farve" runat="server" DataSourceID="SqlDataSource1"
                    DataTextField="farve" DataValueField="farveID" OnDataBound="DropDownList_farve_DataBound">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                    SelectCommand="SELECT * FROM [farve]"></asp:SqlDataSource>
                <br />
                <asp:DropDownList ID="DropDownList_styrke" runat="server" DataSourceID="SqlDataSource2"
                    DataTextField="styrke" DataValueField="styrkeID" OnDataBound="DropDownList_styrke_DataBound">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                    SelectCommand="SELECT * FROM [styrke]"></asp:SqlDataSource>
                <br />
                <asp:DropDownList ID="DropDownList_surhed" runat="server" DataSourceID="SqlDataSource3"
                    DataTextField="surhed" DataValueField="surhedID" OnDataBound="DropDownList_surhed_DataBound">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                    SelectCommand="SELECT * FROM [surhed]"></asp:SqlDataSource>
                <br />
                <asp:DropDownList ID="DropDownList_type" runat="server" DataSourceID="SqlDataSource4"
                    DataTextField="type" DataValueField="typeID" OnDataBound="DropDownList_type_DataBound">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                    SelectCommand="SELECT * FROM [type]"></asp:SqlDataSource>
                <br />
                <asp:Button ID="Button_søg" runat="server" Text="Søg" 
                    onclick="Button_søg_Click" />
            </td>
            <td>
                <asp:Repeater ID="Repeater_resultat" runat="server">
                    <HeaderTemplate>
                        <table>
                            <tr>
                                <td>
                                    Navn
                                </td>
                                <td>
                                    Vægt
                                </td>
                                <td>
                                    Pris
                                </td>
                                <td>
                                    Farve
                                </td>
                                <td>
                                    Surhed
                                </td>
                                <td>
                                    Styrke
                                </td>
                                <td>
                                    Type
                                </td>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                       <tr>
                                <td>
                                    <%#Eval("Navn") %>
                                </td>
                                <td>
                                    <%#Eval("vaegt") %>
                                </td>
                                <td>
                                    <%#Eval("pris") %>
                                </td>
                                <td>
                                    <%#Eval("FK_faveID") %>
                                </td>
                                <td>
                                    <%#Eval("FK_surhedID") %>
                                </td>
                                <td>
                                    <%#Eval("FK_styrkeID") %>
                                </td>
                                <td>
                                    <%#Eval("FK_typeID") %>
                                </td>
                            </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </td>
        </tr>
    </table>
    <br />
    <br />
    </form>
</body>
</html>
