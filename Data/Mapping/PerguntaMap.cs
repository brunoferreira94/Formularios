using Formularios.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Formularios.Data.Mapping
{
    public class PerguntaMap : IEntityTypeConfiguration<Pergunta>
    {
        public void Configure(EntityTypeBuilder<Pergunta> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Titulo)
                .HasMaxLength(200)
                .IsRequired();

            builder.HasOne(t => t.Usuario)
                .WithMany(tt => tt.Perguntas)
                .HasForeignKey(t => t.UsuarioId)
                .HasConstraintName("UsuarioId")
                .IsRequired();

            builder.HasOne(t => t.Formulario)
                .WithMany(tt => tt.Perguntas)
                .HasForeignKey(t => t.FormularioId)
                .HasConstraintName("FormularioId")
                .IsRequired();

            builder.ToTable("Pergunta");
        }
    }
}