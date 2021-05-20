using System;

namespace College_GeneratorAccounts.Model
{
	public class Account
	{
		public string Login { get; init; }
		public string Email { get; init; }
		public string Password { get; init; }
		public string Name { get; init; }
		public string LastName { get; init; }
		public string Group { get; init; }

		public Account() { }

		public Account(string login, string email, string password, string name, string lastName, string group)
		{
			Login = login ?? throw new ArgumentNullException(nameof(login));
			Email = email ?? throw new ArgumentNullException(nameof(email));
			Password = password ?? throw new ArgumentNullException(nameof(password));
			Name = name ?? throw new ArgumentNullException(nameof(name));
			LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
			Group = group ?? throw new ArgumentNullException(nameof(group));
		}

		/// <summary>
		/// Сгенерировать аккаунт
		/// </summary>
		/// <param name="info">Полное имя</param>
		/// <returns>Аккаунт</returns>
		public static Account GetnerateAccount(string info)
		{
			string[] infoArray = info.Replace("  ", " ").Split();
			string nameToLatin = Generator.GetCollectionTranslitToLatin(info).Replace("  ", " ");
			return new Account(Generator.GenerateLogin(nameToLatin), Generator.GenerateEmails(nameToLatin), Generator.GeneratePassword(), infoArray[1], infoArray[0], infoArray[3]);
		}

		/// <summary>
		/// Возвращает строку, представляющую текущий объект для csv
		/// </summary>
		/// <returns> Cтроку, представляющую текущий объект для csv</returns>
		public string ToStringCsv() => $"{Login};{Password};{Email};{Name};{LastName};{Group}\n";

		public override string ToString() => $"Login: {Login}\nPassword: {Password}\nEmail: {Email}\n";
	}
}