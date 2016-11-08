namespace Rilke_Schule_Student_Management.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateGuardianTable2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Guardianships", "UserName", "dbo.AspNetUsers");
            DropIndex("dbo.Guardianships", new[] { "UserName" });
            AlterColumn("dbo.Guardianships", "UserName", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Guardianships", "UserName");
            AddForeignKey("dbo.Guardianships", "UserName", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Guardianships", "UserName", "dbo.AspNetUsers");
            DropIndex("dbo.Guardianships", new[] { "UserName" });
            AlterColumn("dbo.Guardianships", "UserName", c => c.String(maxLength: 128));
            CreateIndex("dbo.Guardianships", "UserName");
            AddForeignKey("dbo.Guardianships", "UserName", "dbo.AspNetUsers", "Id");
        }
    }
}
