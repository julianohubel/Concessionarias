using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;   

namespace Concessionaria.Models
{
    public class IncializarBanco : DropCreateDatabaseAlways<ConcessionariaContext>
    {
        protected override void Seed(ConcessionariaContext context)
        {
            //Criar dados 
            new List<Fabricante>
            {
                new Fabricante
                {
                    Nome = "Chevrolet",
                    PaisOrigem= "Alemanha",
                    Carros  =  new List<Carro>
                    {
                        new Carro
                        {
                            Nome = "Chevette",

                        }
                    }

                }
            }.ForEach(f => context.Fabricante.Add(f));         
            
            base.Seed(context);
        }
    }
}