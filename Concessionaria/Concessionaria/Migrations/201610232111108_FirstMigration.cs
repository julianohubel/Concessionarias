namespace Concessionaria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Proprietarios",
                c => new
                    {
                        ProprietarioID = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Email = c.String(),
                        CPF = c.String(),
                        DataNascimento = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ProprietarioID);
            
            CreateTable(
                "dbo.ProprietarioCarroes",
                c => new
                    {
                        Proprietario_ProprietarioID = c.Int(nullable: false),
                        Carro_CarroID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Proprietario_ProprietarioID, t.Carro_CarroID })
                .ForeignKey("dbo.Proprietarios", t => t.Proprietario_ProprietarioID, cascadeDelete: true)
                .ForeignKey("dbo.Carroes", t => t.Carro_CarroID, cascadeDelete: true)
                .Index(t => t.Proprietario_ProprietarioID)
                .Index(t => t.Carro_CarroID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProprietarioCarroes", "Carro_CarroID", "dbo.Carroes");
            DropForeignKey("dbo.ProprietarioCarroes", "Proprietario_ProprietarioID", "dbo.Proprietarios");
            DropIndex("dbo.ProprietarioCarroes", new[] { "Carro_CarroID" });
            DropIndex("dbo.ProprietarioCarroes", new[] { "Proprietario_ProprietarioID" });
            DropTable("dbo.ProprietarioCarroes");
            DropTable("dbo.Proprietarios");
        }
    }
}
