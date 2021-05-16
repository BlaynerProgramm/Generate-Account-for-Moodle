using College_GeneratorAccounts.Model;

using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace College_GeneratorAccounts
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private string[] data;
		private readonly List<Account> accounts = new();
		public MainWindow()
		{
			InitializeComponent();
		}

		private void BtGenerateAccount_Click(object sender, RoutedEventArgs e)
		{
			for (int i = 0; i < data.Length; i++)
			{
				accounts.Add(Account.GetnerateAccount(data[i]));
			}
			tb.Text = "";
			foreach (Account account in accounts)
			{
				tb.Text += account;
			}
		}

		private void BtImport_Click(object sender, RoutedEventArgs e)
		{
			data = ImportData.GetData();
			for (int i = 0; i < data.Length; i++)
			{
				tb.Text += data[i];
			}
		}

		private void BtExport_Click(object sender, RoutedEventArgs e)
		{
			
			MessageBox.Show("Ok");
		}
	}
}

