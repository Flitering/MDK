using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace NightCrow
{
	public partial class FormPosition : System.Web.UI.Page
	{
		private string QR = "";
		protected void Page_Load(object sender, EventArgs e)
		{
			QR = DBConnection.qrPosition;
			if (!IsPostBack)
			{
				gvFill(QR);
			}
		}
		private void gvFill(string qr)
		{
			sdsPosition.ConnectionString =
				DBConnection.connection.ConnectionString.ToString();
			sdsPosition.SelectCommand = qr;
			sdsPosition.DataSourceMode = SqlDataSourceMode.DataReader;
			gvPosition.DataSource = sdsPosition;
			gvPosition.DataBind();
		}

		protected void btSearch_Click(object sender, EventArgs e)
		{
			foreach (GridViewRow row in gvPosition.Rows)
			{
				if (row.Cells[2].Text.Equals(tbSearch.Text)
					|| row.Cells[3].Text.Equals(tbSearch.Text))
					row.BackColor = System.Drawing.Color.DarkGray;
				else
					row.BackColor = System.Drawing.Color.White;
			}
		}

		protected void btFilter_Click(object sender, EventArgs e)
		{
			string newQr = QR + " where [EmployeesPosition] like '%" + tbSearch.Text + "%' or " +
			   "[Salary] like '%" + tbSearch.Text + "%'";
			gvFill(newQr);
		}

		protected void btCancel_Click(object sender, EventArgs e)
		{
			tbSearch.Text = "";
			btSearch_Click(sender, e);
			gvFill(QR);
		}

		protected void gvPosition_Sorting(object sender, GridViewSortEventArgs e)
		{
			SortDirection sortDirection = SortDirection.Ascending;
			string strField = string.Empty;
			switch (e.SortExpression)
			{
				case ("Должность подчинённого"):
					e.SortExpression = "[EmployeesPosition]";
					break;
				case ("Оклад"):
					e.SortExpression = "[Salary]";
					break;
			}
			sortGridView(gvPosition, e, out sortDirection, out strField);
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

		protected void gvPosition_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			e.Row.Cells[1].Visible = false;
		}

		protected void gvPosition_SelectedIndexChanged(object sender, EventArgs e)
		{
			GridViewRow row = gvPosition.SelectedRow;
			tbEmployeesPosition.Text = row.Cells[2].Text.ToString();
			tbSalary.Text = row.Cells[3].Text.ToString();
			DBConnection.IDrecord = Convert.ToInt32(row.Cells[1].Text.ToString());
		}

		protected void btInsert_Click(object sender, EventArgs e)
		{
			switch (tbEmployeesPosition.Text == "")
			{
				case (true):
					tbEmployeesPosition.BackColor = System.Drawing.Color.Red;
					break;
				case (false):
					tbEmployeesPosition.BackColor = System.Drawing.Color.White;
					switch (tbSalary.Text == "")
					{
						case (true):
							tbSalary.BackColor = System.Drawing.Color.Red;
							break;
						case (false):
							tbSalary.BackColor = System.Drawing.Color.White;
							SqlCommand command = new SqlCommand("", DBConnection.connection);
							command.CommandText = "insert into [dbo].[Position] ([EmployeesPosition]" +
							 " ,[Salary]" +
							  ") values ('" + tbEmployeesPosition.Text + "','"
								+ tbSalary.Text + "')";
							DBConnection.connection.Open();
							command.ExecuteNonQuery();
							DBConnection.connection.Close();
							Page_Load(sender, e);
							gvFill(QR);
							break;
					}
					break;
			}
		}

		protected void btUpdate_Click(object sender, EventArgs e)
		{
			switch (tbEmployeesPosition.Text == "")
			{
				case (true):
					tbEmployeesPosition.BackColor = System.Drawing.Color.Red;
					break;
				case (false):
					tbEmployeesPosition.BackColor = System.Drawing.Color.White;
					switch (tbSalary.Text == "")
					{
						case (true):
							tbSalary.BackColor = System.Drawing.Color.Red;
							break;
						case (false):
							tbSalary.BackColor = System.Drawing.Color.White;
							SqlCommand command = new SqlCommand("", DBConnection.connection);
							command.CommandText = "update [dbo].[Position] set " +
							 "[EmployeesPosition] = '" + tbEmployeesPosition.Text + "', " +
							 "[Salary] = '" + tbSalary.Text + "'" +
							 " where ID_Position = " + DBConnection.IDrecord + "";
							DBConnection.connection.Open();
							command.ExecuteNonQuery();
							DBConnection.connection.Close();
							Page_Load(sender, e);
							gvFill(QR);
							break;
					}
					break;
			}
		}

		protected void btDelete_Click(object sender, EventArgs e)
		{
			SqlCommand command = new SqlCommand("", DBConnection.connection);
			command.CommandText = "delete from [dbo].[Position] " +
				"where ID_Position = " + DBConnection.IDrecord + "";
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