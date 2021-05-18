using Microsoft.Win32;

using System.IO;

namespace College_GeneratorAccounts.Model
{
	/// <summary>
	/// Импорт данных
	/// </summary>
	public static class ImportData
	{
		public static string[] GetData()
		{
			OpenFileDialog ofd = new() 
			{
				Filter = "csv(*.csv)|*.csv|xls(*.xls)|*.xls",
				Title = "Выбрать файл данных" 
			};

			if ((bool)ofd.ShowDialog())
			{
				if (ofd.FileName.EndsWith(".csv"))
				{
					using StreamReader stream = new(ofd.FileName);
					string data = stream.ReadToEnd();
					return data.Split(';');
				}

				else throw new System.Exception("Этот тип файлов временно не поддерживается");
			}
			throw new FileNotFoundException("Выберите файл");
		}
	}
}