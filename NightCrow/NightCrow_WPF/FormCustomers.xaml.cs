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
	/// Логика взаимодействия для FormCustomers.xaml
	/// </summary>
	public partial class FormCustomers : Window
	{
		public FormCustomers()
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
			DBConnection.qrCustomers = qr;
			connection.CustomersFill();
			dgFormCustomers.ItemsSource = connection.dtCustomers.DefaultView;
			dgFormCustomers.Columns[0].Visibility = Visibility.Collapsed;
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			QR = DBConnection.qrCustomers;
			dgFill(QR);
		}

		private void DgFormCustomers_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
		{
			switch (e.Column.Header)
			{
				case ("FNameC"):
					e.Column.Header = "Имя клиента";
					break;
				case ("MNameC"):
					e.Column.Header = "Фамилия клиента";
					break;
				case ("LNameC"):
					e.Column.Header = "Отчество клиента";
					break;
				case ("CustomerPosition"):
					e.Column.Header = "Должность клиента";
					break;
				case ("CustomerPhone"):
					e.Column.Header = "Телефон клиента";
					break;
			}
		}

		private void BtDelete_Click(object sender, RoutedEventArgs e)
		{
			switch (MessageBox.Show("Удалить запись?", "Удаление записи", MessageBoxButton.YesNo, MessageBoxImage.Warning))
			{
				case MessageBoxResult.Yes:
					DataRowView ID =
						(DataRowView)dgFormCustomers.SelectedItems[0];
					procedures.spCustomers_delete(Convert.ToInt32(ID["ID_Customer"]));
					dgFill(QR);
					break;
			}
		}

		private void BtInsert_Click(object sender, RoutedEventArgs e)
		{
			switch (tbFNameC.Text == "")
			{
				case (true):
					tbFNameC.Background = Brushes.Red;
					break;
				case (false):
					tbFNameC.Background = Brushes.White;
					switch (tbMNameC.Text == "")
					{
						case (true):
							tbMNameC.Background = Brushes.Red;
							break;
						case (false):
							tbMNameC.Background = Brushes.White;
							switch (tbLNameC.Text == "")
							{
								case (true):
									tbLNameC.Background = Brushes.Red;
									break;
								case (false):
									tbLNameC.Background = Brushes.White;
									switch (tbCustomerPosition.Text == "")
									{
										case (true):
											tbCustomerPosition.Background = Brushes.Red;
											break;
										case (false):
											tbCustomerPosition.Background = Brushes.White;
											switch (tbCustomerPhone.Text == "")
											{
												case (true):
													tbCustomerPhone.Background = Brushes.Red;
													break;
												case (false):
													tbCustomerPhone.Background = Brushes.White;
													string strClient_Code = "";
													SqlCommand command = new SqlCommand("Select Count(*) " +
														"from [dbo].[Customers]", DBConnection.connection);
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
													procedures.spCustomers_insert(tbFNameC.Text, tbMNameC.Text, tbLNameC.Text, tbCustomerPosition.Text, tbCustomerPhone.Text);
													dgFill(QR);
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

		private void BtUpdate_Click(object sender, RoutedEventArgs e)
		{
			switch (tbFNameC.Text == "")
			{
				case (true):
					tbFNameC.Background = Brushes.Red;
					break;
				case (false):
					tbFNameC.Background = Brushes.White;
					switch (tbMNameC.Text == "")
					{
						case (true):
							tbMNameC.Background = Brushes.Red;
							break;
						case (false):
							tbMNameC.Background = Brushes.White;
							switch (tbLNameC.Text == "")
							{
								case (true):
									tbLNameC.Background = Brushes.Red;
									break;
								case (false):
									tbLNameC.Background = Brushes.White;
									switch (tbCustomerPosition.Text == "")
									{
										case (true):
											tbCustomerPosition.Background = Brushes.Red;
											break;
										case (false):
											tbCustomerPosition.Background = Brushes.White;
											switch (tbCustomerPhone.Text == "")
											{
												case (true):
													tbCustomerPhone.Background = Brushes.Red;
													break;
												case (false):
													tbCustomerPhone.Background = Brushes.White;
													DataRowView ID = (DataRowView)dgFormCustomers.SelectedItems[0];
													procedures.spCustomers_update(Convert.ToInt32(ID["ID_Customer"]),
													tbFNameC.Text, tbMNameC.Text, tbLNameC.Text, tbCustomerPosition.Text, tbCustomerPhone.Text);
													dgFill(QR);
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

		private void ChbFilter_Unchecked(object sender, RoutedEventArgs e)
		{
		dgFill(QR);
		}

		private void ChangeSize(int X, int Y)
		{

			if ((X >= 0 || Y >= 0) && (X < 800 || Y < 600))
			{
				lblTitle.FontSize = 13;
				lblFNameC.FontSize = 13;
				lblMNameC.FontSize = 13;
				lblLNameC.FontSize = 13;
				lblCustomerPosition.FontSize = 13;
				lblCustomerPhone.FontSize = 13;
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
					lblFNameC.FontSize = 15;
					lblMNameC.FontSize = 15;
					lblLNameC.FontSize = 15;
					lblCustomerPosition.FontSize = 15;
					lblCustomerPhone.FontSize = 15;
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
						lblFNameC.FontSize = 18;
						lblMNameC.FontSize = 18;
						lblLNameC.FontSize = 18;
						lblCustomerPosition.FontSize = 18;
						lblCustomerPhone.FontSize = 18;
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
							lblFNameC.FontSize = 20;
							lblMNameC.FontSize = 20;
							lblLNameC.FontSize = 20;
							lblCustomerPosition.FontSize = 20;
							lblCustomerPhone.FontSize = 20;
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
					string newQr = QR + " where [FNameC] like '%" + tbSearch.Text + "%' or " +
			   "[MNameC] like '%" + tbSearch.Text + "%' or " +
			   "[LNameC] like '%" + tbSearch.Text + "%' or" +
			   "[CustomerPosition] like '%" + tbSearch.Text + "%' or" +
			   "[CustomerPhone] like '%" + tbSearch.Text + "%'";
					dgFill(newQr);
					break;
				case (false):
					foreach (DataRowView dataRow in (DataView)dgFormCustomers.ItemsSource)
					{
						if (dataRow.Row.ItemArray[1].ToString() == tbSearch.Text)
						{
							dgFormCustomers.SelectedItem = dataRow;
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
