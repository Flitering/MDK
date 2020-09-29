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
	/// Логика взаимодействия для FormPosition.xaml
	/// </summary>
	public partial class FormPosition : Window
	{
		public FormPosition()
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
			DBConnection.qrPosition = qr;
			connection.PositionFill();
			dgFormPosition.ItemsSource = connection.dtPosition.DefaultView;
			dgFormPosition.Columns[0].Visibility = Visibility.Collapsed;
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			QR = DBConnection.qrPosition;
			dgFill(QR);
		}

		private void DgFormPosition_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
		{
			switch (e.Column.Header)
			{
				case ("EmployeesPosition"):
					e.Column.Header = "Должность подчинённого";
					break;
				case ("Salary"):
					e.Column.Header = "Оклад";
					break;
			}
		}

		private void BtDelete_Click(object sender, RoutedEventArgs e)
		{
			switch (MessageBox.Show("Удалить запись?", "Удаление записи", MessageBoxButton.YesNo, MessageBoxImage.Warning))
			{
				case MessageBoxResult.Yes:
					DataRowView ID =
						(DataRowView)dgFormPosition.SelectedItems[0];
					procedures.spPosition_delete(Convert.ToInt32(ID["ID_Position"]));
					dgFill(QR);
					break;
			}
		}

		private void BtInsert_Click(object sender, RoutedEventArgs e)
		{
			switch (tbEmployeesPosition.Text == "")
			{
				case (true):
					tbEmployeesPosition.Background = Brushes.Red;
					break;
				case (false):
					tbEmployeesPosition.Background = Brushes.White;
					switch (tbSalary.Text == "")
					{
						case (true):
							tbSalary.Background = Brushes.Red;
							break;
						case (false):
							tbSalary.Background = Brushes.White;
							string strClient_Code = "";
							SqlCommand command = new SqlCommand("Select Count(*) " +
								"from [dbo].[Position]", DBConnection.connection);
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
							procedures.spPosition_insert(tbEmployeesPosition.Text, tbSalary.Text);
							dgFill(QR);
							break;
					}
					break;
			}
		}

		private void BtUpdate_Click(object sender, RoutedEventArgs e)
		{
			switch (tbEmployeesPosition.Text == "")
			{
				case (true):
					tbEmployeesPosition.Background = Brushes.Red;
					break;
				case (false):
					tbEmployeesPosition.Background = Brushes.White;
					switch (tbSalary.Text == "")
					{
						case (true):
							tbSalary.Background = Brushes.Red;
							break;
						case (false):
							tbSalary.Background = Brushes.White;
							DataRowView ID = (DataRowView)dgFormPosition.SelectedItems[0];
							procedures.spPosition_update(Convert.ToInt32(ID["ID_Position"]),
								 tbEmployeesPosition.Text, tbSalary.Text);
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
				lblEmployeesPosition.FontSize = 13;
				lblSalary.FontSize = 13;
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
					lblEmployeesPosition.FontSize = 15;
					lblSalary.FontSize = 15;
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
						lblEmployeesPosition.FontSize = 18;
						lblSalary.FontSize = 18;
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
							lblEmployeesPosition.FontSize = 20;
							lblSalary.FontSize = 20;
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
					string newQr = QR + "where [EmployeesPosition] like '%" + tbSearch.Text + "%' or " + "[Salary] like '%" + tbSearch.Text + "%'";
					dgFill(newQr);
					break;
				case (false):
					foreach (DataRowView dataRow in (DataView)dgFormPosition.ItemsSource)
					{
						if (dataRow.Row.ItemArray[1].ToString() == tbSearch.Text)
						{
							dgFormPosition.SelectedItem = dataRow;
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