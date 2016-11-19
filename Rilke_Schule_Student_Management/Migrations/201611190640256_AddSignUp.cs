namespace Rilke_Schule_Student_Management.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSignUp : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SignUps",
                c => new
                    {
                        Sign_Up_Id = c.Int(nullable: false, identity: true),
                        FieldTrip_Id = c.Int(nullable: false),
                        Student_Id = c.Int(nullable: false),
                        Chaperone_Name = c.String(nullable: false, maxLength: 100),
                        Chaperone_Contact_Number = c.String(nullable: false, maxLength: 100),
                        Emergency_Contact_Name = c.String(nullable: false, maxLength: 100),
                        Emergency_Contact_Number = c.String(nullable: false, maxLength: 100),
                        Date_Signed = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Sign_Up_Id)
                .ForeignKey("dbo.FieldTrips", t => t.FieldTrip_Id, cascadeDelete: false)
                .ForeignKey("dbo.Students", t => t.Student_Id, cascadeDelete: true)
                .Index(t => t.FieldTrip_Id)
                .Index(t => t.Student_Id);
            
            AddColumn("dbo.FieldTrips", "Class_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.FieldTrips", "Class_Id");
            AddForeignKey("dbo.FieldTrips", "Class_Id", "dbo.Classes", "Class_Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SignUps", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.SignUps", "FieldTrip_Id", "dbo.FieldTrips");
            DropForeignKey("dbo.FieldTrips", "Class_Id", "dbo.Classes");
            DropIndex("dbo.SignUps", new[] { "Student_Id" });
            DropIndex("dbo.SignUps", new[] { "FieldTrip_Id" });
            DropIndex("dbo.FieldTrips", new[] { "Class_Id" });
            DropColumn("dbo.FieldTrips", "Class_Id");
            DropTable("dbo.SignUps");
        }
    }
}
