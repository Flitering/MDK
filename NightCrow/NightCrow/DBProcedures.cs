using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace NightCrow
{
	public class DBProcedures
	{
		private SqlCommand command
		   = new SqlCommand("", DBConnection.connection);

		private void commandConfig(string config)
		{
			command.CommandType =
				System.Data.CommandType.StoredProcedure;
			command.CommandText = "[dbo].[" + config + "]";
			command.Parameters.Clear();
		}

		public void spTypeOfContract_insert(string Name, string Description)
		{
			commandConfig("TypeOfContract_insert");
			command.Parameters.AddWithValue("@Name", Name);
			command.Parameters.AddWithValue("@Description", Description);
			DBConnection.connection.Open();
			command.ExecuteNonQuery();
			DBConnection.connection.Close();
		}
		public void spTypeOfContract_update(Int32 ID_TypeOfContract, string Name, string Description)
		{
			commandConfig("TypeOfContract_update");
			command.Parameters.AddWithValue("@ID_TypeOfContract", ID_TypeOfContract);
			command.Parameters.AddWithValue("@Name", Name);
			command.Parameters.AddWithValue("@Description", Description);
			DBConnection.connection.Open();
			command.ExecuteNonQuery();
			DBConnection.connection.Close();
		}

		public void spTypeOfContract_delete(Int32 ID_TypeOfContract)
		{
			commandConfig("TypeOfContract_delete");
			command.Parameters.AddWithValue("@ID_TypeOfContract", ID_TypeOfContract);
			DBConnection.connection.Open();
			command.ExecuteNonQuery();
			DBConnection.connection.Close();
		}

		public void spPosition_insert(string EmployeesPosition, string Salary)
		{
			commandConfig("Position_insert");
			command.Parameters.AddWithValue("@EmployeesPosition", EmployeesPosition);
			command.Parameters.AddWithValue("@Salary", Salary);
			DBConnection.connection.Open();
			command.ExecuteNonQuery();
			DBConnection.connection.Close();
		}
		public void spPosition_update(Int32 ID_Position, string EmployeesPosition, string Salary)
		{
			commandConfig("Position_update");
			command.Parameters.AddWithValue("@ID_Position", ID_Position);
			command.Parameters.AddWithValue("@EmployeesPosition", EmployeesPosition);
			command.Parameters.AddWithValue("@Salary", Salary);
			DBConnection.connection.Open();
			command.ExecuteNonQuery();
			DBConnection.connection.Close();
		}

		public void spPosition_delete(Int32 ID_Position)
		{
			commandConfig("Position_delete");
			command.Parameters.AddWithValue("@ID_Position", ID_Position);
			DBConnection.connection.Open();
			command.ExecuteNonQuery();
			DBConnection.connection.Close();
		}

		public void spCustomers_insert(string FNameC, string MNameC, string LNameC, string CustomerPosition, string CustomerPhone)
		{
			commandConfig("Customers_insert");
			command.Parameters.AddWithValue("@FNameC", FNameC);
			command.Parameters.AddWithValue("@MNameC", MNameC);
			command.Parameters.AddWithValue("@LNameC", LNameC);
			command.Parameters.AddWithValue("@CustomerPosition", CustomerPosition);
			command.Parameters.AddWithValue("@CustomerPhone", CustomerPhone);
			DBConnection.connection.Open();
			command.ExecuteNonQuery();
			DBConnection.connection.Close();
		}
		public void spCustomers_update(Int32 ID_Customer, string FNameC, string MNameC, string LNameC, string CustomerPosition, string CustomerPhone)
		{
			commandConfig("Customers_update");
			command.Parameters.AddWithValue("@ID_Customer", ID_Customer);
			command.Parameters.AddWithValue("@FNameC", FNameC);
			command.Parameters.AddWithValue("@MNameC", MNameC);
			command.Parameters.AddWithValue("@LNameC", LNameC);
			command.Parameters.AddWithValue("@CustomerPosition", CustomerPosition);
			command.Parameters.AddWithValue("@CustomerPhone", CustomerPhone);
			DBConnection.connection.Open();
			command.ExecuteNonQuery();
			DBConnection.connection.Close();
		}

		public void spCustomers_delete(Int32 ID_Customer)
		{
			commandConfig("Customers_delete");
			command.Parameters.AddWithValue("@ID_Customer", ID_Customer);
			DBConnection.connection.Open();
			command.ExecuteNonQuery();
			DBConnection.connection.Close();
		}

		public void spTypeOfService_insert(string Name, string Description)
		{
			commandConfig("TypeOfService_insert");
			command.Parameters.AddWithValue("@Name", Name);
			command.Parameters.AddWithValue("@Description", Description);
			DBConnection.connection.Open();
			command.ExecuteNonQuery();
			DBConnection.connection.Close();
		}
		public void spTypeOfService_update(Int32 ID_TypeOfService, string Name, string Description)
		{
			commandConfig("TypeOfService_update");
			command.Parameters.AddWithValue("@ID_TypeOfService", ID_TypeOfService);
			command.Parameters.AddWithValue("@Name", Name);
			command.Parameters.AddWithValue("@Description", Description);
			DBConnection.connection.Open();
			command.ExecuteNonQuery();
			DBConnection.connection.Close();
		}

		public void spTypeOfService_delete(Int32 ID_TypeOfService)
		{
			commandConfig("TypeOfService_delete");
			command.Parameters.AddWithValue("@ID_TypeOfService", ID_TypeOfService);
			DBConnection.connection.Open();
			command.ExecuteNonQuery();
			DBConnection.connection.Close();
		}

		public void spBase_insert(string BaseAddress, string ProvidesService)
		{
			commandConfig("Base_insert");
			command.Parameters.AddWithValue("@BaseAddress", BaseAddress);
			command.Parameters.AddWithValue("@ProvidesService", ProvidesService);
			DBConnection.connection.Open();
			command.ExecuteNonQuery();
			DBConnection.connection.Close();
		}
		public void spBase_update(Int32 ID_Base, string BaseAddress, string ProvidesService)
		{
			commandConfig("Base_update");
			command.Parameters.AddWithValue("@ID_Base", ID_Base);
			command.Parameters.AddWithValue("@BaseAddress", BaseAddress);
			command.Parameters.AddWithValue("@ProvidesService", ProvidesService);
			DBConnection.connection.Open();
			command.ExecuteNonQuery();
			DBConnection.connection.Close();
		}

		public void spBase_delete(Int32 ID_Base)
		{
			commandConfig("Base_delete");
			command.Parameters.AddWithValue("@ID_Base", ID_Base);
			DBConnection.connection.Open();
			command.ExecuteNonQuery();
			DBConnection.connection.Close();
		}

		public void spStaff_insert(string FNameS, string MNameS, string LNameS, string LoginS, string PasswordS, Int32 Position_ID)
		{
			commandConfig("Staff_insert");
			command.Parameters.AddWithValue("@FNameS", FNameS);
			command.Parameters.AddWithValue("@MNameS", MNameS);
			command.Parameters.AddWithValue("@LNameS", LNameS);
			command.Parameters.AddWithValue("@LoginS", LoginS);
			command.Parameters.AddWithValue("@PasswordS", PasswordS);
			command.Parameters.AddWithValue("@Position_ID", Position_ID);
			DBConnection.connection.Open();
			command.ExecuteNonQuery();
			DBConnection.connection.Close();
		}
		public void spStaff_update(Int32 ID_Staff, string FNameS, string MNameS, string LNameS, string LoginS, string PasswordS, Int32 Position_ID)
		{
			commandConfig("Staff_update");
			command.Parameters.AddWithValue("@ID_Staff", ID_Staff);
			command.Parameters.AddWithValue("@FNameS", FNameS);
			command.Parameters.AddWithValue("@MNameS", MNameS);
			command.Parameters.AddWithValue("@LNameS", LNameS);
			command.Parameters.AddWithValue("@LoginS", LoginS);
			command.Parameters.AddWithValue("@PasswordS", PasswordS);
			command.Parameters.AddWithValue("@Position_ID", Position_ID);
			DBConnection.connection.Open();
			command.ExecuteNonQuery();
			DBConnection.connection.Close();
		}

		public void spStaff_delete(Int32 ID_Staff)
		{
			commandConfig("Staff_delete");
			command.Parameters.AddWithValue("@ID_Staff", ID_Staff);
			DBConnection.connection.Open();
			command.ExecuteNonQuery();
			DBConnection.connection.Close();
		}

		public void spService_insert(string HiringContractEmployees, string Available, Int32 TypeOfService_ID, Int32 Base_ID)
		{
			commandConfig("Service_insert");
			command.Parameters.AddWithValue("@HiringContractEmployees", HiringContractEmployees);
			command.Parameters.AddWithValue("@Available", Available);
			command.Parameters.AddWithValue("@TypeOfService_ID", TypeOfService_ID);
			command.Parameters.AddWithValue("@Base_ID", Base_ID);
			DBConnection.connection.Open();
			command.ExecuteNonQuery();
			DBConnection.connection.Close();
		}
		public void spService_update(Int32 ID_Service, string HiringContractEmployees, string Available, Int32 TypeOfService_ID, Int32 Base_ID)
		{
			commandConfig("Service_update");
			command.Parameters.AddWithValue("@ID_Service", ID_Service);
			command.Parameters.AddWithValue("@HiringContractEmployees", HiringContractEmployees);
			command.Parameters.AddWithValue("@Available", Available);
			command.Parameters.AddWithValue("@TypeOfService_ID", TypeOfService_ID);
			command.Parameters.AddWithValue("@Base_ID", Base_ID);
			DBConnection.connection.Open();
			command.ExecuteNonQuery();
			DBConnection.connection.Close();
		}

		public void spService_delete(Int32 ID_Service)
		{
			commandConfig("Service_delete");
			command.Parameters.AddWithValue("@ID_Service", ID_Service);
			DBConnection.connection.Open();
			command.ExecuteNonQuery();
			DBConnection.connection.Close();
		}
		public void spContract_insert(string ContractDate, string ContractExpirationDate, string TransactionAmount, Int32 Staff_ID, Int32 Customer_ID, Int32 TypeOfContract_ID, Int32 Service_ID)
		{
			commandConfig("Contract_insert");
			command.Parameters.AddWithValue("@ContractDate", ContractDate);
			command.Parameters.AddWithValue("@ContractExpirationDate", ContractExpirationDate);
			command.Parameters.AddWithValue("@TransactionAmount", TransactionAmount);
			command.Parameters.AddWithValue("@Staff_ID", Staff_ID);
			command.Parameters.AddWithValue("@Customer_ID", Customer_ID);
			command.Parameters.AddWithValue("@TypeOfContract_ID", TypeOfContract_ID);
			command.Parameters.AddWithValue("@Service_ID", Service_ID);
			DBConnection.connection.Open();
			command.ExecuteNonQuery();
			DBConnection.connection.Close();
		}
		public void spContract_update(Int32 ID_Contract, string ContractDate, string ContractExpirationDate, string TransactionAmount, Int32 Staff_ID, Int32 Customer_ID, Int32 TypeOfContract_ID, Int32 Service_ID)
		{
			commandConfig("Contract_update");
			command.Parameters.AddWithValue("@ID_Contract", ID_Contract);
			command.Parameters.AddWithValue("@ContractDate", ContractDate);
			command.Parameters.AddWithValue("@ContractExpirationDate", ContractExpirationDate);
			command.Parameters.AddWithValue("@TransactionAmount", TransactionAmount);
			command.Parameters.AddWithValue("@Staff_ID", Staff_ID);
			command.Parameters.AddWithValue("@Customer_ID", Customer_ID);
			command.Parameters.AddWithValue("@TypeOfContract_ID", TypeOfContract_ID);
			command.Parameters.AddWithValue("@Service_ID", Service_ID);
			DBConnection.connection.Open();
			command.ExecuteNonQuery();
			DBConnection.connection.Close();
		}

		public void spContract_delete(Int32 ID_Contract)
		{
			commandConfig("Contract_delete");
			command.Parameters.AddWithValue("@ID_Contract", ID_Contract);
			DBConnection.connection.Open();
			command.ExecuteNonQuery();
			DBConnection.connection.Close();
		}

		public Int32 Authorization(string User_Login, string User_Password)
		{
			Int32 ID_record = 0;
			command.CommandType = System.Data.CommandType.Text;
			command.CommandText = "SELECT count(*) FROM [dbo].[Staff] " +
					"where [LoginS] = '" + User_Login + "' and [PasswordS] = '" +
					User_Password + "'";
			DBConnection.connection.Open();
			ID_record = Convert.ToInt32(command.ExecuteScalar().ToString());
			DBConnection.connection.Close();
			return (ID_record);
		}
	}
}