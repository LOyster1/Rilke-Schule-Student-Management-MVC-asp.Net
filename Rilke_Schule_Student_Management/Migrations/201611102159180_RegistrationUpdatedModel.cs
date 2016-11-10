namespace Rilke_Schule_Student_Management.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RegistrationUpdatedModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Students", "Stud_F_Name", c => c.String(nullable: false));
            AlterColumn("dbo.Students", "Stud_L_Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Students", "Stud_L_Name", c => c.String());
            AlterColumn("dbo.Students", "Stud_F_Name", c => c.String());
        }
    }
}
