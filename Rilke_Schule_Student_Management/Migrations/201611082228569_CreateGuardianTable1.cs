namespace Rilke_Schule_Student_Management.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class CreateGuardianTable1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Guardianships", new[] { "UserName" });
            DropPrimaryKey("dbo.Guardianships");
            AddColumn("dbo.Guardianships", "Guardianship_Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Guardianships", "UserName", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.Guardianships", "Guardianship_Id");
            CreateIndex("dbo.Guardianships", "UserName");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Guardianships", new[] { "UserName" });
            DropPrimaryKey("dbo.Guardianships");
            AlterColumn("dbo.Guardianships", "UserName", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Guardianships", "Guardianship_Id");
            AddPrimaryKey("dbo.Guardianships", "UserName");
            CreateIndex("dbo.Guardianships", "UserName");
        }
    }
}
