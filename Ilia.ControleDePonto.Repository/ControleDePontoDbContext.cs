using Ilia.ControleDePonto.Domain;
using Microsoft.EntityFrameworkCore;

namespace Ilia.ControleDePonto.Repository
{
    public class ControleDePontoDbContext : DbContext, IControleDePontoDbContext
    {
        public ControleDePontoDbContext()
        {
            Momentos.Add(new MomentoData { Data = new DateOnly(2018, 08, 20), Hora = new TimeOnly(8, 0) });
            Momentos.Add(new MomentoData { Data = new DateOnly(2018, 08, 20), Hora = new TimeOnly(12, 0) });
            Momentos.Add(new MomentoData { Data = new DateOnly(2018, 08, 20), Hora = new TimeOnly(13, 0) });
            Momentos.Add(new MomentoData { Data = new DateOnly(2018, 08, 20), Hora = new TimeOnly(17, 0) });
            Momentos.Add(new MomentoData { Data = new DateOnly(2018, 08, 21), Hora = new TimeOnly(8, 0) });
            Momentos.Add(new MomentoData { Data = new DateOnly(2018, 08, 21), Hora = new TimeOnly(12, 0) });
            Momentos.Add(new MomentoData { Data = new DateOnly(2018, 08, 21), Hora = new TimeOnly(13, 0) });
            Momentos.Add(new MomentoData { Data = new DateOnly(2018, 08, 21), Hora = new TimeOnly(17, 0) });
            SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase("ControleDePontoDB");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MomentoData>().HasKey(m => new { m.Data, m.Hora });
        }

        public DbSet<MomentoData> Momentos { get; set; }
    }
}