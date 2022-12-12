<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Fahrgemeinschaft.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .button {
          background-color: #4CAF50; /* Green */
          border: none;
          color: white;
          padding: 15px 32px;
          text-align: center;
          text-decoration: none;
          display: inline-block;
          font-size: 16px;
          margin: 4px 2px;
          cursor: pointer;
          background-color: white; 
          color: black; 
          border: 2px solid #4CAF50;
        }

        .button:hover {
            background-color: #4CAF50;
        }

        h1{font-family:'Arial monospaced for SAP'}
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Image ID="ImgLogo" runat="server" Height="127px" ImageUrl="~/Properties/logo_voecklabruck-300x121.png" Width="302px" /> 
        <h1>Fahrgemeinschaft HTL Vöcklabruck</h1>
        <div>
            E-Mail:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
            <asp:TextBox ID="txtEmailLogin" runat="server"></asp:TextBox>
            <br />
            Passwort:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtPasswordLogin" runat="server" TextMode="Password"></asp:TextBox>
            <br />
            <br />
            <asp:Button CssClass="button" ID="btnLogin" runat="server" OnClick="btnLogin_Click" Text="Login" />
&nbsp;
            <br />
            <asp:Label ID="lblInfo_Message" runat="server"></asp:Label>
            <br />
            <br />
            <br />
            <br />
            E-Mail:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
            <asp:TextBox ID="txtEMailCreate" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEMailCreate" ErrorMessage="E-Mail is null" ForeColor="Red" EnableClientScript="False"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEMailCreate" ErrorMessage="Wrong format" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" EnableClientScript="False"></asp:RegularExpressionValidator>
            <br />
            Passwort:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtPasswordCreate" runat="server" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvPasswort" runat="server" ControlToValidate="txtPasswordCreate" ErrorMessage="Password is empty" ForeColor="Red" EnableClientScript="False"></asp:RequiredFieldValidator>
            <asp:CustomValidator ID="cv_Password" runat="server" EnableClientScript="False" ErrorMessage="Wrong Format" OnServerValidate="cv_Password_ServerValidate"></asp:CustomValidator>
            <br />
            Telefonnummer:&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtPhonenumberCreate" runat="server" TextMode="Phone"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvPhonenumber" runat="server" ControlToValidate="txtPhonenumberCreate" ErrorMessage="Please enter your phonenumber" ForeColor="Red" EnableClientScript="False"></asp:RequiredFieldValidator>
            <br />
            <asp:RadioButtonList ID="RadioButtonList1" runat="server" Width="78px">
                <asp:ListItem>Lehrer</asp:ListItem>
                <asp:ListItem>Schueler</asp:ListItem>
            </asp:RadioButtonList>
            <br />
&nbsp;<asp:Button CssClass="button" ID="btnCreateAccount" runat="server" OnClick="btnCreateAccount_Click" Text="Account erstellen" />
            <br />
            <br />
            <asp:Label ID="lblInfo" runat="server"></asp:Label>
        </div>
        <p>
            <asp:TextBox ID="txtVerifyCode" runat="server"></asp:TextBox>
        </p>
        <p>
            <asp:Button ID="btnVerify" runat="server" OnClick="btnVerify_Click" Text="Enter your Verify Code" />
        </p>
    </form>
</body>
</html>
