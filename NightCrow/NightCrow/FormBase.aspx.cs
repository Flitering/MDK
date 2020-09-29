using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace NightCrow
{
	public partial class FormBase : System.Web.UI.Page
	{
		private string QR = "";
		protected void Page_Load(object sender, EventArgs e)
		{
			QR = DBConnection.qrBase;
			if (!IsPostBack)
			{
				gvFill(QR);
			}
		}
		private void gvFill(string qr)
		{
			sdsBase.ConnectionString =
				DBConnection.connection.ConnectionString.ToString();
			sdsBase.SelectCommand = qr;
			sdsBase.DataSourceMode = SqlDataSourceMode.DataReader;
			gvBase.DataSource = sdsBase;
			gvBase.DataBind();
		}

		protected void btSearch_Click(object sender, EventArgs e)
		{
			foreach (GridViewRow row in gvBase.Rows)
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
			string newQr = QR + " where [BaseAddress] like '%" + tbSearch.Text + "%' or " +
			   "[ProvidesServices] like '%" + tbSearch.Text + "%'";
			gvFill(newQr);
		}

		protected void btCancel_Click(object sender, EventArgs e)
		{
			tbSearch.Text = "";
			btSearch_Click(sender, e);
			gvFill(QR);
		}

		protected void gvBase_Sorting(object sender, GridViewSortEventArgs e)
		{
			SortDirection sortDirection = SortDirection.Ascending;
			string strField = string.Empty;
			switch (e.SortExpression)
			{
				case ("Адрес базы"):
					e.SortExpression = "[BaseAddress]";
					break;
				case ("Услуги предоставляет"):
					e.SortExpression = "[ProvidesServices]";
					break;
			}
			sortGridView(gvBase, e, out sortDirection, out strField);
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

		protected void gvBase_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			e.Row.Cells[1].Visible = false;
		}

		protected void gvBase_SelectedIndexChanged(object sender, EventArgs e)
		{
			GridViewRow row = gvBase.SelectedRow;
			tbBaseAddress.Text = row.Cells[2].Text.ToString();
			tbProvidesServices.Text = row.Cells[3].Text.ToString();
			DBConnection.IDrecord = Convert.ToInt32(row.Cells[1].Text.ToString());
		}

		protected void btInsert_Click(object sender, EventArgs e)
		{
			switch (tbBaseAddress.Text == "")
			{
				case (true):
					tbBaseAddress.BackColor = System.Drawing.Color.Red;
					break;
				case (false):
					tbBaseAddress.BackColor = System.Drawing.Color.White;
					switch (tbProvidesServices.Text == "")
					{
						case (true):
							tbProvidesServices.BackColor = System.Drawing.Color.Red;
							break;
						case (false):
							tbProvidesServices.BackColor = System.Drawing.Color.White;
							SqlCommand command = new SqlCommand("", DBConnection.connection);
							command.CommandText = "insert into [dbo].[Base] ([BaseAddress]" +
							 " ,[ProvidesServices]" +
							  ") values ('" + tbBaseAddress.Text + "','"
								+ tbProvidesServices.Text + "')";
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
			switch (tbBaseAddress.Text == "")
			{
				case (true):
					tbBaseAddress.BackColor = System.Drawing.Color.Red;
					break;
				case (false):
					tbBaseAddress.BackColor = System.Drawing.Color.White;
					switch (tbProvidesServices.Text == "")
					{
						case (true):
							tbProvidesServices.BackColor = System.Drawing.Color.Red;
							break;
						case (false):
							tbProvidesServices.BackColor = System.Drawing.Color.White;
							SqlCommand command = new SqlCommand("", DBConnection.connection);
							command.CommandText = "update [dbo].[Base] set " +
							 "[BaseAddress] = '" + tbBaseAddress.Text + "', " +
							 "[ProvidesServices] = '" + tbProvidesServices.Text + "'" +
							 " where ID_Base = " + DBConnection.IDrecord + "";
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
			command.CommandText = "delete from [dbo].[Base] " +
				"where ID_Base = " + DBConnection.IDrecord + "";
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