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
        public Carro()
        {
            this.Proprietarios = new HashSet<Proprietario>();
        }

        [Key]        


        public int CarroID { get; set; }

        public int FabricanteID { get; set; }

        [Required(ErrorMessage="Obrigatório informar Nome")]
        [StringLength(30, ErrorMessage="Nome deve conter no maximo 30 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage="Ano é obrigatório")]            
         public int Ano { get; set; }


        public string Combustivel { get; set; }

        [ForeignKey("FabricanteID")]
        public Fabricante Fabricante { get; set; }

        public  virtual ICollection<Proprietario> Proprietarios { get; set; }

    }
}