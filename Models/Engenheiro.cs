using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestaoObras.Models
{
    public class Engenheiro
    {
        public int EngenheiroId { get; set; }
        public string Nome { get; set; }
        public char Sexo { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }

        public ICollection<Obra> Obras { get; set; }

    }
}