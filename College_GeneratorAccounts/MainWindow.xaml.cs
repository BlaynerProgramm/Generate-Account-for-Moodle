using College_GeneratorAccounts.Model;

using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace College_GeneratorAccounts
{
	public partial class MainWindow : Window
	{
		/// <summary>
		/// Исходные данные с именами
		/// </summary>
		private string[] data;
		/// <summary>
		/// Коллекция готовых аккаунтов
		/// </summary>
		private List<Account> accounts;

		public MainWindow() => InitializeComponent();


		private void BtGenerateAccount_Click(object sender, RoutedEventArgs e)
		{
			accounts = new();
			for (int i = 0; i < data.Length; i++)
			{
				accounts.Add(Account.GetnerateAccount(data[i]));
			}
			tb.Text = string.Empty;
			foreach (Account account in accounts)
			{
				tb.Text += account;
			}

			btExport.IsEnabled = true;
			btSaveToDb.IsEnabled = true;
		}

		private void BtImport_Click(object sender, RoutedEventArgs e)
		{
			tb.Text = string.Empty;
			try
			{
				data = ImportData.GetData();
			}
			catch (FileNotFoundException ex)
			{
				MessageBox.Show(ex.Message);
			}
			for (int i = 0; i < data.Length; i++)
			{
				tb.Text += $"{data[i]}\n";
			}
			btGenerateAccount.IsEnabled = true;
		}

		private void BtExport_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				ExportData.Export(accounts);
				MessageBox.Show("Успешное сохранение", "Экспорт", MessageBoxButton.OK, MessageBoxImage.Information);
			}
			catch (System.ArgumentException ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void BtSaveToDb_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("В разработке", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
		}
	}
}