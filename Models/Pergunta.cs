using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Formularios.Models
{
    public class Pergunta
    {
        public int Id { get; set; }

        [DisplayName("Pergunta"), MaxLength(200), Required]
        public string Titulo { get; set; }

        public DateTime DataCadastro { get; set; }

        [DisplayName("Usuário")]
        public int UsuarioId { get; set; }

        [DisplayName("Formulário")]
        public int FormularioId { get; set; }

        public Usuario Usuario { get; set; }

        public Formulario Formulario { get; set; }

        public List<Resposta> Respostas { get; set; }
    }
}