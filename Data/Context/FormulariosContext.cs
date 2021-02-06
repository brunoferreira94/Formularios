using Formularios.Models;
using Microsoft.EntityFrameworkCore;

namespace Formularios.Data
{
    public class FormulariosContext : DbContext
    {
        public FormulariosContext(DbContextOptions<FormulariosContext> options) : base(options)
        {
        }

        public DbSet<Formulario> Formularios { get; set; }
        public DbSet<Pergunta> Perguntas { get; set; }
        public DbSet<Resposta> Respostas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FormulariosContext).Assembly);
        }
    }
}