using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace NightCrow
{
	public partial class FormCustomers : System.Web.UI.Page
	{
		private string QR = "";
		protected void Page_Load(object sender, EventArgs e)
		{
			QR = DBConnection.qrCustomers;
			if (!IsPostBack)
			{
				gvFill(QR);
			}
		}
		private void gvFill(string qr)
		{
			sdsCustomers.ConnectionString =
				DBConnection.connection.ConnectionString.ToString();
			sdsCustomers.SelectCommand = qr;
			sdsCustomers.DataSourceMode = SqlDataSourceMode.DataReader;
			gvCustomers.DataSource = sdsCustomers;
			gvCustomers.DataBind();
		}

		protected void btSearch_Click(object sender, EventArgs e)
		{
			foreach (GridViewRow row in gvCustomers.Rows)
			{
				if (row.Cells[2].Text.Equals(tbSearch.Text)
					|| row.Cells[3].Text.Equals(tbSearch.Text)
					|| row.Cells[4].Text.Equals(tbSearch.Text)
					|| row.Cells[5].Text.Equals(tbSearch.Text)
					|| row.Cells[6].Text.Equals(tbSearch.Text))
					row.BackColor = System.Drawing.Color.DarkGray;
				else
					row.BackColor = System.Drawing.Color.White;
			}
		}

		protected void btFilter_Click(object sender, EventArgs e)
		{
			string newQr = QR + " where [FNameC] like '%" + tbSearch.Text + "%' or " +
			   "[MNameC] like '%" + tbSearch.Text + "%' or " +
			   "[LNameC] like '%" + tbSearch.Text + "%' or" +
			   "[CustomerPosition] like '%" + tbSearch.Text + "%' or" +
			   "[CustomerPhone] like '%" + tbSearch.Text + "%'";
			gvFill(newQr);
		}

		protected void btCancel_Click(object sender, EventArgs e)
		{
			tbSearch.Text = "";
			btSearch_Click(sender, e);
			gvFill(QR);
		}

		protected void gvCustomers_Sorting(object sender, GridViewSortEventArgs e)
		{
			SortDirection sortDirection = SortDirection.Ascending;
			string strField = string.Empty;
			switch (e.SortExpression)
			{
				case ("Имя клиента"):
					e.SortExpression = "[FNameC]";
					break;
				case ("Фамилия клиента"):
					e.SortExpression = "[MNameC]";
					break;
				case ("Отчество клиента"):
					e.SortExpression = "[LNameC]";
					break;
				case ("Должность клиента"):
					e.SortExpression = "[CustomerPosition]";
					break;
				case ("Телефон клиента"):
					e.SortExpression = "[CustomerPhone]";
					break;
			}
			sortGridView(gvCustomers, e, out sortDirection, out strField);
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

		protected void gvCustomers_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			e.Row.Cells[1].Visible = false;
		}

		protected void gvCustomers_SelectedIndexChanged(object sender, EventArgs e)
		{
			GridViewRow row = gvCustomers.SelectedRow;
			tbFNameC.Text = row.Cells[2].Text.ToString();
			tbMNameC.Text = row.Cells[3].Text.ToString();
			tbLNameC.Text = row.Cells[4].Text.ToString();
			tbCustomerPosition.Text = row.Cells[5].Text.ToString();
			tbCustomerPhone.Text = row.Cells[6].Text.ToString();
			DBConnection.IDrecord = Convert.ToInt32(row.Cells[1].Text.ToString());
		}

		protected void btInsert_Click(object sender, EventArgs e)
		{
			switch (tbFNameC.Text == "")
			{
				case (true):
					tbFNameC.BackColor = System.Drawing.Color.Red;
					break;
				case (false):
					tbFNameC.BackColor = System.Drawing.Color.White;
					switch (tbMNameC.Text == "")
					{
						case (true):
							tbMNameC.BackColor = System.Drawing.Color.Red;
							break;
						case (false):
							tbMNameC.BackColor = System.Drawing.Color.White;
							switch (tbLNameC.Text == "")
							{
								case (true):
									tbLNameC.BackColor = System.Drawing.Color.Red;
									break;
								case (false):
									tbLNameC.BackColor = System.Drawing.Color.White;
									switch (tbCustomerPosition.Text == "")
									{
										case (true):
											tbCustomerPosition.BackColor = System.Drawing.Color.Red;
											break;
										case (false):
											tbCustomerPosition.BackColor = System.Drawing.Color.White;
											switch (tbCustomerPhone.Text == "")
											{
												case (true):
													tbCustomerPhone.BackColor = System.Drawing.Color.Red;
													break;
												case (false):
													tbCustomerPhone.BackColor = System.Drawing.Color.White;
													SqlCommand command = new SqlCommand("", DBConnection.connection);
													command.CommandText = "insert into [dbo].[Customers] ([FNameC]" +
													 " ,[MNameC]" +
													  ",[LNameC]" +
													  ",[CustomerPosition]" +
													  ",[CustomerPhone]" +
													  ") values ('" + tbFNameC.Text + "','"
														+ tbMNameC.Text + "','" + tbLNameC.Text + "','"
														+ tbCustomerPosition.Text + "','" + tbCustomerPhone.Text + "')";
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
							break;
					}
					break;
			}
						
		}

		protected void btUpdate_Click(object sender, EventArgs e)
		{
			switch (tbFNameC.Text == "")
			{
				case (true):
					tbFNameC.BackColor = System.Drawing.Color.Red;
					break;
				case (false):
					tbFNameC.BackColor = System.Drawing.Color.White;
					switch (tbMNameC.Text == "")
					{
						case (true):
							tbMNameC.BackColor = System.Drawing.Color.Red;
							break;
						case (false):
							tbMNameC.BackColor = System.Drawing.Color.White;
							switch (tbLNameC.Text == "")
							{
								case (true):
									tbLNameC.BackColor = System.Drawing.Color.Red;
									break;
								case (false):
									tbLNameC.BackColor = System.Drawing.Color.White;
									switch (tbCustomerPosition.Text == "")
									{
										case (true):
											tbCustomerPosition.BackColor = System.Drawing.Color.Red;
											break;
										case (false):
											tbCustomerPosition.BackColor = System.Drawing.Color.White;
											switch (tbCustomerPhone.Text == "")
											{
												case (true):
													tbCustomerPhone.BackColor = System.Drawing.Color.Red;
													break;
												case (false):
													tbCustomerPhone.BackColor = System.Drawing.Color.White;
													SqlCommand command = new SqlCommand("", DBConnection.connection);
													command.CommandText = "update [dbo].[Customers] set " +
													  "[FNameC] = '" + tbFNameC.Text + "', " +
													  "[MNameC] = '" + tbMNameC.Text + "', " +
													  "[LNameC] = '" + tbLNameC.Text + "', " +
													  "[CustomerPosition] = '" + tbCustomerPosition.Text + "', " +
													  "[CustomerPhone] = '" + tbCustomerPhone.Text + "'" +
													  " where ID_Customer = " + DBConnection.IDrecord + "";
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
							break;
					}
					break;
			}
		}

		protected void btDelete_Click(object sender, EventArgs e)
		{
			SqlCommand command = new SqlCommand("", DBConnection.connection);
			command.CommandText = "delete from [dbo].[Customers] " +
				"where ID_Customer = " + DBConnection.IDrecord + "";
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