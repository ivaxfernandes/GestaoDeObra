using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace GestaoObras.Models
{
    public class Cliente
    {
        public int ClienteId { get; set; }
        public string Nome { get; set; }
        public char Sexo { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }

        public ICollection<Obra> Obras { get; set; }

    }
}
