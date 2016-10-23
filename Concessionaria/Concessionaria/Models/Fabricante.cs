using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Concessionaria.Models
{
    public class Fabricante
    {
        [Key]
        public int FabricanteID { get; set; }

        public string Nome { get; set; }

        public string PaisOrigem { get; set; }

        public virtual ICollection<Carro> Carros { get; set; }
    }
}