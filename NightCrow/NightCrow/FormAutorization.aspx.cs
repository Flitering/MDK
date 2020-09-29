using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace NightCrow
{
	public partial class WebForm1 : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void btOpen_Click(object sender, EventArgs e)
		{
			DBConnection connection = new DBConnection();
			connection.dbEnter(tbLoginStaff.Text, tbPasswordStaff.Text);
			switch (DBConnection.IDuser)
			{
				case (0):
					tbLoginStaff.BackColor = System.Drawing.Color.Red;
					tbPasswordStaff.BackColor = System.Drawing.Color.Red;
					tbPasswordStaff.Text = "";
					tbLoginStaff.Text = "";
					break;
				default:
					Response.Redirect("FormMenu.aspx");
					break;
			}
		}
	}
}
