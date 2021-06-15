using College_GeneratorAccounts.Model;

using System.Windows;

namespace College_GeneratorAccounts.Windows
{
	/// <summary>
	/// Interaction logic for SaveToDbWindow.xaml
	/// </summary>
	public partial class SaveToDbWindow : Window
	{
		public Account[] AccountArray { get; init; }

		public SaveToDbWindow(Account[] accounts)
		{
			InitializeComponent();
			AccountArray = accounts;
		}

		private void BtSave_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show(Controllers.AccountController.Add(AccountArray, tbIp.Text, tbUserId.Text, tbPassword.Text), "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
		}
	}
}
