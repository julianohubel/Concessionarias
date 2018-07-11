using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Concessionaria.Models
{
    public class ConcessionariaContext : DbContext
    {

        // public ConcessionariaContext() : base("name=ConcessionariaContext")
        //{

        //  Database.Connection.ConnectionString = @"data source=HUBEL\SQLEXPRESS; initial catalog=Concessionarias; User Id=sa; Password=sa;";
        //}

        public ConcessionariaContext() : base("ConcessionariaContext") {  }
        
        
        public DbSet<Fabricante> Fabricante { get; set; }
        public DbSet<Carro> Carro { get; set; }        

        public System.Data.Entity.DbSet<Concessionaria.Models.Proprietario> Proprietarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Carro>()
                .HasMany(p => p.Proprietarios)
                .WithMany(c => c.Carros)
                .Map(m => m.MapLeftKey("CarroID").
                    MapRightKey("ProprietarioID").
                    ToTable("ProprietarioCarroes"));
        }
    }
}