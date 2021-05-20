using System;
using System.Collections.Generic;
using System.Text;

namespace College_GeneratorAccounts.Model
{
	/// <summary>
	/// Генератор аккаунтов
	/// </summary>
	public static class Generator
	{
		/// <summary>
		/// Переводит транслитом из кириллицы в латиницу
		/// </summary>
		/// <param name="fullName">Коллекция, которую нужно перевести</param>
		/// <returns>Переведённую коллекцию</returns>
		public static string GetCollectionTranslitToLatin(string fullName)
		{
			Dictionary<string, string> dictionaryChar = new()
			{
				{ "а", "a" },
				{ "б", "b" },
				{ "в", "v" },
				{ "г", "g" },
				{ "д", "d" },
				{ "е", "e" },
				{ "ё", "yo" },
				{ "ж", "zh" },
				{ "з", "z" },
				{ "и", "i" },
				{ "й", "y" },
				{ "к", "k" },
				{ "л", "l" },
				{ "м", "m" },
				{ "н", "n" },
				{ "о", "o" },
				{ "п", "p" },
				{ "р", "r" },
				{ "с", "s" },
				{ "т", "t" },
				{ "у", "u" },
				{ "ф", "f" },
				{ "х", "h" },
				{ "ц", "ts" },
				{ "ч", "ch" },
				{ "ш", "sh" },
				{ "щ", "sch" },
				{ "ъ", "'" },
				{ "ы", "yi" },
				{ "ь", "" },
				{ "э", "e" },
				{ "ю", "yu" },
				{ "я", "ya" }
			};
			StringBuilder sb = new();

			foreach (char ch in fullName.ToLower())
			{
				// берём каждый символ строки и проверяем его на нахождение его в словаре для замены
				if (dictionaryChar.TryGetValue(ch.ToString(), out string ss))
				{
					sb.Append(ss);
				}
				// иначе добавляем тот же символ
				else
				{
					sb.Append(ch);
				}
			}
			return sb.ToString();
		}

		/// <summary>
		/// Генерация почты
		/// </summary>
		/// <param name="fullName">Полное имя на латинице</param>
		/// <returns>Почта</returns>
		public static string GenerateEmails(string fullName)
		{
			string[] fullNameArray = fullName.Split();
			return $"{fullNameArray[0]}.{fullNameArray[1]}@moodle.edu";
		}

		/// <summary>
		/// Генерация логина
		/// </summary>
		/// <param name="nameLatin">Полное имя на латинском</param>
		/// <returns>Логин</returns>
		public static string GenerateLogin(string nameLatin)
		{
			string[] nameLatinArray = nameLatin.Split();
			return $"{nameLatinArray[0]}_{nameLatinArray[1]}";
		}

		/// <summary>
		/// Генератор паролей
		/// </summary>
		/// <returns>Пароль</returns>
		public static string GeneratePassword()
		{
			char[] symbols =
			   {
				'B', 'C', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'V', 'W', 'X', 'Y', 'Z', 'A', 'E', 'I', 'O', 'U', 'Y',
				'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
				'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
				'!', '#', '$', '*'
				};
			Random random = new();
			StringBuilder sb = new();

			for (int i = 0; i < 15; i++)
			{
				sb.Append(symbols[random.Next(0, symbols.Length)]);
			}

			return sb.ToString();
		}
	}
}