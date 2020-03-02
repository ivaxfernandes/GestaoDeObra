using System;
using System.ComponentModel.DataAnnotations;

namespace GestaoObras.Models
{
    public class Obra
    {
        public int ObraId { get; set; }
        public int ClienteId { get; set; }
        public int EngenheiroId { get; set; }
        public string Nome { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataInicio { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataFim { get; set; }
        public decimal Valor { get; set; }


        public Cliente Cliente { get; set; }
        public Engenheiro Engenheiro { get; set; }

    }
}