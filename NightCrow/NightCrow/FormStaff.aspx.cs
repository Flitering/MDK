using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace NightCrow
{
	public partial class FormStaff : System.Web.UI.Page
	{
		private string QR = "";
		protected void Page_Load(object sender, EventArgs e)
		{
			QR = DBConnection.qrStaff;
			if (!IsPostBack)
			{
				gvFill(QR);
				lbFill1();
			}
		}
		private void gvFill(string qr)
		{
			sdsStaff.ConnectionString =
				DBConnection.connection.ConnectionString.ToString();
			sdsStaff.SelectCommand = qr;
			sdsStaff.DataSourceMode = SqlDataSourceMode.DataReader;
			gvStaff.DataSource = sdsStaff;
			gvStaff.DataBind();
		}
		private void lbFill1()
		{
			sdsStaff.ConnectionString =
			DBConnection.connection.ConnectionString.ToString();
			sdsStaff.SelectCommand = DBConnection.qrPosition;
			sdsStaff.DataSourceMode = SqlDataSourceMode.DataReader;
			lbPosition_ID.DataSource = sdsStaff;
			lbPosition_ID.DataTextField = "Должность подчинённого";
			lbPosition_ID.DataValueField = "ID_Position";
			lbPosition_ID.DataBind();
		}

		protected void btSearch_Click(object sender, EventArgs e)
		{
			foreach (GridViewRow row in gvStaff.Rows)
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
			string newQr = QR + " where [FNameS] like '%" + tbSearch.Text + "%' or " +
			   "[MNameS] like '%" + tbSearch.Text + "%' or " +
			   "[LNameS] like '%" + tbSearch.Text + "%' or " +
			   "[LoginS] like '%" + tbSearch.Text + "%' or " +
			   "[PasswordS] like '%" + tbSearch.Text + "%'";
			gvFill(newQr);
		}

		protected void btCancel_Click(object sender, EventArgs e)
		{
			tbSearch.Text = "";
			btSearch_Click(sender, e);
			gvFill(QR);
		}

		protected void gvStaff_Sorting(object sender, GridViewSortEventArgs e)
		{
			SortDirection sortDirection = SortDirection.Ascending;
			string strField = string.Empty;
			switch (e.SortExpression)
			{
				case ("Имя сотрудника"):
					e.SortExpression = "[FNameS]";
					break;
				case ("Фамилия сотрудника"):
					e.SortExpression = "[MNameS]";
					break;
				case ("Отчество сотрудника"):
					e.SortExpression = "[LNameS]";
					break;
				case ("Логин сотрудника"):
					e.SortExpression = "[LoginS]";
					break;
				case ("Пароль сотрудника"):
					e.SortExpression = "[PasswordS]";
					break;
			}
			sortGridView(gvStaff, e, out sortDirection, out strField);
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

		protected void gvStaff_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			e.Row.Cells[1].Visible = false;
			e.Row.Cells[6].Visible = false;
		}

		protected void gvStaff_SelectedIndexChanged(object sender, EventArgs e)
		{
			GridViewRow row = gvStaff.SelectedRow;
			tbFNameS.Text = row.Cells[2].Text.ToString();
			tbMNameS.Text = row.Cells[3].Text.ToString();
			tbLNameS.Text = row.Cells[4].Text.ToString();
			tbLoginS.Text = row.Cells[5].Text.ToString();
			tbPasswordS.Text = row.Cells[6].Text.ToString();
			DBConnection.IDrecord = Convert.ToInt32(row.Cells[1].Text.ToString());
		}

		protected void btInsert_Click(object sender, EventArgs e)
		{
			switch (tbFNameS.Text == "")
			{
				case (true):
					tbFNameS.BackColor = System.Drawing.Color.Red;
					break;
				case (false):
					tbFNameS.BackColor = System.Drawing.Color.White;
					switch (tbMNameS.Text == "")
					{
						case (true):
							tbMNameS.BackColor = System.Drawing.Color.Red;
							break;
						case (false):
							tbMNameS.BackColor = System.Drawing.Color.White;
							switch (tbLNameS.Text == "")
							{
								case (true):
									tbLNameS.BackColor = System.Drawing.Color.Red;
									break;
								case (false):
									tbLNameS.BackColor = System.Drawing.Color.White;
									switch (tbLoginS.Text == "")
									{
										case (true):
											tbLoginS.BackColor = System.Drawing.Color.Red;
											break;
										case (false):
											tbLoginS.BackColor = System.Drawing.Color.White;
											switch (tbPasswordS.Text == "")
											{
												case (true):
													tbPasswordS.BackColor = System.Drawing.Color.Red;
													break;
												case (false):
													tbPasswordS.BackColor = System.Drawing.Color.White;
													SqlCommand command = new SqlCommand("", DBConnection.connection);
													command.CommandText = "insert into [dbo].[Staff] ([FNameS]" +
													 " ,[MNameS]" +
													  ",[LNameS]" +
													  ",[LoginS]" +
													  ",[PasswordS]" +
													  ",[Position_ID]" +
													  ") values ('" + tbFNameS.Text + "','"
														+ tbMNameS.Text + "','" + tbLNameS.Text + "','"
														+ tbLoginS.Text + "','" + tbPasswordS.Text + "','" + lbPosition_ID.SelectedValue + "')";
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
			switch (tbFNameS.Text == "")
			{
				case (true):
					tbFNameS.BackColor = System.Drawing.Color.Red;
					break;
				case (false):
					tbFNameS.BackColor = System.Drawing.Color.White;
					switch (tbMNameS.Text == "")
					{
						case (true):
							tbMNameS.BackColor = System.Drawing.Color.Red;
							break;
						case (false):
							tbMNameS.BackColor = System.Drawing.Color.White;
							switch (tbLNameS.Text == "")
							{
								case (true):
									tbLNameS.BackColor = System.Drawing.Color.Red;
									break;
								case (false):
									tbLNameS.BackColor = System.Drawing.Color.White;
									switch (tbLoginS.Text == "")
									{
										case (true):
											tbLoginS.BackColor = System.Drawing.Color.Red;
											break;
										case (false):
											tbLoginS.BackColor = System.Drawing.Color.White;
											switch (tbPasswordS.Text == "")
											{
												case (true):
													tbPasswordS.BackColor = System.Drawing.Color.Red;
													break;
												case (false):
													tbPasswordS.BackColor = System.Drawing.Color.White;
													SqlCommand command = new SqlCommand("", DBConnection.connection);
													command.CommandText = "update [dbo].[Staff] set " +
													  "[FNameS] = '" + tbFNameS.Text + "', " +
													  "[MNameS] = '" + tbMNameS.Text + "', " +
													  "[LNameS] = '" + tbLNameS.Text + "', " +
													  "[LoginS] = '" + tbLoginS.Text + "', " +
													  "[PasswordS] = '" + tbPasswordS.Text + "', " +
													  "[Position_ID] = '" + lbPosition_ID.SelectedValue + "'" +
													  " where ID_Staff = " + DBConnection.IDrecord + "";
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
			command.CommandText = "delete from [dbo].[Staff] " +
				"where ID_Staff = " + DBConnection.IDrecord + "";
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