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
	/// Логика взаимодействия для FormTypeOfService.xaml
	/// </summary>
	public partial class FormTypeOfService : Window
	{
		public FormTypeOfService()
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
			DBConnection.qrTypeOfService = qr;
			connection.TypeOfServiceFill();
			dgFormTypeOfService.ItemsSource = connection.dtTypeOfService.DefaultView;
			dgFormTypeOfService.Columns[0].Visibility = Visibility.Collapsed;
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			QR = DBConnection.qrTypeOfService;
			dgFill(QR);
		}

		private void DgFormTypeOfService_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
		{
			switch (e.Column.Header)
			{
				case ("Name"):
					e.Column.Header = "Название";
					break;
				case ("Description"):
					e.Column.Header = "Описание";
					break;
			}
		}

		private void BtDelete_Click(object sender, RoutedEventArgs e)
		{
			switch (MessageBox.Show("Удалить запись?", "Удаление записи", MessageBoxButton.YesNo, MessageBoxImage.Warning))
			{
				case MessageBoxResult.Yes:
					DataRowView ID =
						(DataRowView)dgFormTypeOfService.SelectedItems[0];
					procedures.spTypeOfService_delete(Convert.ToInt32(ID["ID_TypeOfService"]));
					dgFill(QR);
					break;
			}
		}

		private void BtInsert_Click(object sender, RoutedEventArgs e)
		{
			switch (tbName.Text == "")
			{
				case (true):
					tbName.Background = Brushes.Red;
					break;
				case (false):
					tbName.Background = Brushes.White;
					switch (tbDescription.Text == "")
					{
						case (true):
							tbDescription.Background = Brushes.Red;
							break;
						case (false):
							tbDescription.Background = Brushes.White;
							string strClient_Code = "";
							SqlCommand command = new SqlCommand("Select Count(*) " +
								"from [dbo].[TypeOfService]", DBConnection.connection);
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
							procedures.spTypeOfService_insert(tbName.Text, tbDescription.Text);
							dgFill(QR);
							break;
					}
					break;
			}
		}

		private void BtUpdate_Click(object sender, RoutedEventArgs e)
		{
			switch (tbName.Text == "")
			{
				case (true):
					tbName.Background = Brushes.Red;
					break;
				case (false):
					tbName.Background = Brushes.White;
					switch (tbDescription.Text == "")
					{
						case (true):
							tbDescription.Background = Brushes.Red;
							break;
						case (false):
							tbDescription.Background = Brushes.White;
							DataRowView ID = (DataRowView)dgFormTypeOfService.SelectedItems[0];
							procedures.spTypeOfService_update(Convert.ToInt32(ID["ID_TypeOfService"]),
							tbName.Text, tbDescription.Text);
							dgFill(QR);
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
				lblName.FontSize = 13;
				lblDescription.FontSize = 13;
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
					lblName.FontSize = 15;
					lblDescription.FontSize = 15;
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
						lblName.FontSize = 18;
						lblDescription.FontSize = 18;
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
							lblName.FontSize = 20;
							lblDescription.FontSize = 20;
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
					string newQr = QR + " where [Name] like '%" + tbSearch.Text + "%' or " +
			   "[Description] like '%" + tbSearch.Text + "%'";
					dgFill(newQr);
					break;
				case (false):
					foreach (DataRowView dataRow in (DataView)dgFormTypeOfService.ItemsSource)
					{
						if (dataRow.Row.ItemArray[1].ToString() == tbSearch.Text)
						{
							dgFormTypeOfService.SelectedItem = dataRow;
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
