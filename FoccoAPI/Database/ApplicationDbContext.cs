using FoccoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FoccoAPI.Database
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }

        public DbSet<TransactionsModel> Transactions { get; set; }

    }
}
