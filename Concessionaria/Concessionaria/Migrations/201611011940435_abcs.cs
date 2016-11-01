namespace Concessionaria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class abcs : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProprietarioCarroes", "Proprietario_ProprietarioID", "dbo.Proprietarios");
            DropForeignKey("dbo.ProprietarioCarroes", "Carro_CarroID", "dbo.Carroes");
            DropIndex("dbo.ProprietarioCarroes", new[] { "Proprietario_ProprietarioID" });
            DropIndex("dbo.ProprietarioCarroes", new[] { "Carro_CarroID" });
            AddColumn("dbo.Proprietarios", "Carros_CarroID", c => c.Int());
            CreateIndex("dbo.Proprietarios", "Carros_CarroID");
            AddForeignKey("dbo.Proprietarios", "Carros_CarroID", "dbo.Carroes", "CarroID");
            DropTable("dbo.ProprietarioCarroes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProprietarioCarroes",
                c => new
                    {
                        Proprietario_ProprietarioID = c.Int(nullable: false),
                        Carro_CarroID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Proprietario_ProprietarioID, t.Carro_CarroID });
            
            DropForeignKey("dbo.Proprietarios", "Carros_CarroID", "dbo.Carroes");
            DropIndex("dbo.Proprietarios", new[] { "Carros_CarroID" });
            DropColumn("dbo.Proprietarios", "Carros_CarroID");
            CreateIndex("dbo.ProprietarioCarroes", "Carro_CarroID");
            CreateIndex("dbo.ProprietarioCarroes", "Proprietario_ProprietarioID");
            AddForeignKey("dbo.ProprietarioCarroes", "Carro_CarroID", "dbo.Carroes", "CarroID", cascadeDelete: true);
            AddForeignKey("dbo.ProprietarioCarroes", "Proprietario_ProprietarioID", "dbo.Proprietarios", "ProprietarioID", cascadeDelete: true);
        }
    }
}
