using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Concessionaria.Models
{
    public class Fabricante
    {
        [Key]
        public int FabricanteID { get; set; }
        [Required(ErrorMessage = "Nome é Obrigatorio!")]
       
        public string Nome { get; set; }
        [Required(ErrorMessage ="Pais é Obrigatório!")]
        public string PaisOrigem { get; set; }

        public virtual ICollection<Carro> Carros { get; set; }
    }
}