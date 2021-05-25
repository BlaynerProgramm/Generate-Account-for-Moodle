using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace College_GeneratorAccounts.Services
{
	/// <summary>
	/// Экспорт данных
	/// </summary>
	public static class ExportData
	{
		/// <summary>
		/// Выполнить экспорт данных
		/// </summary>
		/// <param name="data">Коллекция аккаунтов, которую надо сохранить</param>
		/// <param name="path">Полный путь для экспорта</param>
		public static void Export(IEnumerable<Account> data, string path)
		{
			using StreamWriter stream = new(path, true, System.Text.Encoding.UTF8);

			if (path.EndsWith(".csv"))
			{
				ExportCsv(data, stream);
			}

			else
			{
				ExportXml(data, stream);
			}
		}

		/// <summary>
		/// Экспорт в xml
		/// </summary>
		/// <param name="data"></param>
		/// <param name="stream"></param>
		private static void ExportXml(IEnumerable<Account> data, StreamWriter stream)
		{
			foreach (Account account in data)
			{
				XmlSerializer formatter = new(typeof(Account));
				formatter.Serialize(stream, account);
			}
		}

		/// <summary>
		/// Экспорт в csv
		/// </summary>
		/// <param name="data"></param>
		/// <param name="stream"></param>
		private static void ExportCsv(IEnumerable<Account> data, StreamWriter stream)
		{
			stream.WriteLine("login;password;email;name;lname;cohort");
			foreach (Account account in data)
			{
				stream.Write(account.ToStringCsv());
			}
		}
	}
}