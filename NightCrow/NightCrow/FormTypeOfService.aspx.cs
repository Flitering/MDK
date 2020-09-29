using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace NightCrow
{
	public partial class FormTypeOfService : System.Web.UI.Page
	{
		private string QR = "";
		protected void Page_Load(object sender, EventArgs e)
		{
			QR = DBConnection.qrTypeOfService;
			if (!IsPostBack)
			{
				gvFill(QR);
			}
		}
		private void gvFill(string qr)
		{
			sdsTypeOfService.ConnectionString =
				DBConnection.connection.ConnectionString.ToString();
			sdsTypeOfService.SelectCommand = qr;
			sdsTypeOfService.DataSourceMode = SqlDataSourceMode.DataReader;
			gvTypeOfService.DataSource = sdsTypeOfService;
			gvTypeOfService.DataBind();
		}

		protected void btSearch_Click(object sender, EventArgs e)
		{
			foreach (GridViewRow row in gvTypeOfService.Rows)
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
			string newQr = QR + " where [Name] like '%" + tbSearch.Text + "%' or " +
			   "[Description] like '%" + tbSearch.Text + "%'";
			gvFill(newQr);
		}

		protected void btCancel_Click(object sender, EventArgs e)
		{
			tbSearch.Text = "";
			btSearch_Click(sender, e);
			gvFill(QR);
		}

		protected void gvTypeOfService_Sorting(object sender, GridViewSortEventArgs e)
		{
			SortDirection sortDirection = SortDirection.Ascending;
			string strField = string.Empty;
			switch (e.SortExpression)
			{
				case ("Название"):
					e.SortExpression = "[Name]";
					break;
				case ("Описание"):
					e.SortExpression = "[Description]";
					break;
			}
			sortGridView(gvTypeOfService, e, out sortDirection, out strField);
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

		protected void gvTypeOfService_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			e.Row.Cells[1].Visible = false;
		}

		protected void gvTypeOfService_SelectedIndexChanged(object sender, EventArgs e)
		{
			GridViewRow row = gvTypeOfService.SelectedRow;
			tbName.Text = row.Cells[2].Text.ToString();
			tbDescription.Text = row.Cells[3].Text.ToString();
			DBConnection.IDrecord = Convert.ToInt32(row.Cells[1].Text.ToString());
		}

		protected void btDelete_Click(object sender, EventArgs e)
		{
			SqlCommand command = new SqlCommand("", DBConnection.connection);
			command.CommandText = "delete from [dbo].[TypeOfService] " +
				"where ID_TypeOfService = " + DBConnection.IDrecord + "";
			DBConnection.connection.Open();
			command.ExecuteNonQuery();
			DBConnection.connection.Close();
			Page_Load(sender, e);
			gvFill(QR);
		}

		protected void btInsert_Click(object sender, EventArgs e)
		{
			switch (tbName.Text == "")
			{
				case (true):
					tbName.BackColor = System.Drawing.Color.Red;
					break;
				case (false):
					tbName.BackColor = System.Drawing.Color.White;
					switch (tbDescription.Text == "")
					{
						case (true):
							tbDescription.BackColor = System.Drawing.Color.Red;
							break;
						case (false):
							tbDescription.BackColor = System.Drawing.Color.White;
							SqlCommand command = new SqlCommand("", DBConnection.connection);
							command.CommandText = "insert into [dbo].[TypeOfService] ([Name]" +
							 " ,[Description]" +
							  ") values ('" + tbName.Text + "','"
								+ tbDescription.Text + "')";
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
			switch (tbName.Text == "")
			{
				case (true):
					tbName.BackColor = System.Drawing.Color.Red;
					break;
				case (false):
					tbName.BackColor = System.Drawing.Color.White;
					switch (tbDescription.Text == "")
					{
						case (true):
							tbDescription.BackColor = System.Drawing.Color.Red;
							break;
						case (false):
							tbDescription.BackColor = System.Drawing.Color.White;
							SqlCommand command = new SqlCommand("", DBConnection.connection);
							command.CommandText = "update [dbo].[TypeOfService] set " +
							 "[Name] = '" + tbName.Text + "', " +
							 "[Description] = '" + tbDescription.Text + "'" +
							 " where ID_TypeOfService = " + DBConnection.IDrecord + "";
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

		protected void btBack_Click(object sender, EventArgs e)
		{
			DBConnection connection = new DBConnection();
			Response.Redirect("FormMenu.aspx");
		}
	}
}