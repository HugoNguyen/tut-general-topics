using CodingWiki_Model.Models;
using Microsoft.EntityFrameworkCore;

namespace CodingWiki_DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB;Database=CodingWiki;TrustServerCertificate=True;Trusted_Connection=True;";
        }
    }
}
