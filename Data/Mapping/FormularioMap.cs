using Formularios.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Formularios.Data.Mapping
{
    public class FormularioMap : IEntityTypeConfiguration<Formulario>
    {
        public void Configure(EntityTypeBuilder<Formulario> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Titulo)
                .HasMaxLength(200)
                .IsRequired();

            builder.ToTable("Formulario");
        }
    }
}