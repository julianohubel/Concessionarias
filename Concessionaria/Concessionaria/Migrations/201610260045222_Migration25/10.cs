namespace Concessionaria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2510 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Carroes", "Nome", c => c.String(nullable: false, maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Carroes", "Nome", c => c.String());
        }
    }
}
