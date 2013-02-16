<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Soeg_Mobler.aspx.cs" Inherits="Soeg_Mobler" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 500px;
        }
        .style2
        {
            height: 16px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table class="style1">
        <tr>
            <td>
                <asp:Label ID="Label_varenummer" runat="server" Text="Vare nr."></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox_varenummer" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="Button_varenummer" runat="server" Text="Søg" 
                    onclick="Button_varenummer_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label_moebelserie" runat="server" Text="Møbelserie"></asp:Label>
            </td>
            <td>
               
                <asp:CheckBoxList ID="CheckBoxList_kategori" runat="server" 
                    DataSourceID="SqlDataSource2" DataTextField="K_Navn" DataValueField="K_id" 
                    RepeatColumns="2" RepeatLayout="Flow">
                    
                </asp:CheckBoxList>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                    SelectCommand="SELECT [K_id], [K_Navn] FROM [Kategori]"></asp:SqlDataSource>
            </td>
            <td>
                &nbsp;
            </td>


        </tr>
        <tr>
            <td>
                <asp:Label ID="Label_designer" runat="server" Text="Designer"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="DropDownList_designer" runat="server" DataSourceID="SqlDataSource1"
                    DataTextField="D_Navn" DataValueField="D_id"  AppendDataBoundItems="true">
                <asp:ListItem Value="0">-- Alle --</asp:ListItem></asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                    SelectCommand="SELECT * FROM [Design]"></asp:SqlDataSource>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Label ID="Label_design_aar" runat="server" Text="Design år"></asp:Label>
            </td>
            <td class="style2">
                Min
                <asp:TextBox ID="TextBox_designer_min" runat="server" MaxLength="4" 
                    Width="35px"></asp:TextBox>
                &nbsp; max
                <asp:TextBox ID="TextBox_designer_max" runat="server" MaxLength="4" 
                    Width="35px"></asp:TextBox>
            </td>
            <td class="style2">
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Label ID="Label_pris" runat="server" Text="Pris"></asp:Label>
            </td>
            <td class="style2">
                Min
                <asp:TextBox ID="TextBox_pris_min" runat="server" MaxLength="100" Width="45px"></asp:TextBox>
                &nbsp; max
                <asp:TextBox ID="TextBox_pris_max" runat="server" MaxLength="100" Width="45px"></asp:TextBox>
            </td>
            <td class="style2">
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:Button ID="Button_main_soeg" runat="server" Text="Søg" OnClick="Button_main_soeg_Click" />
            </td>
        </tr>
    </table>
    <section>
        <article>
            <asp:Repeater ID="Repeater_mobler_soeg_resultat" runat="server">
            <HeaderTemplate>
            <h1>Møbler</h1>
            </HeaderTemplate>
            <ItemTemplate>
            <%#Eval("M_Navn") %> - fk_kat: <%#Eval("FK_Kategori") %> - FK_Designer: <%#Eval("FK_Design") %><hr />
            </ItemTemplate>
            </asp:Repeater>
        <br />
        </article>
    <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        SelectCommand="SELECT Mobler.M_Navn, Design.D_Navn, Moble_Billeder.B_Navn, Kategori.K_Navn FROM Mobler INNER JOIN Design ON Mobler.FK_Design = Design.D_id INNER JOIN Moble_Billeder ON Mobler.M_id = Moble_Billeder.FK_Mobler INNER JOIN Kategori ON Mobler.FK_Kategori = Kategori.K_id WHERE (Mobler.FK_Kategori = @kategori) OR (Mobler.FK_Kategori = @kategori1) AND (Mobler.FK_Design = @designer) AND (Mobler.M_Design_aar BETWEEN @design_arr_min AND @design_arr_max) AND (Mobler.M_Pris BETWEEN @Design_pris_min AND @Design_pris_max)">
        <SelectParameters>
            <asp:Parameter Name="kategori" />
            <asp:Parameter Name="kategori1" />
            <asp:Parameter Name="designer" />
            <asp:Parameter Name="design_arr_min" />
            <asp:Parameter Name="design_arr_max" />
            <asp:Parameter Name="Design_pris_min" />
            <asp:Parameter Name="Design_pris_max" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />
    </section>
    <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        
        SelectCommand="SELECT Mobler.M_Navn, Kategori.K_Navn, Mobler.FK_Kategori FROM Mobler INNER JOIN Kategori ON Mobler.FK_Kategori = Kategori.K_id WHERE (Mobler.FK_Kategori = @kategori) AND (Mobler.FK_Design = @designer) AND (Mobler.M_Design_aar BETWEEN @design_arr_min AND @design_arr_max) AND (Mobler.M_Pris BETWEEN @Design_pris_min AND @Design_pris_max) OR (Mobler.FK_Kategori = @kategori1) AND (Mobler.FK_Design = @designer) AND (Mobler.M_Design_aar BETWEEN @design_arr_min AND @design_arr_max) AND (Mobler.M_Pris BETWEEN @Design_pris_min AND @Design_pris_max)">
        <SelectParameters>
            <asp:Parameter Name="kategori" />
            <asp:Parameter Name="designer" />
            <asp:Parameter Name="design_arr_min" />
            <asp:Parameter Name="design_arr_max" />
            <asp:Parameter Name="Design_pris_min" />
            <asp:Parameter Name="Design_pris_max" />
            <asp:Parameter Name="kategori1" />
        </SelectParameters>
    </asp:SqlDataSource>
    </asp:Content>
