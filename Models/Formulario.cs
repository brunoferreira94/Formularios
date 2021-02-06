using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Formularios.Models
{
    public class Formulario
    {
        public int Id { get; set; }

        [MaxLength(200), Required]
        public string Titulo { get; set; }

        public List<Pergunta> Perguntas { get; set; }
    }
}