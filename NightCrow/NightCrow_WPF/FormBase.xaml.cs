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
	/// Логика взаимодействия для FormBase.xaml
	/// </summary>
	public partial class FormBase : Window
	{
		public FormBase()
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
			try
			{
				DBConnection connection = new DBConnection();
				DBConnection.qrBase = qr;
				connection.BaseFill();
				dgFormBase.ItemsSource = connection.dtBase.DefaultView;
				dgFormBase.Columns[0].Visibility = Visibility.Collapsed;
			}
			catch { }
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			QR = DBConnection.qrBase;
			dgFill(QR);
		}

		private void DgFormBase_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
		{
			switch (e.Column.Header)
			{
				case ("BaseAddress"):
					e.Column.Header = "Адрес базы";
					break;
				case ("ProvidesServices"):
					e.Column.Header = "Услуги предоставляет";
					break;
			}
		}

		private void BtDelete_Click(object sender, RoutedEventArgs e)
		{
			switch (MessageBox.Show("Удалить запись?", "Удаление записи", MessageBoxButton.YesNo, MessageBoxImage.Warning))
			{
				case MessageBoxResult.Yes:
					DataRowView ID =
						(DataRowView)dgFormBase.SelectedItems[0];
					procedures.spBase_delete(Convert.ToInt32(ID["ID_Base"]));
					dgFill(QR);
					break;
			}
		}

		private void BtInsert_Click(object sender, RoutedEventArgs e)
		{
			switch (tbBaseAddress.Text == "")
			{
				case (true):
					tbBaseAddress.Background = Brushes.Red;
					break;
				case (false):
					tbBaseAddress.Background = Brushes.White;
					switch (tbProvidesServices.Text == "")
					{
						case (true):
							tbProvidesServices.Background = Brushes.Red;
							break;
						case (false):
							tbProvidesServices.Background = Brushes.White;
							string strClient_Code = "";
							SqlCommand command = new SqlCommand("Select Count(*) " +
								"from [dbo].[Base]", DBConnection.connection);
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
							procedures.spBase_insert(tbBaseAddress.Text, tbProvidesServices.Text);
							dgFill(QR);
							break;
					}
					break;
			}
		}

		private void BtUpdate_Click(object sender, RoutedEventArgs e)
		{
			switch (tbBaseAddress.Text == "")
			{
				case (true):
					tbBaseAddress.Background = Brushes.Red;
					break;
				case (false):
					tbBaseAddress.Background = Brushes.White;
					switch (tbProvidesServices.Text == "")
					{
						case (true):
							tbProvidesServices.Background = Brushes.Red;
							break;
						case (false):
							tbProvidesServices.Background = Brushes.White;
							DataRowView ID = (DataRowView)dgFormBase.SelectedItems[0];
							procedures.spBase_update(Convert.ToInt32(ID["ID_Base"]),
								 tbBaseAddress.Text, tbProvidesServices.Text);
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
				lblBaseAddress.FontSize = 13;
				lblProvidesServices.FontSize = 13;
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
					lblBaseAddress.FontSize = 15;
					lblProvidesServices.FontSize = 15;
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
						lblBaseAddress.FontSize = 18;
						lblProvidesServices.FontSize = 18;
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
							lblBaseAddress.FontSize = 20;
							lblProvidesServices.FontSize = 20;
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
					string newQr = QR + "where [BaseAddress] like '%" + tbSearch.Text + "%' or " + "[ProvidesServices] like '%" + tbSearch.Text + "%'";
					dgFill(newQr);
					break;
				case (false):
					foreach (DataRowView dataRow in (DataView)dgFormBase.ItemsSource)
					{
						if (dataRow.Row.ItemArray[1].ToString() == tbSearch.Text)
						{
							dgFormBase.SelectedItem = dataRow;
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
