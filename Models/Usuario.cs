using System.Collections.Generic;

namespace Formularios.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Apelido { get; set; }
        public string Senha { get; set; }

        public IEnumerable<Pergunta> Perguntas { get; set; }
    }
}