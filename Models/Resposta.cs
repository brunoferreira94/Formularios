using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Formularios.Models
{
    public class Resposta
    {
        public int Id { get; set; }

        [DisplayName("Resposta"), MaxLength(200), Required]
        public string Texto { get; set; }

        public DateTime DataCadastro { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        [DisplayName("Pergunta")]
        public int PerguntaId { get; set; }

        public Pergunta Pergunta { get; set; }
    }
}