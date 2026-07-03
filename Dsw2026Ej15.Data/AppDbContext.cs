using Dsw2026Ej15.Domain;
using Microsoft.EntityFrameworkCore;

namespace Dsw2026Ej15.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Definimos las tablas para médicos y especialidades en la BD
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Speciality> Specialities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Mapeamos las claves primarias heredadas de BaseEntity
            modelBuilder.Entity<Doctor>().HasKey(d => d.Id);
            modelBuilder.Entity<Speciality>().HasKey(s => s.Id);

            // Configuramos la relación: Un Doctor tiene una Speciality obligatoria
            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Speciality)
                .WithMany()
                .IsRequired();
        }
    }
}