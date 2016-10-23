using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Concessionaria.Models
{
    public class Carro
    {
        [Key]
        public int CarroID { get; set; }

        public int FabricanteID { get; set; }

        public string Nome { get; set; }

        public int Ano { get; set; }

        
        public string Combustivel { get; set; }

        [ForeignKey("FabricanteID")]
        public Fabricante Fabricante { get; set; }
        
    }
}