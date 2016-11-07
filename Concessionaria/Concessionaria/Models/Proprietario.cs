using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Concessionaria.Models
{
    public class Proprietario
    {

        public Proprietario()
        {
            this.Carros = new HashSet<Carro>();
        }

        [Key]
        public int ProprietarioID { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string CPF { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode =true)]
        public DateTime DataNascimento { get; set; }

        public virtual ICollection<Carro> Carros { get; set; }
    }
}