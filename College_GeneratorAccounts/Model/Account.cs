using System;

namespace College_GeneratorAccounts.Model
{
	internal class Account
	{
		public Account(string login, string email, string password)
		{
			Login = login ?? throw new ArgumentNullException(nameof(login));
			Email = email ?? throw new ArgumentNullException(nameof(email));
			Password = password ?? throw new ArgumentNullException(nameof(password));
		}

		public string Login { get; init; }
		public string Email { get; init; }
		public string Password { get; init; }

		public static Account GetnerateAccount(string fullName)
		{
			string nameToLatin = Generator.GetCollectionTranslitToLatin(fullName);
			return new Account(Generator.GenerateLogin(nameToLatin), Generator.GenerateEmails(nameToLatin), Generator.GeneratePassword());
		}

		public string ToStringCsv() => $"{Login} {Email} {Password};";


		public override string ToString() => $"Login: {Login}\nEmail: {Email}\nPassword: {Password}\n";
	}
}