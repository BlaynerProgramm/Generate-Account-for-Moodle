using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;

using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace College_GeneratorAccounts.Model
{
	/// <summary>
	/// Импорт данных
	/// </summary>
	public static class ImportData
	{
		/// <summary>
		/// Открывает окно диалога для выбора файла импорта
		/// </summary>
		/// <returns>массив данных</returns>
		public static string[] Import()
		{
			OpenFileDialog ofd = new()
			{
				Filter = "csv(*.csv)|*.csv|xls(*.xls)|*.xls|xlsx(*.xlsx)|*.xlsx",
				Title = "Выбрать файл данных"
			};

			if ((bool)ofd.ShowDialog())
			{
				if (ofd.FileName.EndsWith(".csv"))
				{
					return ImportCsv(ofd);
				}

				else
				{
					return ImportSheet(ofd.FileName);
				}
			}
			throw new FileNotFoundException("Выберите файл");
		}

		/// <summary>
		/// Импорт из csv
		/// </summary>
		/// <param name="ofd"></param>
		/// <returns></returns>
		private static string[] ImportCsv(OpenFileDialog ofd)
		{
			using StreamReader stream = new(ofd.FileName);
			string data = stream.ReadToEnd();
			return data.Split(';');
		}

		/// <summary>
		/// Импорт таблиц
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public static string[] ImportSheet(string path)
		{
			Application ObjExcel = new();
			Workbook ObjWorkBook = ObjExcel.Workbooks.Open(path, 0, true, 5, "", "", false, XlPlatform.xlWindows, "", true, false, 0, true, false, false);
			dynamic ObjWorkSheet = (Worksheet)ObjWorkBook.Sheets[1];
			string[] data = new string[23];

			int row = 5;
			for (int i = 0; i < 23; i++)
			{
				int cloum = 4;
				for (int j = 0; j < 3; j++)
				{
					data[i] += ObjWorkSheet.Cells[row, cloum++].Text.ToString() + " ";
				}
				row++;
			}
			ObjExcel.Quit();
			Marshal.ReleaseComObject(ObjWorkBook);
			Marshal.ReleaseComObject(ObjWorkSheet);
			Marshal.ReleaseComObject(ObjExcel);
			return data.Where(x => char.IsLetter(x.ToCharArray()[0])).ToArray();
		}
	}
}