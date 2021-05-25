using College_GeneratorAccounts.Services;
using College_GeneratorAccounts.Model;

using MahApps.Metro.Controls;

using Microsoft.Win32;

using System.Collections.Generic;
using System.Windows;

namespace College_GeneratorAccounts
{
	public partial class MainWindow : MetroWindow
	{
		#region Общие переменные
		/// <summary>
		/// Исходные данные с именами
		/// </summary>
		private string[] data;
		/// <summary>
		/// Коллекция готовых аккаунтов
		/// </summary>
		private readonly List<Account> accounts = new(); 
		/// <summary>
		/// Счётчик страниц
		/// </summary>
		private int i = 1;

		private delegate void Operation(int page);

		/// <summary>
		///  Событие перелистования страниц в таблице
		/// </summary>
		private event Operation TurnThePage;
		/// <summary>
		/// Электронная таблица
		/// </summary>
		private Worksheet worksheet;
		#endregion

		public MainWindow()
		{
			InitializeComponent();
			TurnThePage += TurnPage;
		}

		private void TurnPage(int i)
		{
			try
			{
				data = worksheet.GetSheet(i);
				tb.Text = string.Empty;
				for (int j = 0; j < data.Length; j++)
				{
					tb.Text += $"{data[j]}\n";
				}
				lbPages.Content = $"{i} из {worksheet.Count}";
			}
			catch (System.Runtime.InteropServices.COMException ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void BtGenerateAccount_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				for (int i = 0; i < data.Length; i++)
				{
					accounts.Add(Account.GetnerateAccount(data[i]));
				}
				tb.Text = string.Empty;
				foreach(var account in accounts)
				{
					tb.Text += account;
				}

				btExport.IsEnabled = true;
				btSaveToDb.IsEnabled = true;
			}
			catch (System.IndexOutOfRangeException ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private async void BtImport_Click(object sender, RoutedEventArgs e)
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
					if (ofd.FileName.EndsWith(".csv"))
					{
						data = await ImportData.ImportCsvAsync(ofd.FileName);
					}
					else
					{
						worksheet = new(ofd.FileName);
						data = await worksheet.GetSheetAsync();
						btGenerateAccountForAll.Visibility = Visibility.Visible;
					}	
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
			else
			{
				return;
			}
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
				lbPages.Content = string.Empty;
				worksheet.Dispose();
				tb.Text = string.Empty;
				i = 1;
			}
			else
			{
				return;
			}
		}

		private void BtSaveToDb_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("В разработке", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("В разработке", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
		}

		private void BtBack_Click(object sender, RoutedEventArgs e) => TurnThePage?.Invoke(--i);

		private void BtNext_Click(object sender, RoutedEventArgs e) => TurnThePage?.Invoke(++i);

		private void BtGenerateAccountForAll_Click(object sender, RoutedEventArgs e)
		{
			BtGenerateAccount_Click(sender, e); // Вызываем генерацию у первой страницы
			for (int j = 0; j < worksheet.Count - 1; j++)
			{
				TurnThePage?.Invoke(++i);
				BtGenerateAccount_Click(sender, e);
			}
			MessageBox.Show("Можно экспортировать", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
		}
	}
}