using Formularios.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Formularios.Data.Mapping
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(t => t.Id);

            //builder.HasMany(t => t.Perguntas)
            //    .WithOne(tt => tt.Usuario)
            //    .HasForeignKey(tt => tt.UsuarioId)
            //    .HasConstraintName("UsuarioId");

            builder.Property(t => t.Apelido)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(t => t.Senha)
                .HasMaxLength(10)
                .IsRequired();

            builder.ToTable("Usuario");
        }
    }
}