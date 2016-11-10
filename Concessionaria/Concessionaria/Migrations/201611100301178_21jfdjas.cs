namespace Concessionaria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _21jfdjas : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Carro", "NomeUnico");
            DropIndex("dbo.Fabricante", "NomeUnico");
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Fabricante", "Nome", unique: true, name: "NomeUnico");
            CreateIndex("dbo.Carro", "Nome", unique: true, name: "NomeUnico");
        }
    }
}
