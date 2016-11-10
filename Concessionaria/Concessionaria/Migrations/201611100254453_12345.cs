namespace Concessionaria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _12345 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Fabricante", "Nome", c => c.String(nullable: false));
            AlterColumn("dbo.Fabricante", "PaisOrigem", c => c.String(nullable: false));
            CreateIndex("dbo.Carro", "Nome", unique: true, name: "NomeUnicoCarro");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Carro", "NomeUnicoCarro");
            AlterColumn("dbo.Fabricante", "PaisOrigem", c => c.String());
            AlterColumn("dbo.Fabricante", "Nome", c => c.String());
        }
    }
}
