namespace Concessionaria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carroes",
                c => new
                    {
                        CarroID = c.Int(nullable: false, identity: true),
                        FabricanteID = c.Int(nullable: false),
                        Nome = c.String(),
                        Ano = c.Int(nullable: false),
                        Combustivel = c.String(),
                    })
                .PrimaryKey(t => t.CarroID)
                .ForeignKey("dbo.Fabricantes", t => t.FabricanteID, cascadeDelete: true)
                .Index(t => t.FabricanteID);
            
            CreateTable(
                "dbo.Fabricantes",
                c => new
                    {
                        FabricanteID = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        PaisOrigem = c.String(),
                    })
                .PrimaryKey(t => t.FabricanteID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Carroes", "FabricanteID", "dbo.Fabricantes");
            DropIndex("dbo.Carroes", new[] { "FabricanteID" });
            DropTable("dbo.Fabricantes");
            DropTable("dbo.Carroes");
        }
    }
}
