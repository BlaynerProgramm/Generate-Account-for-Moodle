using College_GeneratorAccounts.Model;

using MahApps.Metro.Controls;

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace College_GeneratorAccounts
{
	public partial class MainWindow : MetroWindow
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
				data = ImportData.Import();
				for (int i = 0; i < data.Length; i++)
				{
					tb.Text += $"{data[i]}\n";
				}
				btGenerateAccount.IsEnabled = true;
			}
			catch (FileNotFoundException ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void BtExport_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				ExportData.Export(accounts);
				MessageBox.Show("Успешное сохранение", "Экспорт", MessageBoxButton.OK, MessageBoxImage.Information);
			}
			catch (ArgumentException ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void BtSaveToDb_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("В разработке", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("В разработке");
		}
	}
}