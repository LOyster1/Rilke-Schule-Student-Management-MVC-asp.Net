namespace Rilke_Schule_Student_Management.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class updateASPNetUser : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "StudentFirstName");
            DropColumn("dbo.AspNetUsers", "StudentLastName");
            DropColumn("dbo.AspNetUsers", "DateOfBirth");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "DateOfBirth", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "StudentLastName", c => c.String());
            AddColumn("dbo.AspNetUsers", "StudentFirstName", c => c.String());
        }
    }
}
