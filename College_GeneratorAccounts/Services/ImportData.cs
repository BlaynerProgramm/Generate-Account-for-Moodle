using Microsoft.Office.Interop.Excel;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace College_GeneratorAccounts.Services
{
	/// <summary>
	/// Импорт данных
	/// </summary>
	public static class ImportData
	{
		/// <summary>
		/// Выполнить импорт
		/// </summary>
		/// <param name="path">Полный путь к файлу данных</param>
		/// <returns></returns>
		public async static Task<string[]> ImportAsync(string path)
		{
			if (path.EndsWith(".csv"))
			{
				return await Task.Run(() => ImportCsv(path));
			}

			else
			{
				return await Task.Run(() => ImportSheet(path, out _));
			}
		}

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

		/// <summary>
		/// Импорт таблиц
		/// </summary>
		/// <param name="path">Полный путь</param>
		/// <param name="cellsCount">Кол-во столбцов в таблице</param>
		/// <param name="numPage">Номер страницы в файле</param>
		/// <returns>Массив данных</returns>
		public static string[] ImportSheet(string path, out int cellsCount, int numPage = 1)
		{
			Application ObjExcel = new();
			Workbook ObjWorkBook = ObjExcel.Workbooks.Open(path, 0, true, 5, "", "", false, XlPlatform.xlWindows, "", true, false, 0, true, false, false);
			dynamic ObjWorkSheet = (Worksheet)ObjWorkBook.Sheets[numPage];
			dynamic group = ObjWorkSheet.Cells[1, 1].Text.ToString(); // Считывание группы
			cellsCount = ObjWorkBook.Sheets.Count;
			if (group == "")
			{
				group = ObjWorkSheet.Cells[1, 2].Text.ToString(); //Погрешность
			}
			dynamic numberOfPeople = ObjWorkSheet.Cells[3, 5].Text.ToString(); //Считывание кол-во человек в группе
			string[] data = new string[Convert.ToInt32(numberOfPeople)];
			int row = 5;
			int column;

			//Считывание данных из таблицы
			for (int i = 0; i < data.Length; i++)
			{
				column = 4;
				for (int j = 0; j < 3; j++)
				{
					data[i] += $"{ObjWorkSheet.Cells[row, column++].Text} ";
				}
				data[i] += $"{group} ";
				row++;
			}

			#region Освобождение памяти
			ObjExcel.Quit();
			Marshal.ReleaseComObject(ObjWorkBook);
			Marshal.ReleaseComObject(ObjWorkSheet);
			Marshal.ReleaseComObject(ObjExcel);
			#endregion

			return data.Where(x => char.IsLetter(x.ToCharArray()[0])).ToArray();
		}
	}
}