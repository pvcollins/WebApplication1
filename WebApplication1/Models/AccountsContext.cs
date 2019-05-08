using System.Data.Entity;
namespace WebApplication1.Models
{
    public class AccountsContext : DbContext
    {
        public AccountsContext()
                : base("name=CleanerConn")
        {
        }
        public DbSet<Accounts> Accounts { get; set; }
    }
}