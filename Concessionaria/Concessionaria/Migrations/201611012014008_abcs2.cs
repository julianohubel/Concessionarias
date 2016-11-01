namespace Concessionaria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class abcs2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Proprietarios", "Carros_CarroID", "dbo.Carroes");
            DropIndex("dbo.Proprietarios", new[] { "Carros_CarroID" });
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
            
            DropColumn("dbo.Proprietarios", "Carros_CarroID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Proprietarios", "Carros_CarroID", c => c.Int());
            DropForeignKey("dbo.ProprietarioCarroes", "Carro_CarroID", "dbo.Carroes");
            DropForeignKey("dbo.ProprietarioCarroes", "Proprietario_ProprietarioID", "dbo.Proprietarios");
            DropIndex("dbo.ProprietarioCarroes", new[] { "Carro_CarroID" });
            DropIndex("dbo.ProprietarioCarroes", new[] { "Proprietario_ProprietarioID" });
            DropTable("dbo.ProprietarioCarroes");
            CreateIndex("dbo.Proprietarios", "Carros_CarroID");
            AddForeignKey("dbo.Proprietarios", "Carros_CarroID", "dbo.Carroes", "CarroID");
        }
    }
}
