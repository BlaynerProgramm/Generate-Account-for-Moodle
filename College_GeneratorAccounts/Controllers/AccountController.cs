using College_GeneratorAccounts.Model;

namespace College_GeneratorAccounts.Controllers
{
	public static class AccountController
	{
		public static string Add(Account[] accountArray, string ip, string userId, string password)
		{
			try
			{
				using DataBaseContext context = new(ip, userId, password);
				context.Accounts.AddRange(accountArray);
				context.SaveChanges();
				return "Сохранено";
			}
			catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException ex)
			{
				return ex.Message;
			}
			catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
			{
				return ex.Message;
			}
		}
	}
}
