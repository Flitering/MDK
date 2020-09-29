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
using System.Data.SqlClient;
using System.Data;
using Microsoft.Win32;
using System.Management;
using System.Runtime.InteropServices;
using NightCrow;

namespace NightCrow_WPF
{
	/// <summary>
	/// Логика взаимодействия для FormAutorization.xaml
	/// </summary>
	public partial class FormAutorization : Window
	{
		public FormAutorization()
		{
			switch (startup)
			{
				case true:
					InitializeComponent();
					tbEnterLogin.Clear();
					tbEnterPassword.Clear();
					ChangeSize((int)Width, (int)Height);
					break;

				case false:
					Close();
					break;

			}
			ChangeSize((int)Width, (int)Height);
		}

		bool startup = true;
		private void BtEnter_Click(object sender, RoutedEventArgs e)
		{
			DBProcedures procedures = new DBProcedures();
			Int32 ID_Record =
				procedures.Authorization(tbEnterLogin.Password.ToString(),
				tbEnterPassword.Password.ToString());
			switch (ID_Record)
			{
				case (0):
					tbEnterLogin.Clear();
					tbEnterPassword.Clear();
					MessageBox.Show("Данные не верны! " +
						"\n Повторите попытку ввода!", "NightCrow",
						MessageBoxButton.OK, MessageBoxImage.Warning);
					break;
				default:
					DBConnection.IDuser = ID_Record;
					Menu Menu = new Menu();
					Menu.Show();
					Visibility = Visibility.Collapsed;
					break;
			}
		}

		private void BtLeave_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void AuthorizForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			switch (MessageBox.Show("Покинуть приложение?", "NightCrow", MessageBoxButton.YesNo, MessageBoxImage.Question))
			{
				case MessageBoxResult.Yes:
					e.Cancel = false;
					break;
				case MessageBoxResult.No:
					e.Cancel = true;
					break;
			}
		}
		//private void SystemCheck()
		//{
		//	int Major = Environment.OSVersion.Version.Major;
		//	int Minor = Environment.OSVersion.Version.Minor;
		//	if ((Major >= 6) && (Minor >= 0))
		//	{
		//		RegistryKey registrySQL =
		//			Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server");
		//		RegistryKey registryNET =
		//			Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\.NETFramework");
		//		RegistryKey registryExcel =
		//			Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Office\Excel");
		//		RegistryKey registryWord =
		//			Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Office\16.0\Word");
		//		RegistryKey registryEdge =
		//			Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\MicrosoftEdge");
		//		RegistryKey registryChrome =
		//			Registry.CurrentUser.OpenSubKey(@"Software\Google\Chrome");
		//		RegistryKey registryExplorer =
		//		Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Internet Explorer");


		//		if (registrySQL == null)
		//		{
		//			MessageBox.Show("Запуск системы не возможен, в системе отсутствует Microsoft SQL Server", "Продажа товара");
		//			startup = false;
		//		}
		//		else if (registryNET == null)
		//		{
		//			MessageBox.Show("Запуск системы не возможен, в системе отсутствует .NETFramework", "Продажа товара");
		//			startup = false;
		//		}
		//		else if (registryExcel == null)
		//		{
		//			MessageBox.Show("Запуск системы не возможен, в системе отсутствует Microsoft Excel", "Продажа товара");
		//			startup = false;
		//		}

		//		else if (registryWord == null)
		//		{
		//			MessageBox.Show("Запуск системы не возможен, в системе отсутствует Microsoft Word", "Продажа товара");
		//			startup = false;
		//		}
		//		else if (registryEdge == null)
		//		{
		//			MessageBox.Show("Запуск системы не возможен, в системе отсутствует Microsoft Edge", "Продажа товара");
		//			startup = false;
		//		}
		//		else if (registryChrome == null)
		//		{
		//			MessageBox.Show("Запуск системы не возможен, в системе отсутствует брауер Google Chrome", "Продажа товара");
		//			startup = false;
		//		}
		//		else if (registryExplorer == null)
		//		{
		//			MessageBox.Show("Запуск системы не возможен, в системе отсутствует брауер Internet Explorer", "Продажа товара");
		//			startup = false;
		//		}


		//		else
		//		{
		//			try
		//			{
		//				DBConnection.connection.Open();
		//			}
		//			catch
		//			{
		//				MessageBox.Show("Не возможно подключиться к источнику данных", "Продажа товара");
		//				startup = false;
		//			}
		//			finally
		//			{
		//				DBConnection.connection.Close();
		//			}
		//		}

		//		RegistryKey freckey = Registry.LocalMachine;
		//		freckey = freckey.OpenSubKey(@"HARDWARE\DESCRIPTION\System\CentralProcessor\0", false);
		//		string str = freckey.GetValue("~MHz").ToString();
		//		//MessageBox.Show(String.Format("Частота Мгц:   {0}", str));
		//		if (Convert.ToInt32(str) <= 1000)
		//		{
		//			MessageBox.Show(String.Format("Данное приложение не запуститься с такой тактовой частотой:  {0}", str));
		//			startup = false;
		//		}


		//		GetSystemInfo();
		//	}
		//	else
		//	{
		//		MessageBox.Show("Данная операционная система не предназначена для запуска приложения!", "Продажа товара");
		//	}
		//}


		//[DllImport("wininet.dll")]
		//private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

		//public static bool IsConnectedToInternet()
		//{
		//	int Desc;
		//	return InternetGetConnectedState(out Desc, 0);
		//}
		//[DllImport("kernel32.dll")]
		//[return: MarshalAs(UnmanagedType.Bool)]
		//static extern bool GetPhysicallyInstalledSystemMemory(out long TotalMemoryInKylobytes);

		//void GetSystemInfo()
		//{
		//	long memKb;
		//	GetPhysicallyInstalledSystemMemory(out memKb);
		//	if ((memKb / 1024 / 1024) < 8)
		//	{
		//		MessageBox.Show(String.Format("Данное приложение не запустится с таким количеством RAM: ", memKb / 1024 / 1024));
		//		startup = false;
		//	}
		//	if (IsConnectedToInternet() == false)
		//	{
		//		MessageBox.Show(String.Format("Отсутствует подключение к интернету"));
		//		startup = false;
		//	}
		//}

		private void ChangeSize(int X, int Y)
		{

			if ((X >= 0 || Y >= 0) && (X < 800 || Y < 600))
			{

				lblEnterLogin.FontSize = 13;
				lblEnterPassword.FontSize = 13;
				tbEnterLogin.FontSize = 13;
				tbEnterPassword.FontSize = 13;
				btEnter.FontSize = 13;
				btLeave.FontSize = 13;
			}
			else
			{
				if ((X >= 800 || Y >= 600) && (X < 1280 || Y < 1024))
				{
					lblEnterLogin.FontSize = 15;
					lblEnterPassword.FontSize = 15;
					tbEnterLogin.FontSize = 15;
					tbEnterPassword.FontSize = 15;
					btEnter.FontSize = 15;
					btLeave.FontSize = 15;
				}
				else
				{
					if ((X >= 1280 || Y >= 1024) && (X < 1600 || Y < 900))
					{
						lblEnterLogin.FontSize = 18;
						lblEnterPassword.FontSize = 18;
						tbEnterLogin.FontSize = 18;
						tbEnterPassword.FontSize = 18;
						btEnter.FontSize = 18;
						btLeave.FontSize = 18;
					}
					else
					{
						if ((X >= 1920 || Y >= 1080))
						{
							lblEnterLogin.FontSize = 20;
							lblEnterPassword.FontSize = 20;
							tbEnterLogin.FontSize = 20;
							tbEnterPassword.FontSize = 20;
							btEnter.FontSize = 20;
							btLeave.FontSize = 20;
						}
					}
				}
			}
		}
	}
}
