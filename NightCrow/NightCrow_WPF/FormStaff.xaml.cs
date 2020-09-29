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
	/// Логика взаимодействия для FormStaff.xaml
	/// </summary>
	public partial class FormStaff : Window
	{
		public FormStaff()
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
			DBConnection.qrStaff = qr;
			connection.StaffFill();
			dgFormStaff.ItemsSource = connection.dtStaff.DefaultView;
			dgFormStaff.Columns[0].Visibility = Visibility.Collapsed;
		}
		private void cbFill()
		{
			DBConnection connection = new DBConnection();
			connection.PositionFill();
			cbPosition_ID.ItemsSource = connection.dtPosition.DefaultView;
			cbPosition_ID.SelectedValuePath = "ID_Position";
			cbPosition_ID.DisplayMemberPath = "Должность подчинённого";
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			QR = DBConnection.qrStaff;
			dgFill(QR);
			cbFill();
		}

		private void DgFormStaff_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
		{
			switch (e.Column.Header)
			{
				case ("FNameS"):
					e.Column.Header = "Имя сотрудника";
					break;
				case ("MNameS"):
					e.Column.Header = "Фамилия сотрудника";
					break;
				case ("LNameS"):
					e.Column.Header = "Отчество сотрудника";
					break;
				case ("LoginS"):
					e.Column.Header = "Логин сотрудника";
					break;
				case ("PasswordS"):
					e.Column.Header = "Пароль сотрудника";
					break;
			}
		}

		private void BtDelete_Click(object sender, RoutedEventArgs e)
		{
			switch (MessageBox.Show("Удалить запись?", "Удаление записи", MessageBoxButton.YesNo, MessageBoxImage.Warning))
			{
				case MessageBoxResult.Yes:
					DataRowView ID =
						(DataRowView)dgFormStaff.SelectedItems[0];
					procedures.spStaff_delete(Convert.ToInt32(ID["ID_Staff"]));
					dgFill(QR);
					break;
			}
		}

		private void BtInsert_Click(object sender, RoutedEventArgs e)
		{
			switch (tbFNameS.Text == "")
			{
				case (true):
					tbFNameS.Background = Brushes.Red;
					break;
				case (false):
					tbFNameS.Background = Brushes.White;
					switch (tbMNameS.Text == "")
					{
						case (true):
							tbMNameS.Background = Brushes.Red;
							break;
						case (false):
							tbMNameS.Background = Brushes.White;
							switch (tbLNameS.Text == "")
							{
								case (true):
									tbLNameS.Background = Brushes.Red;
									break;
								case (false):
									tbLNameS.Background = Brushes.White;
									switch (tbLoginS.Text == "")
									{
										case (true):
											tbLoginS.Background = Brushes.Red;
											break;
										case (false):
											tbLoginS.Background = Brushes.White;
											switch (tbPasswordS.Text == "")
											{
												case (true):
													tbPasswordS.Background = Brushes.Red;
													break;
												case (false):
													tbPasswordS.Background = Brushes.White;
													string strClient_Code = "";
													SqlCommand command = new SqlCommand("Select Count(*) " +
														"from [dbo].[Staff]", DBConnection.connection);
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
													procedures.spStaff_insert(tbFNameS.Text, tbMNameS.Text, tbLNameS.Text, tbLoginS.Text, tbPasswordS.Text, Convert.ToInt32(cbPosition_ID.SelectedValue));
													dgFill(QR);
													cbFill();
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
			switch (tbFNameS.Text == "")
			{
				case (true):
					tbFNameS.Background = Brushes.Red;
					break;
				case (false):
					tbFNameS.Background = Brushes.White;
					switch (tbMNameS.Text == "")
					{
						case (true):
							tbMNameS.Background = Brushes.Red;
							break;
						case (false):
							tbMNameS.Background = Brushes.White;
							switch (tbLNameS.Text == "")
							{
								case (true):
									tbLNameS.Background = Brushes.Red;
									break;
								case (false):
									tbLNameS.Background = Brushes.White;
									switch (tbLoginS.Text == "")
									{
										case (true):
											tbLoginS.Background = Brushes.Red;
											break;
										case (false):
											tbLoginS.Background = Brushes.White;
											switch (tbPasswordS.Text == "")
											{
												case (true):
													tbPasswordS.Background = Brushes.Red;
													break;
												case (false):
													tbPasswordS.Background = Brushes.White;
													DataRowView ID = (DataRowView)dgFormStaff.SelectedItems[0];
													procedures.spStaff_update(Convert.ToInt32(ID["ID_Staff"]),
													tbFNameS.Text, tbMNameS.Text, tbLNameS.Text, tbLoginS.Text, tbPasswordS.Text, Convert.ToInt32(cbPosition_ID.SelectedValue));
													dgFill(QR);
													cbFill();
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
				lblFNameS.FontSize = 13;
				lblMNameS.FontSize = 13;
				lblLNameS.FontSize = 13;
				lblLoginS.FontSize = 13;
				lblPasswordS.FontSize = 13;
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
					lblFNameS.FontSize = 15;
					lblMNameS.FontSize = 15;
					lblLNameS.FontSize = 15;
					lblLoginS.FontSize = 15;
					lblPasswordS.FontSize = 15;
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
						lblFNameS.FontSize = 18;
						lblMNameS.FontSize = 18;
						lblLNameS.FontSize = 18;
						lblLoginS.FontSize = 18;
						lblPasswordS.FontSize = 18;
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
							lblFNameS.FontSize = 20;
							lblMNameS.FontSize = 20;
							lblLNameS.FontSize = 20;
							lblLoginS.FontSize = 20;
							lblPasswordS.FontSize = 20;
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
					string newQr = QR + " where [FNameS] like '%" + tbSearch.Text + "%' or " +
			   "[MNameS] like '%" + tbSearch.Text + "%' or " +
			   "[LNameS] like '%" + tbSearch.Text + "%' or" +
			   "[LoginS] like '%" + tbSearch.Text + "%' or" +
			   "[PasswordS] like '%" + tbSearch.Text + "%'";
					dgFill(newQr);
					break;
				case (false):
					foreach (DataRowView dataRow in (DataView)dgFormStaff.ItemsSource)
					{
						if (dataRow.Row.ItemArray[1].ToString() == tbSearch.Text)
						{
							dgFormStaff.SelectedItem = dataRow;
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
