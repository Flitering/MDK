using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace NightCrow
{
	public partial class FormService : System.Web.UI.Page
	{
		private string QR = "";
		protected void Page_Load(object sender, EventArgs e)
		{
			QR = DBConnection.qrService;
			if (!IsPostBack)
			{
				gvFill(QR);
				lbFill1();
				lbFill2();
			}
		}
		private void gvFill(string qr)
		{
			sdsService.ConnectionString =
				DBConnection.connection.ConnectionString.ToString();
			sdsService.SelectCommand = qr;
			sdsService.DataSourceMode = SqlDataSourceMode.DataReader;
			gvService.DataSource = sdsService;
			gvService.DataBind();
		}
		private void lbFill1()
		{
			sdsService.ConnectionString =
			DBConnection.connection.ConnectionString.ToString();
			sdsService.SelectCommand = DBConnection.qrTypeOfService;
			sdsService.DataSourceMode = SqlDataSourceMode.DataReader;
			lbTypeOfService_ID.DataSource = sdsService;
			lbTypeOfService_ID.DataTextField = "Название";
			lbTypeOfService_ID.DataValueField = "ID_TypeOfService";
			lbTypeOfService_ID.DataBind();
		}

		private void lbFill2()
		{
			sdsService.ConnectionString =
			DBConnection.connection.ConnectionString.ToString();
			sdsService.SelectCommand = DBConnection.qrBase;
			sdsService.DataSourceMode = SqlDataSourceMode.DataReader;
			lbBase_ID.DataSource = sdsService;
			lbBase_ID.DataTextField = "Адрес базы";
			lbBase_ID.DataValueField = "ID_Base";
			lbBase_ID.DataBind();
		}

		protected void btSearch_Click(object sender, EventArgs e)
		{
			foreach (GridViewRow row in gvService.Rows)
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
			string newQr = QR + " where [HiringContractEmployees] like '%" + tbSearch.Text + "%' or " +
			   "[Available] like '%" + tbSearch.Text + "%'";
			gvFill(newQr);
		}

		protected void btCancel_Click(object sender, EventArgs e)
		{
			tbSearch.Text = "";
			btSearch_Click(sender, e);
			gvFill(QR);
		}

		protected void gvService_Sorting(object sender, GridViewSortEventArgs e)
		{
			SortDirection sortDirection = SortDirection.Ascending;
			string strField = string.Empty;
			switch (e.SortExpression)
			{
				case ("Найм служащих по контракту"):
					e.SortExpression = "[HiringContractEmployees]";
					break;
				case ("Доступно"):
					e.SortExpression = "[Available]";
					break;
			}
			sortGridView(gvService, e, out sortDirection, out strField);
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

		protected void gvService_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			e.Row.Cells[1].Visible = false;
		}

		protected void gvService_SelectedIndexChanged(object sender, EventArgs e)
		{
			GridViewRow row = gvService.SelectedRow;
			tbHiringContractEmployees.Text = row.Cells[2].Text.ToString();
			tbAvailable.Text = row.Cells[3].Text.ToString();
			DBConnection.IDrecord = Convert.ToInt32(row.Cells[1].Text.ToString());
		}

		protected void btInsert_Click(object sender, EventArgs e)
		{
			switch (tbHiringContractEmployees.Text == "")
			{
				case (true):
					tbHiringContractEmployees.BackColor = System.Drawing.Color.Red;
					break;
				case (false):
					tbHiringContractEmployees.BackColor = System.Drawing.Color.White;
					switch (tbAvailable.Text == "")
					{
						case (true):
							tbAvailable.BackColor = System.Drawing.Color.Red;
							break;
						case (false):
							tbAvailable.BackColor = System.Drawing.Color.White;
							int id1 = Convert.ToInt32(lbTypeOfService_ID.SelectedValue.ToString());
							int id2 = Convert.ToInt32(lbBase_ID.SelectedValue.ToString());
							SqlCommand command = new SqlCommand("", DBConnection.connection);
							command.CommandText = "insert into [dbo].[Service] ([HiringContractEmployees]" +
							" ,[Available]" +
							",[TypeOfService_ID]" +
							",[Base_ID]" +
							") values ('" + tbHiringContractEmployees.Text + "','"
							+ tbAvailable.Text + "','" + lbTypeOfService_ID.SelectedValue + "','" + lbBase_ID.SelectedValue + "')";
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
			switch (tbHiringContractEmployees.Text == "")
			{
				case (true):
					tbHiringContractEmployees.BackColor = System.Drawing.Color.Red;
					break;
				case (false):
					tbHiringContractEmployees.BackColor = System.Drawing.Color.White;
					switch (tbAvailable.Text == "")
					{
						case (true):
							tbAvailable.BackColor = System.Drawing.Color.Red;
							break;
						case (false):
							tbAvailable.BackColor = System.Drawing.Color.White;
							int id1 = Convert.ToInt32(lbTypeOfService_ID.SelectedValue.ToString());
							int id2 = Convert.ToInt32(lbBase_ID.SelectedValue.ToString());
							SqlCommand command = new SqlCommand("", DBConnection.connection);
							command.CommandText = "update [dbo].[Service] set " +
							 "[HiringContractEmployees] = '" + tbHiringContractEmployees.Text + "', " +
							 "[Available] = '" + tbAvailable.Text + "', " +
							 "[TypeOfService_ID] = '" + lbTypeOfService_ID.SelectedValue + "', " +
							 "[Base_ID] = '" + lbBase_ID.SelectedValue + "'" +
							 " where ID_Service = " + DBConnection.IDrecord + "";
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
			command.CommandText = "delete from [dbo].[Service] " +
				"where ID_Service = " + DBConnection.IDrecord + "";
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