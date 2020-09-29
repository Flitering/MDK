using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace NightCrow
{
	public partial class FormContract : System.Web.UI.Page
	{
		private string QR = "";
		protected void Page_Load(object sender, EventArgs e)
		{
			QR = DBConnection.qrContract;
			if (!IsPostBack)
			{
				gvFill(QR);
				lbFill1();
				lbFill2();
				lbFill3();
				lbFill4();
			}
		}
		private void gvFill(string qr)
		{
			sdsContract.ConnectionString =
				DBConnection.connection.ConnectionString.ToString();
			sdsContract.SelectCommand = qr;
			sdsContract.DataSourceMode = SqlDataSourceMode.DataReader;
			gvContract.DataSource = sdsContract;
			gvContract.DataBind();
		}
		private void lbFill1()
		{
			sdsContract.ConnectionString =
			DBConnection.connection.ConnectionString.ToString();
			sdsContract.SelectCommand = DBConnection.qrStaff;
			sdsContract.DataSourceMode = SqlDataSourceMode.DataReader;
			lbStaff_ID.DataSource = sdsContract;
			lbStaff_ID.DataTextField = "Фамилия сотрудника";
			lbStaff_ID.DataValueField = "ID_Staff";
			lbStaff_ID.DataBind();
		}

		private void lbFill2()
		{
			sdsContract.ConnectionString =
			DBConnection.connection.ConnectionString.ToString();
			sdsContract.SelectCommand = DBConnection.qrCustomers;
			sdsContract.DataSourceMode = SqlDataSourceMode.DataReader;
			lbCustomer_ID.DataSource = sdsContract;
			lbCustomer_ID.DataTextField = "Фамилия клиента";
			lbCustomer_ID.DataValueField = "ID_Customer";
			lbCustomer_ID.DataBind();
		}

		private void lbFill3()
		{
			sdsContract.ConnectionString =
			DBConnection.connection.ConnectionString.ToString();
			sdsContract.SelectCommand = DBConnection.qrTypeOfContract;
			sdsContract.DataSourceMode = SqlDataSourceMode.DataReader;
			lbTypeOfContract_ID.DataSource = sdsContract;
			lbTypeOfContract_ID.DataTextField = "Описание";
			lbTypeOfContract_ID.DataValueField = "ID_TypeOfContract";
			lbTypeOfContract_ID.DataBind();
		}

		private void lbFill4()
		{
			sdsContract.ConnectionString =
			DBConnection.connection.ConnectionString.ToString();
			sdsContract.SelectCommand = DBConnection.qrService;
			sdsContract.DataSourceMode = SqlDataSourceMode.DataReader;
			lbService_ID.DataSource = sdsContract;
			lbService_ID.DataTextField = "Доступно";
			lbService_ID.DataValueField = "ID_Service";
			lbService_ID.DataBind();
		}

		protected void btSearch_Click(object sender, EventArgs e)
		{
			foreach (GridViewRow row in gvContract.Rows)
			{
				if (row.Cells[2].Text.Equals(tbSearch.Text)
					|| row.Cells[3].Text.Equals(tbSearch.Text)
					|| row.Cells[4].Text.Equals(tbSearch.Text))
					row.BackColor = System.Drawing.Color.DarkGray;
				else
					row.BackColor = System.Drawing.Color.White;
			}
		}

		protected void btFilter_Click(object sender, EventArgs e)
		{
			string newQr = QR + " where [ContractDate] like '%" + tbSearch.Text + "%' or " +
			   "[ContractExpirationDate] like '%" + tbSearch.Text + "%' or " +
			   "[TransactionAmount] like '%" + tbSearch.Text + "%'";
			gvFill(newQr);
		}

		protected void btCancel_Click(object sender, EventArgs e)
		{
			tbSearch.Text = "";
			btSearch_Click(sender, e);
			gvFill(QR);
		}

		protected void gvContract_Sorting(object sender, GridViewSortEventArgs e)
		{
			SortDirection sortDirection = SortDirection.Ascending;
			string strField = string.Empty;
			switch (e.SortExpression)
			{
				case ("Дата контракта"):
					e.SortExpression = "[ContractDate]";
					break;
				case ("Дата истечения контракта"):
					e.SortExpression = "[ContractExpirationDate]";
					break;
				case ("Сумма перевода"):
					e.SortExpression = "[TransactionAmount]";
					break;
			}
			sortGridView(gvContract, e, out sortDirection, out strField);
			string strDirection = sortDirection
				== SortDirection.Ascending ? "ASC" : "DESC";
			gvFill(QR + " order by " + e.SortExpression + " " + strDirection);
		}

		private void sortGridView(GridView gridView,
			GridViewSortEventArgs e,
			out SortDirection sortDirection,
			out string strSortField)
		{
			strSortField = e.SortExpression;
			sortDirection = e.SortDirection;

			if (gridView.Attributes["CurrentSortField"] != null &&
				gridView.Attributes["CurrentSortDirection"] != null)
			{
				if (strSortField ==
					gridView.Attributes["CurrentSortField"])
				{
					if (gridView.Attributes["CurrentSortDirection"]
						== "ASC")
					{
						sortDirection = SortDirection.Descending;
					}
					else
					{
						sortDirection = SortDirection.Ascending;
					}
				}
			}
			gridView.Attributes["CurrentSortField"] = strSortField;
			gridView.Attributes["CurrentSortDirection"] =
				(sortDirection == SortDirection.Ascending ? "ASC"
				: "DESC");
		}

		protected void gvContract_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			e.Row.Cells[1].Visible = false;
		}

		protected void gvContract_SelectedIndexChanged(object sender, EventArgs e)
		{
			GridViewRow row = gvContract.SelectedRow;
			tbContractDate.Text = row.Cells[2].Text.ToString();
			tbContractExpirationDate.Text = row.Cells[3].Text.ToString();
			tbTransactionAmount.Text = row.Cells[4].Text.ToString();
			DBConnection.IDrecord = Convert.ToInt32(row.Cells[1].Text.ToString());
		}

		protected void btInsert_Click(object sender, EventArgs e)
		{
			switch (tbContractDate.Text == "")
			{
				case (true):
					tbContractDate.BackColor = System.Drawing.Color.Red;
					break;
				case (false):
					tbContractDate.BackColor = System.Drawing.Color.White;
					switch (tbContractExpirationDate.Text == "")
					{
						case (true):
							tbContractExpirationDate.BackColor = System.Drawing.Color.Red;
							break;
						case (false):
							tbContractExpirationDate.BackColor = System.Drawing.Color.White;
							switch (tbTransactionAmount.Text == "")
							{
								case (true):
									tbTransactionAmount.BackColor = System.Drawing.Color.Red;
									break;
								case (false):
									tbTransactionAmount.BackColor = System.Drawing.Color.White;
									int id1 = Convert.ToInt32(lbStaff_ID.SelectedValue.ToString());
									int id2 = Convert.ToInt32(lbCustomer_ID.SelectedValue.ToString());
									int id3 = Convert.ToInt32(lbTypeOfContract_ID.SelectedValue.ToString());
									int id4 = Convert.ToInt32(lbService_ID.SelectedValue.ToString());
									SqlCommand command = new SqlCommand("", DBConnection.connection);
									command.CommandText = "insert into [dbo].[Contract] ([ContractDate]" +
									 " ,[ContractExpirationDate]" +
									  ",[TransactionAmount]" +
									  ",[Staff_ID]" +
									  ",[Customer_ID]" +
									  ",[TypeOfContract_ID]" +
									  ",[Service_ID]" +
									  ") values ('" + tbContractDate.Text + "','"
										+ tbContractExpirationDate.Text + "','" + tbTransactionAmount.Text + "','"
										+ lbStaff_ID.SelectedValue + "','" + lbCustomer_ID.SelectedValue + "','" + lbTypeOfContract_ID.SelectedValue + "','" + lbService_ID.SelectedValue + "')";
									DBConnection.connection.Open();
									command.ExecuteNonQuery();
									DBConnection.connection.Close();
									Page_Load(sender, e);
									gvFill(QR);
									break;
							}
							break;
					}
					break;

			}
		}

		protected void btUpdate_Click(object sender, EventArgs e)
		{
			switch (tbContractDate.Text == "")
			{
				case (true):
					tbContractDate.BackColor = System.Drawing.Color.Red;
					break;
				case (false):
					tbContractDate.BackColor = System.Drawing.Color.White;
					switch (tbContractExpirationDate.Text == "")
					{
						case (true):
							tbContractExpirationDate.BackColor = System.Drawing.Color.Red;
							break;
						case (false):
							tbContractExpirationDate.BackColor = System.Drawing.Color.White;
							switch (tbTransactionAmount.Text == "")
							{
								case (true):
									tbTransactionAmount.BackColor = System.Drawing.Color.Red;
									break;
								case (false):
									tbTransactionAmount.BackColor = System.Drawing.Color.White;
									int id1 = Convert.ToInt32(lbStaff_ID.SelectedValue.ToString());
									int id2 = Convert.ToInt32(lbCustomer_ID.SelectedValue.ToString());
									int id3 = Convert.ToInt32(lbTypeOfContract_ID.SelectedValue.ToString());
									int id4 = Convert.ToInt32(lbService_ID.SelectedValue.ToString());
									SqlCommand command = new SqlCommand("", DBConnection.connection);
									command.CommandText = "update [dbo].[Contract] set " +
									 "[ContractDate] = '" + tbContractDate.Text + "', " +
									 "[ContractExpirationDate] = '" + tbContractExpirationDate.Text + "', " +
									 "[TransactionAmount] = '" + tbTransactionAmount.Text + "', " +
									 "[Staff_ID] = '" + lbStaff_ID.SelectedValue + "', " +
									 "[Customer_ID] = '" + lbCustomer_ID.SelectedValue + "', " +
									 "[TypeOfContract_ID] = '" + lbTypeOfContract_ID.SelectedValue + "', " +
									 "[Service_ID] = '" + lbService_ID.SelectedValue + "'" +
									 " where ID_Contract = " + DBConnection.IDrecord + "";
									DBConnection.connection.Open();
									command.ExecuteNonQuery();
									DBConnection.connection.Close();
									Page_Load(sender, e);
									gvFill(QR);
									break;
							}
							break;
					}
					break;

			}
		}

		protected void btDelete_Click(object sender, EventArgs e)
		{
					SqlCommand command = new SqlCommand("", DBConnection.connection);
					command.CommandText = "delete from [dbo].[Contract] " +
						"where ID_Contract = " + DBConnection.IDrecord + "";
					DBConnection.connection.Open();
					command.ExecuteNonQuery();
					DBConnection.connection.Close();
					Page_Load(sender, e);
					gvFill(QR);
		}

		protected void btBack_Click(object sender, EventArgs e)
		{
			DBConnection connection = new DBConnection();
			Response.Redirect("FormMenu.aspx");
		}
	}
}