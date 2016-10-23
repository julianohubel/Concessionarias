using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Concessionaria.Models
{
    public class ConcessionariaContext : DbContext
    {

       // public ConcessionariaContext() : base("name=ConcessionariaContext")
        //{
            
          //  Database.Connection.ConnectionString = @"data source=HUBEL\SQLEXPRESS; initial catalog=Concessionarias; User Id=sa; Password=sa;";
        //}

        public DbSet<Fabricante> Fabricante { get; set; }
        public DbSet<Carro> Carro { get; set; }
    }
}