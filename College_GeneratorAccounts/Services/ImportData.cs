using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace College_GeneratorAccounts.Services
{
	/// <summary>
	/// Импорт данных
	/// </summary>
	public static class ImportData
	{
		/// <summary>
		/// Асинхронный метод для импорта csv
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public static async Task<string[]> ImportCsvAsync(string path) => await Task.Run(() => ImportCsv(path));

		/// <summary>
		/// Импорт из csv
		/// </summary>
		/// <param name="ofd"></param>
		/// <returns></returns>
		private static string[] ImportCsv(string path)
		{
			try
			{
				using StreamReader stream = new(path, System.Text.Encoding.UTF8);
				string data = stream.ReadToEnd();

				List<string> dataList = data.Split('\n').ToList();
				dataList.RemoveRange(0, 1);

				for (int i = 0; i < dataList.Count; i++)
				{
					dataList[i] = dataList[i].Replace(';', ' ');
				}
				return dataList.ToArray();
			}
			catch (IOException)
			{
				throw new IOException("Файл уже занят каким-то процессом");
			}
		}
	}
}