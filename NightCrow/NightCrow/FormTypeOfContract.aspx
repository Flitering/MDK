<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormTypeOfContract.aspx.cs" Inherits="NightCrow.FormTypeOfContract" %>

<!DOCTYPE html>
<link rel="stylesheet" href="NightCrow.css" />
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="TypeOfContract" runat="server">
		<asp:SqlDataSource ID="sdsTypeOfContract" runat ="server"></asp:SqlDataSource>
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
                <asp:Label ID="lblPageStaff" runat="server" Font-Names="Arial" Font-Size="23" Font-Bold="true" Text="Тип контракта" Width="300"></asp:Label>
                <br />
                <br />
                <br />
                <br />
                <br />
                <asp:TextBox ID="tbSearch" runat="server" Font-Names="Arial" Font-Size="15" Text="Поиск..." Width="300"></asp:TextBox>
                <br />
                <asp:Button ID="btSearch" runat="server" Font-Names="Arial" Font-Size="12" Text="Поиск" Width="150" OnClick="btSearch_Click" />
                <asp:Button ID="btFilter" runat="server" Font-Names="Arial" Font-Size="12" Text="Фильтр" Width="150" OnClick="btFilter_Click" />
                <br />
                <asp:Button ID="btCancel" runat="server" Font-Names="Arial" Font-Size="12" Text="Отмена" Width="300" OnClick="btCancel_Click" />
                <br />
                <br />
                <br />
                <asp:GridView ID ="gvTypeOfContract" runat ="server" AllowSorting ="true" CurrentSortField ="" CurrentSortDirection ="ASC" OnRowDataBound="gvTypeOfContract_RowDataBound" OnSelectedIndexChanged="gvTypeOfContract_SelectedIndexChanged" OnSorting="gvTypeOfContract_Sorting" >
                    <Columns>
                        <asp:CommandField ShowSelectButton ="true" />
                    </Columns>
                </asp:GridView>
                <br />
                <br />
                <br />
                <br />
            </center>
            <asp:Label ID="lblName" runat="server" Font-Names="Arial" Font-Size="15" Text="Название: " Width="190"></asp:Label>
            <asp:TextBox ID="tbName" runat="server" Font-Names="Arial" Font-Size="12" Width="300" MaxLength="20"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="lblDescription" runat="server" Font-Names="Arial" Font-Size="15" Text="Описание: " Width="190"></asp:Label>
            <asp:TextBox ID="tbDescription" runat="server" Font-Names="Arial" Font-Size="12" Width="300" MaxLength="40"></asp:TextBox>
            <br />
            <br />
            <br />
            <br />
            <center>
                <asp:Button ID="btInsert" runat="server" Font-Names="Arial" Font-Size="15" Height="25" Width="300" Text="Добавит" OnClick="btInsert_Click" />
                <br />
                <asp:Button ID="btUpdate" runat="server" Font-Names="Arial" Font-Size="15" Height="25" Width="300"  Text="Изменит" OnClick="btUpdate_Click" />
                <br />
                <asp:Button ID="btDelete" runat="server" Font-Names="Arial" Font-Size="15" Height="25" Width="300" Text="Удалить" OnClick="btDelete_Click" />
                <br />
                <asp:Button ID="btBack" runat="server" Font-Names="Arial" Font-Size="15" Height="25" Width="300" Text="Назад" OnClick="btBack_Click"/>
            </center>
        </div>
    </form>
</body>
</html>
