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

namespace NightCrow_WPF
{
	/// <summary>
	/// Логика взаимодействия для Menu.xaml
	/// </summary>
	public partial class Menu : Window
	{
		public Menu()
		{
			InitializeComponent();
			ChangeSize((int)Width, (int)Height);
		}

		private void btBase_Click(object sender, RoutedEventArgs e)
		{
			DBProcedures procedures = new DBProcedures();
			FormBase FormBase = new FormBase();
			FormBase.Show();
			Close();
		}

		private void btContract_Click(object sender, RoutedEventArgs e)
		{
			DBProcedures procedures = new DBProcedures();
			FormContract FormContract = new FormContract();
			FormContract.Show();
			Close();
		}

		private void btCustomer_Click(object sender, RoutedEventArgs e)
		{
			DBProcedures procedures = new DBProcedures();
			FormCustomers FormCustomers = new FormCustomers();
			FormCustomers.Show();
			Close();
		}

		private void btTypeOfService_Click(object sender, RoutedEventArgs e)
		{
			DBProcedures procedures = new DBProcedures();
			FormTypeOfService FormTypeOfService = new FormTypeOfService();
			FormTypeOfService.Show();
			Close();
		}

		private void btPosition_Click(object sender, RoutedEventArgs e)
		{
			DBProcedures procedures = new DBProcedures();
			FormPosition FormPosition = new FormPosition();
			FormPosition.Show();
			Close();
		}

		private void btService_Click(object sender, RoutedEventArgs e)
		{
			DBProcedures procedures = new DBProcedures();
			FormService FormService = new FormService();
			FormService.Show();
			Close();
		}

		private void btStaff_Click(object sender, RoutedEventArgs e)
		{
			DBProcedures procedures = new DBProcedures();
			FormStaff FormStaff = new FormStaff();
			FormStaff.Show();
			Close();
		}

		private void btTypeOfContract_Click(object sender, RoutedEventArgs e)
		{
			DBProcedures procedures = new DBProcedures();
			FormTypeOfContract FormTypeOfContract = new FormTypeOfContract();
			FormTypeOfContract.Show();
			Close();
		}
		private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			ChangeSize((int)e.NewSize.Width, (int)e.NewSize.Height);
		}
		private void ChangeSize(int X, int Y)
		{

			if ((X >= 0 || Y >= 0) && (X < 800 || Y < 600))
			{
				btBase.FontSize = 15;
				lblMenu.FontSize = 15;
				btStaff.FontSize = 15;
				btContract.FontSize = 15;
				btCustomer.FontSize = 15;
				btTypeOfService.FontSize = 15;
				btTypeOfContract.FontSize = 15;
				btPosition.FontSize = 15;
				btService.FontSize = 15;
			}
			else
			{
				if ((X >= 800 || Y >= 600) && (X < 1280 || Y < 1024))
				{
					btBase.FontSize = 18;
					lblMenu.FontSize = 18;
					btStaff.FontSize = 18;
					btContract.FontSize = 18;
					btCustomer.FontSize = 18;
					btTypeOfService.FontSize = 18;
					btTypeOfContract.FontSize = 18;
					btPosition.FontSize = 18;
					btService.FontSize = 18;
				}
				else
				{
					if ((X >= 1280 || Y >= 1024) && (X < 1600 || Y < 900))
					{
						btBase.FontSize = 20;
						lblMenu.FontSize = 20;
						btStaff.FontSize = 20;
						btContract.FontSize = 20;
						btCustomer.FontSize = 20;
						btTypeOfService.FontSize = 20;
						btTypeOfContract.FontSize = 20;
						btPosition.FontSize = 20;
						btService.FontSize = 20;
					}
					else
					{
						if ((X >= 1920 || Y >= 1080))
						{
							btBase.FontSize = 25;
							lblMenu.FontSize = 25;
							btStaff.FontSize = 25;
							btContract.FontSize = 25;
							btCustomer.FontSize = 25;
							btTypeOfService.FontSize = 25;
							btTypeOfContract.FontSize = 25;
							btPosition.FontSize = 25;
							btService.FontSize = 25;
						}
					}
				}
			}
		}
	}
}
