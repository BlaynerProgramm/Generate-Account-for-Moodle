using Microsoft.Win32;

using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace College_GeneratorAccounts.Model
{
	/// <summary>
	/// Экспорт данных
	/// </summary>
	public static class ExportData
	{
		public static void Export(IEnumerable<Account> data)
		{
			SaveFileDialog sfd = new()
			{
				Filter = "csv(*.csv)|*.csv|xml(*.xml)|*.xml",
				Title = "Экспорт данных аккаунтов"
			};
			sfd.ShowDialog();

			try
			{
				using StreamWriter stream = new(sfd.FileName);

				if (sfd.FileName.EndsWith(".csv"))
				{
					ExportCsv(data, stream);
				}

				else
				{
					ExportXml(data, stream);
				}
			}
			catch (System.ArgumentException)
			{
				throw new System.ArgumentException("Путь не выбран");
			}
		}

		private static void ExportXml(IEnumerable<Account> data, StreamWriter stream)
		{
			foreach (Account account in data)
			{
				XmlSerializer formatter = new(typeof(Account));
				formatter.Serialize(stream, account);
			}
		}

		private static void ExportCsv(IEnumerable<Account> data, StreamWriter stream)
		{
			stream.WriteLine("login;email;password;");
			foreach (Account account in data)
			{
				stream.Write(account.ToStringCsv());
			}
		}
	}
}
