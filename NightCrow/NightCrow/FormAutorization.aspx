<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormAutorization.aspx.cs" Inherits="NightCrow.WebForm1" %>

<!DOCTYPE html>
<link rel="stylesheet" href="NightCrow.css" />
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
 <form id="Autorization" runat="server" style= "background-color:dimgray,gray">
		<div>
			<script type ="text/javascript"> 

				if ((navigator.userAgent.indexOf("Opera") || navigator.userAgent.indexOf('OPR')) != -1) {

					alert('Opera');

				}

				else if (navigator.userAgent.indexOf("Chrome") != -1) {

					alert('Chrome');

				}

				else if (navigator.userAgent.indexOf("Safari") != -1) {

					alert('Safari');

				}

				else if (navigator.userAgent.indexOf("Firefox") != -1) {

					alert('Firefox');

				}

				else if ((navigator.userAgent.indexOf("MSIE") != -1) || (!!document.documentMode == true)) //IF IE > 10 

				{

					alert('IE');

				}

				else {

					alert('unknown');

					 }
    </script> 
            <center>
                <asp:Label ID="lblPage" runat="server" Font-Size="20" Font-Names="Arial" Font-Bold="true" Text="Авторизация"></asp:Label>
                <br />
                <br />
                <br />
                <asp:Label ID="lblLoginStaff" runat="server" Font-Size="15" Font-Names="Arial" Text="Логин" Width="300"></asp:Label>
                <br />
                <asp:TextBox ID="tbLoginStaff" runat="server" Font-Names="Arial" Font-Size="15" Width="300" MaxLength="20"></asp:TextBox>
                <br />
                <br />
                <asp:Label ID="lblPasswordStaff" runat="server" Font-Size="15" Font-Names="Arial" Text="Пароль" Width="300"></asp:Label>
                <br />
                <asp:TextBox ID="tbPasswordStaff" runat="server" Font-Names="Arial" Font-Size="15" Width="300" MaxLength="20"></asp:TextBox>
                <br />
                <br />
                <br />
                <asp:Button ID="btOpen" runat="server" Font-Names="Arial" Font-Size="15" Width="150" Text="Вход" OnClick="btOpen_Click"/>
            </center>
        </div>
</form>
</body>
</html>
