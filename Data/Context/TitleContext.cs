using Data.Tables;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class TitleContext : DbContext
    {
        public DbSet<Title> Titles { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<TitleCredit> TitleCredits { get; set; }

        public TitleContext(DbContextOptions<TitleContext> options) : base(options) { }
    }
}
