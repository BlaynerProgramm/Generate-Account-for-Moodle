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
				foreach (Account account in data)
				{
					if (sfd.FileName.EndsWith(".csv"))
					{
						stream.Write(account.ToStringCsv());
					}
					else if (sfd.FileName.EndsWith(".xml"))
					{
						XmlSerializer formatter = new(typeof(Account));
						formatter.Serialize(stream, account);
					}
				}
			}
			catch (System.ArgumentException)
			{
				throw new System.ArgumentException("Путь не выбран");
			}
		}
	}
}
