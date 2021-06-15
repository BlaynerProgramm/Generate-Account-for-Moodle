using College_GeneratorAccounts.Model;

using Microsoft.EntityFrameworkCore;

using System;

namespace College_GeneratorAccounts
{
	public class DataBaseContext : DbContext
	{
		public string Ip { get; init; }
		public string UserId { get; init; }
		public string Password { get; init; }

		public DataBaseContext(string ip, string userId, string password)
		{
			Ip = ip ?? throw new ArgumentNullException(nameof(ip));
			UserId = userId;
			Password = password;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)=>
			optionsBuilder.UseSqlServer($@"Server={Ip}; Database=Accounts_Students_Moodle; User Id={UserId}; Password={Password}; Trusted_Connection=True;");

		public DbSet<Account> Accounts { get; set; }
	}
}
