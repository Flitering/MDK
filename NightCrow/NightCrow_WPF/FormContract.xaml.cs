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
	/// Логика взаимодействия для FormContract.xaml
	/// </summary>
	public partial class FormContract : Window
	{
		public FormContract()
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
			DBConnection.qrContract = qr;
			connection.ContractFill();
			dgFormContract.ItemsSource = connection.dtContract.DefaultView;
			dgFormContract.Columns[0].Visibility = Visibility.Collapsed;
		}
		private void cbFill1()
		{
			DBConnection connection = new DBConnection();
			connection.StaffFill();
			cbStaff_ID.ItemsSource = connection.dtStaff.DefaultView;
			cbStaff_ID.SelectedValuePath = "ID_Staff";
			cbStaff_ID.DisplayMemberPath = "Фамилия сотрудника";
		}
		private void cbFill2()
		{
			DBConnection connection = new DBConnection();
			connection.CustomersFill();
			cbCustomer_ID.ItemsSource = connection.dtCustomers.DefaultView;
			cbCustomer_ID.SelectedValuePath = "ID_Customer";
			cbCustomer_ID.DisplayMemberPath = "Фамилия клиента";
		}
		private void cbFill3()
		{
			DBConnection connection = new DBConnection();
			connection.TypeOfСontractFill();
			cbTypeOfContract_ID.ItemsSource = connection.dtTypeOfContract.DefaultView;
			cbTypeOfContract_ID.SelectedValuePath = "ID_TypeOfContract";
			cbTypeOfContract_ID.DisplayMemberPath = "Описание";
		}
		private void cbFill4()
		{
			DBConnection connection = new DBConnection();
			connection.ServiceFill();
			cbService_ID.ItemsSource = connection.dtService.DefaultView;
			cbService_ID.SelectedValuePath = "ID_Service";
			cbService_ID.DisplayMemberPath = "Доступно";
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			QR = DBConnection.qrContract;
			dgFill(QR);
			cbFill1();
			cbFill2();
			cbFill3();
			cbFill4();
		}

		private void DgFormContract_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
		{
			switch (e.Column.Header)
			{
				case ("ContractDate"):
					e.Column.Header = "Дата контракта";
					break;
				case ("ContractExpirationDate"):
					e.Column.Header = "Дата истечения контракта";
					break;
				case ("TransactionAmount"):
					e.Column.Header = "Сумма перевода";
					break;
			}
		}

		private void BtDelete_Click(object sender, RoutedEventArgs e)
		{
			switch (MessageBox.Show("Удалить запись?", "Удаление записи", MessageBoxButton.YesNo, MessageBoxImage.Warning))
			{
				case MessageBoxResult.Yes:
					DataRowView ID =
						(DataRowView)dgFormContract.SelectedItems[0];
					procedures.spContract_delete(Convert.ToInt32(ID["ID_Contract"]));
					dgFill(QR);
					break;
			}
		}

		private void BtInsert_Click(object sender, RoutedEventArgs e)
		{
			switch (tbContractDate.Text == "")
			{
				case (true):
					tbContractDate.Background = Brushes.Red;
					break;
				case (false):
					tbContractDate.Background = Brushes.White;
					switch (tbContractExpirationDate.Text == "")
					{
						case (true):
							tbContractExpirationDate.Background = Brushes.Red;
							break;
						case (false):
							tbContractExpirationDate.Background = Brushes.White;
							switch (tbTransactionAmount.Text == "")
							{
								case (true):
									tbTransactionAmount.Background = Brushes.Red;
									break;
								case (false):
									tbTransactionAmount.Background = Brushes.White;
									string strClient_Code = "";
									SqlCommand command = new SqlCommand("Select Count(*) " +
										"from [dbo].[Contract]", DBConnection.connection);
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
									procedures.spContract_insert(tbContractDate.Text, tbContractExpirationDate.Text, tbTransactionAmount.Text, Convert.ToInt32(cbStaff_ID.SelectedValue), Convert.ToInt32(cbCustomer_ID.SelectedValue), Convert.ToInt32(cbTypeOfContract_ID.SelectedValue), Convert.ToInt32(cbService_ID.SelectedValue));
									dgFill(QR);
									cbFill1();
									cbFill2();
									cbFill3();
									cbFill4();
									break;
							}
							break;
					}
					break;
			}

		}


		private void BtUpdate_Click(object sender, RoutedEventArgs e)
		{
			switch (tbContractDate.Text == "")
			{
				case (true):
					tbContractDate.Background = Brushes.Red;
					break;
				case (false):
					tbContractDate.Background = Brushes.White;
					switch (tbContractExpirationDate.Text == "")
					{
						case (true):
							tbContractExpirationDate.Background = Brushes.Red;
							break;
						case (false):
							tbContractExpirationDate.Background = Brushes.White;
							switch (tbTransactionAmount.Text == "")
							{
								case (true):
									tbTransactionAmount.Background = Brushes.Red;
									break;
								case (false):
									tbTransactionAmount.Background = Brushes.White;
									DataRowView ID = (DataRowView)dgFormContract.SelectedItems[0];
									procedures.spContract_update(Convert.ToInt32(ID["ID_Contract"]),
									tbContractDate.Text, tbContractExpirationDate.Text, tbTransactionAmount.Text, Convert.ToInt32(cbStaff_ID.SelectedValue), Convert.ToInt32(cbCustomer_ID.SelectedValue), Convert.ToInt32(cbTypeOfContract_ID.SelectedValue), Convert.ToInt32(cbService_ID.SelectedValue));
									dgFill(QR);
									cbFill1();
									cbFill2();
									cbFill3();
									cbFill4();
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
				lblContractDate.FontSize = 13;
				lblContractExpirationDate.FontSize = 13;
				lblTransactionAmount.FontSize = 13;
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
					lblContractDate.FontSize = 15;
					lblContractExpirationDate.FontSize = 15;
					lblTransactionAmount.FontSize = 15;
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
						lblContractDate.FontSize = 18;
						lblContractExpirationDate.FontSize = 18;
						lblTransactionAmount.FontSize = 18;
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
							lblContractDate.FontSize = 20;
							lblContractExpirationDate.FontSize = 20;
							lblTransactionAmount.FontSize = 20;
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
					string newQr = QR + " where [ContractDate] like '%" + tbSearch.Text + "%' or " +
				"[ContractExpirationDate] like '%" + tbSearch.Text + "%' or " +
			    "[TransactionAmount] like '%" + tbSearch.Text + "%'";
					dgFill(newQr);
					break;
				case (false):
					foreach (DataRowView dataRow in (DataView)dgFormContract.ItemsSource)
					{
						if (dataRow.Row.ItemArray[1].ToString() == tbSearch.Text)
						{
							dgFormContract.SelectedItem = dataRow;
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
