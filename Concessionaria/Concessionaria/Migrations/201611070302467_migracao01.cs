namespace Concessionaria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migracao01 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Carroes", newName: "Carro");
            RenameTable(name: "dbo.Fabricantes", newName: "Fabricante");
            RenameTable(name: "dbo.Proprietarios", newName: "Proprietario");
            RenameColumn(table: "dbo.ProprietarioCarroes", name: "Proprietario_ProprietarioID", newName: "ProprietarioID");
            RenameColumn(table: "dbo.ProprietarioCarroes", name: "Carro_CarroID", newName: "CarroID");
            RenameIndex(table: "dbo.ProprietarioCarroes", name: "IX_Carro_CarroID", newName: "IX_CarroID");
            RenameIndex(table: "dbo.ProprietarioCarroes", name: "IX_Proprietario_ProprietarioID", newName: "IX_ProprietarioID");
            DropPrimaryKey("dbo.ProprietarioCarroes");
            AddPrimaryKey("dbo.ProprietarioCarroes", new[] { "CarroID", "ProprietarioID" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ProprietarioCarroes");
            AddPrimaryKey("dbo.ProprietarioCarroes", new[] { "Proprietario_ProprietarioID", "Carro_CarroID" });
            RenameIndex(table: "dbo.ProprietarioCarroes", name: "IX_ProprietarioID", newName: "IX_Proprietario_ProprietarioID");
            RenameIndex(table: "dbo.ProprietarioCarroes", name: "IX_CarroID", newName: "IX_Carro_CarroID");
            RenameColumn(table: "dbo.ProprietarioCarroes", name: "CarroID", newName: "Carro_CarroID");
            RenameColumn(table: "dbo.ProprietarioCarroes", name: "ProprietarioID", newName: "Proprietario_ProprietarioID");
            RenameTable(name: "dbo.Proprietario", newName: "Proprietarios");
            RenameTable(name: "dbo.Fabricante", newName: "Fabricantes");
            RenameTable(name: "dbo.Carro", newName: "Carroes");
        }
    }
}
