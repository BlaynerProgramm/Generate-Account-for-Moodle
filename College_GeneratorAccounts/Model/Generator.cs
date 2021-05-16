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
			//TODO: Написать траслитор
			return fullName;
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

		public static string GeneratePassword()
		{
			return string.Empty;
		}
	}
}