using Microsoft.EntityFrameworkCore;
using MülakatProje.Models;


namespace MülakatProje.Context
{
    public class VeritabaniContext : DbContext
    {
        public VeritabaniContext(DbContextOptions<VeritabaniContext> options) : base(options) { }

        public DbSet<Sanatci>? Sanatcilar { get; set; }
        public DbSet<Album> Albumler { get; set; }
        public DbSet<Sarki> Sarkilar { get; set; }
        public DbSet<CalmaListesi> CalmaListeleri { get; set; }
        public DbSet<CalmaListesiSarkilari> CalmaListesiSarkilari { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=MuzikVeritabani;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

    }
}
