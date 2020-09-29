<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormMenu.aspx.cs" Inherits="NightCrow.FormMenu" %>

<!DOCTYPE html>
<link rel="stylesheet" href="NightCrow.css" />
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="Menu" runat="server">
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
                <asp:Label ID="lblMenu" runat="server" Font-Names="Arial" Font-Size="20" Font-Bold="true" Text="Меню"></asp:Label>
                <br />
                <br />
                <br />
                <asp:Button ID="btContract" runat="server" Font-Names="Arial" Font-Size="15" Text="Контракт"  Width="300" OnClick="btContract_Click" />
                <br />
                <br />
                <asp:Button ID="btStaff" runat="server" Font-Names="Arial" Font-Size="15" Text="Персонал" Width="300" OnClick="btStaff_Click" />
                <br />
                <br />
                <asp:Button ID="btCustomers" runat="server" Font-Names="Arial" Font-Size="15" Text="Клиент" Width="300" OnClick="btCustomers_Click" />
                <br />
                <br />
                <asp:Button ID="btPosition" runat="server" Font-Names="Arial" Font-Size="15" Text="Должность" Width="300" OnClick="btPosition_Click" />
                <br />
                <br />
                <asp:Button ID="btService" runat="server" Font-Names="Arial" Font-Size="15" Text="Услуга" Width="300" OnClick="btService_Click" />
                <br />
                <br />
                <asp:Button ID="btBase" runat="server" Font-Names="Arial" Font-Size="15" Text="База" Width="300" OnClick="btBase_Click" />
                <br />
                <br />
                <asp:Button ID="btTypeOfContract" runat="server" Font-Names="Arial" Font-Size="15" Text="Тип контракта" Width="300" OnClick="btTypeOfContract_Click" />
                <br />
                <br />
                <asp:Button ID="btTypeOfService" runat="server" Font-Names="Arial" Font-Size="15" Text="Тип услуги" Width="300" OnClick="btTypeOfService_Click" />        
            </center>
        </div>
    </form>
</body>
</html>
