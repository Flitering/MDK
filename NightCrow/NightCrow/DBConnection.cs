using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;

namespace NightCrow
{
	public class DBConnection
	{
		public static SqlConnection connection = new SqlConnection("Data Source = LAPTOP-FTS17DRH\\MYSERVER; " +
				" Initial Catalog = PrivateMilitaryCompany; Persist Security Info = true;" +
				" User ID = SA; Password = \"paralon\"");

		public DataTable dtBase = new DataTable("Base");
		public DataTable dtContract = new DataTable("Contract");
		public DataTable dtCustomers = new DataTable("Customers");
		public DataTable dtPosition = new DataTable("Position");
		public DataTable dtService = new DataTable("Service");
		public DataTable dtStaff = new DataTable("Staff");
		public DataTable dtTypeOfService = new DataTable("TypeOfService");
		public DataTable dtTypeOfContract = new DataTable("TypeOfContract");
		public static Int32 ID_User = 0;

		public static string qrBase = "SELECT [ID_Base], [BaseAddress] as \'Адрес базы\', " +
			"[ProvidesServices] as \'Услуги предоставляет\' FROM [dbo].[Base]",
			qrContract = "SELECT [ID_Contract], [ContractDate] as \'Дата контракта\', " +
			"[ContractExpirationDate] as \'Дата истечения контракта\', [TransactionAmount] as \'Сумма перевода\' FROM [dbo].[Contract] INNER JOIN[dbo].[Staff] on[dbo].[Contract].[Staff_ID]=[dbo].[Staff].[ID_Staff]" +
			"INNER JOIN[dbo].[Customers] on[dbo].[Contract].[Customer_ID]=[dbo].[Customers].[ID_Customer]" + "INNER JOIN[dbo].[TypeOfContract] on[dbo].[Contract].[TypeOfContract_ID]=[dbo].[TypeOfContract].[ID_TypeOfContract]" +
			"INNER JOIN[dbo].[Service] on[dbo].[Contract].[Service_ID]=[dbo].[Service].[ID_Service]",
			qrCustomers = "SELECT [ID_Customer], [FNameC] as \"Имя клиента\", " +
			"[MNameC] as \"Фамилия клиента\", [LNameC] as \"Отчество клиента\", [CustomerPosition] as \"Должность клиента\", [CustomerPhone] as \"Телефон клиента\" FROM [dbo].[Customers]",
			qrPosition = "SELECT [ID_Position], [EmployeesPosition] as \"Должность подчинённого\", " +
			"[Salary] as \"Оклад\" FROM [dbo].[Position]",
			qrService = "SELECT [ID_Service], [HiringContractEmployees] as \"Найм служащих по контракту\", " +
			"[Available] as \"Доступно\" FROM [dbo].[Service] INNER JOIN[dbo].[TypeOfService] on[dbo].[Service].[TypeOfService_ID]=[dbo].[TypeOfService].[ID_TypeOfService]" +
			"INNER JOIN[dbo].[Base] on[dbo].[Service].[Base_ID]=[dbo].[Base].[ID_Base]",
			qrStaff = "SELECT [ID_Staff], [FNameS] as \"Имя сотрудника\", [MNameS] as \"Фамилия сотрудника\", [LNameS] as \"Отчество сотрудника\", [LoginS] as \"Логин сотрудника\", [PasswordS] as \"Пароль сотрудника\" FROM [dbo].[Staff] INNER JOIN[dbo].[Position] on[dbo].[Staff].[Position_ID]=[dbo].[Position].[ID_Position]",
			qrTypeOfService = "SELECT [ID_TypeOfService], [Name] as \"Название\", [Description] as \"Описание\" FROM [dbo].[TypeOfService]",
			qrTypeOfContract = "SELECT [ID_TypeOfContract], [Name] as \"Название\", [Description] as \"Описание\" FROM [dbo].[TypeOfContract]";

		private SqlCommand command = new SqlCommand("", connection);
		public static Int32 IDrecord, IDuser;

		public void dbEnter(string login, string password)
		{
			command.CommandText = "SELECT count(*) FROM [dbo].[Staff] " +
				"where [LoginS] = '" + login + "' and [PasswordS] = '" +
				password + "'";
			connection.Open();
			IDuser = Convert.ToInt32(command.ExecuteScalar().ToString());
			connection.Close();
		}

		private void dtFill(DataTable table, string query)
		{
			command.CommandText = query;
			connection.Open();
			table.Load(command.ExecuteReader());
			connection.Close();
		}

		public void StaffFill()
		{
			dtFill(dtStaff, qrStaff);
		}
		public void ContractFill()
		{
			dtFill(dtContract, qrContract);
		}
		public void ServiceFill()
		{
			dtFill(dtService, qrService);
		}
		public void CustomersFill()
		{
			dtFill(dtCustomers, qrCustomers);
		}
		public void TypeOfСontractFill()
		{
			dtFill(dtTypeOfContract, qrTypeOfContract);
		}
		public void BaseFill()
		{
			dtFill(dtBase, qrBase);
		}
		public void TypeOfServiceFill()
		{
			dtFill(dtTypeOfService, qrTypeOfService);
		}
		public void PositionFill()
		{
			dtFill(dtPosition, qrPosition);
		}
	} 
}