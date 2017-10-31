using Microsoft.EntityFrameworkCore;

namespace test
{
    class WokerContext :DbContext
    {
        public DbSet<Woker> Wokers { get; set; }
        public DbSet<Accaunt> Accaunts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=DB.db");
        }

    }
}
