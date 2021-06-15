using System;

using College_GeneratorAccounts.Services;

namespace College_GeneratorAccounts.Model
{
	public class Account
	{
		public Guid Id { get; set; }
		public string Username { get; init; }
		public string Email { get; init; }
		public string Password { get; init; }
		public string Firstname { get; init; }
		public string LastName { get; init; }
		public string Cohort1 { get; init; }

		/// <summary>
		/// Для сериализации в xml
		/// </summary>
		public Account() { }

		public Account(string login, string email, string password, string name, string lastName, string group)
		{
			Username = login ?? throw new ArgumentNullException(nameof(login));
			Email = email ?? throw new ArgumentNullException(nameof(email));
			Password = password ?? throw new ArgumentNullException(nameof(password));
			Firstname = name ?? throw new ArgumentNullException(nameof(name));
			LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
			Cohort1 = group ?? throw new ArgumentNullException(nameof(group));
		}

		/// <summary>
		/// Сгенерировать аккаунт
		/// </summary>
		/// <param name="info">Полное имя</param>
		/// <param name="fileName">Путь к файлу</param>
		/// <returns>Аккаунт</returns>
		public static Account GetnerateAccount(string info, string fileName)
		{
			string[] infoArray = info.Replace("  ", " ").Split();
			string nameToLatin = Generator.GetCollectionTranslitToLatin(info).Replace("  ", " ");
			string year = fileName.Split('_')[2].Remove(0,2);
			try
			{
				var login = Generator.GenerateLogin(nameToLatin, year);
				return new Account(login, Generator.GenerateEmails(login), $"{Generator.GeneratePassword()}#", infoArray[1], infoArray[0], $"{infoArray[3].Replace('_','-')}({year})");
			}
			catch (IndexOutOfRangeException ex)
			{
				throw new IndexOutOfRangeException(ex.Message);
			}
		}

		/// <summary>
		/// Возвращает строку, представляющую текущий объект для csv
		/// </summary>
		/// <returns> Cтроку, представляющую текущий объект для csv</returns>
		public string ToStringCsv() => $"{Username};{Password};{Firstname};{LastName};{Email};{Cohort1}\n";

		public override string ToString() => $"LastName: {LastName}\nName: {Firstname}\nLogin: {Username}\nPassword: {Password}\nEmail: {Email}\nGroup: {Cohort1}\n";
	}
}