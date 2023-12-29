
using Microsoft.EntityFrameworkCore;

namespace Tangy_DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=.\\SqlExpress; Database=TestBlazor67;" +
                "Trusted_connection=true; TrustServerCertificate=true");
        }

        public DbSet<Category> Categories { get; set; }
    }
}
