using Formularios.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Formularios.Data.Mapping
{
    public class RespostaMap : IEntityTypeConfiguration<Resposta>
    {
        public void Configure(EntityTypeBuilder<Resposta> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Texto)
                .HasMaxLength(200)
                .IsRequired();

            builder.HasOne(t => t.Pergunta)
                .WithMany(tt => tt.Respostas)
                .HasForeignKey(t => t.PerguntaId)
                .HasConstraintName("PerguntaId")
                .IsRequired();

            builder.ToTable("Resposta");
        }
    }
}