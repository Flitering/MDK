using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using NightCrow;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace NightCrow_WPF
{
	/// <summary>
	/// Логика взаимодействия для FormService.xaml
	/// </summary>
	public partial class FormService : Window
	{
		public FormService()
		{
			InitializeComponent();
			ChangeSize((int)Width, (int)Height);
		}
		private string QR = "";
		DBProcedures procedures = new DBProcedures();
		private void BtBack_Click(object sender, RoutedEventArgs e)
		{
			DBProcedures procedures = new DBProcedures();
			Menu Menu = new Menu();
			Menu.Show();
			Close();
		}
		private void dgFill(string qr)
		{
			DBConnection connection = new DBConnection();
			DBConnection.qrService = qr;
			connection.ServiceFill();
			dgFormService.ItemsSource = connection.dtService.DefaultView;
			dgFormService.Columns[0].Visibility = Visibility.Collapsed;
		}
		private void cbFill1()
		{
			DBConnection connection = new DBConnection();
			connection.TypeOfServiceFill();
			cbTypeOfService_ID.ItemsSource = connection.dtTypeOfService.DefaultView;
			cbTypeOfService_ID.SelectedValuePath = "ID_TypeOfService";
			cbTypeOfService_ID.DisplayMemberPath = "Название";
		}
		private void cbFill2()
		{
			DBConnection connection = new DBConnection();
			connection.BaseFill();
			cbBase_ID.ItemsSource = connection.dtBase.DefaultView;
			cbBase_ID.SelectedValuePath = "ID_Base";
			cbBase_ID.DisplayMemberPath = "Адрес базы";
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			QR = DBConnection.qrService;
			dgFill(QR);
			cbFill1();
			cbFill2();
		}

		private void DgFormService_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
		{
			switch (e.Column.Header)
			{
				case ("HiringContractEmployees"):
					e.Column.Header = "Найм служащих по контракту";
					break;
				case ("Available"):
					e.Column.Header = "Доступно";
					break;
			}
		}

		private void BtDelete_Click(object sender, RoutedEventArgs e)
		{
			switch (MessageBox.Show("Удалить запись?", "Удаление записи", MessageBoxButton.YesNo, MessageBoxImage.Warning))
			{
				case MessageBoxResult.Yes:
					DataRowView ID =
						(DataRowView)dgFormService.SelectedItems[0];
					procedures.spService_delete(Convert.ToInt32(ID["ID_Service"]));
					dgFill(QR);
					break;
			}
		}

		private void BtInsert_Click(object sender, RoutedEventArgs e)
		{
			switch (tbHiringContractEmployees.Text == "")
			{
				case (true):
					tbHiringContractEmployees.Background = Brushes.Red;
					break;
				case (false):
					tbHiringContractEmployees.Background = Brushes.White;
					switch (tbAvailable.Text == "")
					{
						case (true):
							tbAvailable.Background = Brushes.Red;
							break;
						case (false):
							tbAvailable.Background = Brushes.White;
							string strClient_Code = "";
							SqlCommand command = new SqlCommand("Select Count(*) " +
								"from [dbo].[Service]", DBConnection.connection);
							DBConnection.connection.Open();
							Int32 productCount = Convert.ToInt32(command.
								ExecuteScalar().
								ToString());
							DBConnection.connection.Close();
							for (Int32 i = 0; i < 10 - productCount.ToString().Length; i++)
							{
								strClient_Code += "0";
							}
							strClient_Code += productCount + 1;
							procedures.spService_insert(tbHiringContractEmployees.Text, tbAvailable.Text, Convert.ToInt32(cbTypeOfService_ID.SelectedValue), Convert.ToInt32(cbBase_ID.SelectedValue));
							dgFill(QR);
							cbFill1();
							cbFill2();
							break;
					}
					break;
			}

		}


		private void BtUpdate_Click(object sender, RoutedEventArgs e)
		{
			switch (tbHiringContractEmployees.Text == "")
			{
				case (true):
					tbHiringContractEmployees.Background = Brushes.Red;
					break;
				case (false):
					tbHiringContractEmployees.Background = Brushes.White;
					switch (tbAvailable.Text == "")
					{
						case (true):
							tbAvailable.Background = Brushes.Red;
							break;
						case (false):
							tbAvailable.Background = Brushes.White;
							DataRowView ID = (DataRowView)dgFormService.SelectedItems[0];
							procedures.spService_update(Convert.ToInt32(ID["ID_Service"]),
							tbHiringContractEmployees.Text, tbAvailable.Text, Convert.ToInt32(cbTypeOfService_ID.SelectedValue), Convert.ToInt32(cbBase_ID.SelectedValue));
							dgFill(QR);
							cbFill1();
							cbFill2();
							break;
					}
					break;
			}
		}
		private void ChbFilter_Unchecked(object sender, RoutedEventArgs e)
		{
			dgFill(QR);
		}
		private void ChangeSize(int X, int Y)
		{

			if ((X >= 0 || Y >= 0) && (X < 800 || Y < 600))
			{
				lblTitle.FontSize = 13;
				lblHiringContractEmployees.FontSize = 13;
				lblAvailable.FontSize = 13;
				btBack.FontSize = 13;
				btDelete.FontSize = 13;
				btInsert.FontSize = 13;
				btUpdate.FontSize = 13;
			}
			else
			{
				if ((X >= 800 || Y >= 600) && (X < 1280 || Y < 1024))
				{
					lblTitle.FontSize = 15;
					lblHiringContractEmployees.FontSize = 15;
					lblAvailable.FontSize = 15;
					btBack.FontSize = 15;
					btDelete.FontSize = 15;
					btInsert.FontSize = 15;
					btUpdate.FontSize = 15;
				}
				else
				{
					if ((X >= 1280 || Y >= 1024) && (X < 1600 || Y < 900))
					{
						lblTitle.FontSize = 18;
						lblHiringContractEmployees.FontSize = 18;
						lblAvailable.FontSize = 18;
						btBack.FontSize = 18;
						btDelete.FontSize = 18;
						btInsert.FontSize = 18;
						btUpdate.FontSize = 18;
					}
					else
					{
						if ((X >= 1920 || Y >= 1080))
						{
							lblTitle.FontSize = 20;
							lblHiringContractEmployees.FontSize = 20;
							lblAvailable.FontSize = 20;
							btBack.FontSize = 20;
							btDelete.FontSize = 20;
							btInsert.FontSize = 20;
							btUpdate.FontSize = 20;
						}
					}
				}
			}
		}
		private void BtSearch_Click(object sender, RoutedEventArgs e)
		{
			switch (chbFilter.IsChecked)
			{
				case (true):
					string newQr = QR + " where [HiringContractEmployees] like '%" + tbSearch.Text + "%' or " +
			   "[Available] like '%" + tbSearch.Text + "%'";
					dgFill(newQr);
					break;
				case (false):
					foreach (DataRowView dataRow in (DataView)dgFormService.ItemsSource)
					{
						if (dataRow.Row.ItemArray[1].ToString() == tbSearch.Text)
						{
							dgFormService.SelectedItem = dataRow;
						}
					}
					break;
			}
		}
		private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			ChangeSize((int)e.NewSize.Width, (int)e.NewSize.Height);
		}
	}
}