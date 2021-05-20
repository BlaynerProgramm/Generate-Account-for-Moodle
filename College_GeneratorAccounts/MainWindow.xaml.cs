using College_GeneratorAccounts.Model;

using MahApps.Metro.Controls;

using Microsoft.Win32;

using System.Collections.Generic;
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
			OpenFileDialog ofd = new()
			{
				Filter = "csv(*.csv)|*.csv|xls(*.xls)|*.xls|xlsx(*.xlsx)|*.xlsx",
				Title = "Выбрать файл данных"
			};

			if (ofd.ShowDialog().Value)
			{
				try
				{
					data = ImportData.Import(ofd.FileName);
				}
				catch (System.IO.IOException ex)
				{
					MessageBox.Show(ex.Message);
				}
				for (int i = 0; i < data.Length; i++)
				{
					tb.Text += $"{data[i]}\n";
				}

				btGenerateAccount.IsEnabled = true;
			}
			else return;
		}

		private void BtExport_Click(object sender, RoutedEventArgs e)
		{
			SaveFileDialog sfd = new()
			{
				Filter = "csv(*.csv)|*.csv|xml(*.xml)|*.xml",
				Title = "Экспорт данных аккаунтов"
			};
			if (sfd.ShowDialog().Value)
			{
				ExportData.Export(accounts, sfd.FileName);
				MessageBox.Show("Успешное сохранение", "Экспорт", MessageBoxButton.OK, MessageBoxImage.Information);
			}
			else return;
		}

		private void BtSaveToDb_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("В разработке", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("В разработке", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
		}
	}
}