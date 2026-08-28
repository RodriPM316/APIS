using CatalogosEmpleados.Entidades;
using Microsoft.EntityFrameworkCore;

namespace CatalogosEmpleados.Datos
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options) 
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Empleados> Empleados { get; set; }
        public DbSet<Departamentos> Departamentos { get; set; }
        public DbSet<EdoCivil> EdoCivil { get; set; }
        public DbSet<Puestos> Puestos { get; set; }
        public DbSet<Sexo> Sexo { get; set; }
        public DbSet<Turnos> Turnos { get; set; }
        public DbSet<Correos> Correos { get; set; }
        public DbSet<Areas> Areas { get; set; }
        public DbSet<TipoCorreo> TipoCorreo { get; set; }
        public DbSet<Employees> Employees { get; set; }
    }
}
