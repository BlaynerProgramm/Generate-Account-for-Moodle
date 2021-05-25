using Microsoft.Office.Interop.Excel;

using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace College_GeneratorAccounts.Model
{
	/// <summary>
	/// Электронная таблица
	/// </summary>
	public class Worksheet : IDisposable
	{
		/// <summary>
		/// Кол-во листов в таблице
		/// </summary>
		public int Count { get; init; }
		/// <summary>
		/// Путь к таблице
		/// </summary>
		public string Path { get; init; }

		#region Приватные переменные для таблицы
		private readonly Application objExcel;
		private readonly Workbook objWorkBook;
		private dynamic objWorkSheet;
		#endregion

		public Worksheet(string path)
		{
			Path = path ?? throw new ArgumentNullException(nameof(path));
			objExcel = new();
			objWorkBook = objExcel.Workbooks.Open(Path, 0, true, 5, "", "", false, XlPlatform.xlWindows, "", true, false, 0, true, false, false);
			Count = objWorkBook.Sheets.Count;
		}

		/// <summary>
		/// Получение данных с таблицы
		/// </summary>
		/// <param name="numPage">номер листа</param>
		/// <returns>Массив данных с таблицы</returns>
		public string[] GetSheet(int numPage = 1)
		{
			objWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)objWorkBook.Sheets[numPage]; // Выбор листа
			dynamic group = objWorkSheet.Cells[1, 1].Text.ToString(); // Считывание группы
			if (group == "")
			{
				group = objWorkSheet.Cells[1, 2].Text.ToString(); //Погрешность
			}
			dynamic numberOfPeople = objWorkSheet.Cells[3, 5].Text.ToString(); //Считывание кол-во человек в группе
			string[] data = new string[Convert.ToInt32(numberOfPeople)];
			int row = 5;
			int column;

			//Считывание данных из таблицы
			for (int i = 0; i < data.Length; i++)
			{
				column = 4;
				for (int j = 0; j < 3; j++)
				{
					data[i] += $"{objWorkSheet.Cells[row, column++].Text} ";
				}
				data[i] += $"{group} ";
				row++;
			}

			return data.Where(x => char.IsLetter(x.ToCharArray()[0])).ToArray();
		}

		/// <summary>
		/// Получение данных с таблицы асинхронно 
		/// </summary>
		/// <param name="numPage">Номер страницы</param>
		/// <returns>Массив данных с таблицы</returns>
		public async Task<string[]> GetSheetAsync(int numPage = 1) => await Task.Run(() => GetSheet(numPage));

		public void Dispose()
		{
			objExcel.Quit();
			Marshal.ReleaseComObject(objWorkBook);
			Marshal.ReleaseComObject(objWorkSheet);
			Marshal.ReleaseComObject(objExcel);
			GC.SuppressFinalize(this);
		}
	}
}