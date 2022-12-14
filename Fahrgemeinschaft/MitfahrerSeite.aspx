<%@ Page Title="" Language="C#" MasterPageFile="~/Fahrgemeinschaft.UI.Master" AutoEventWireup="true" CodeBehind="MitfahrerSeite.aspx.cs" Inherits="Fahrgemeinschaft.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Label ID="lblStartort" runat="server" Text="Startort eingeben:"></asp:Label>
    <br />
    <asp:TextBox ID="txtStartort" runat="server"></asp:TextBox>
    <br />
    <br />
    <asp:Label ID="lblZielort" runat="server" Text="Zielort eingeben:"></asp:Label>
    <br />
    <asp:TextBox ID="txtZielort" runat="server"></asp:TextBox>
    <br />
    <br />
    <asp:CheckBox ID="chkDarfSchueler" runat="server" Text="Schueler als Mitfahrer" />
    <br />
    <br />
    <br />
    <asp:TextBox ID="txtStartzeit" runat="server" TextMode="DateTimeLocal"></asp:TextBox>
    <br />
    <br />
</asp:Content>
