namespace Rilke_Schule_Student_Management.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateStudentTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Guardianships",
                c => new
                    {
                        Guard_ID = c.Int(nullable: false, identity: true),
                        Parent_Id = c.String(maxLength: 128),
                        Student_Student_Number = c.Int(),
                    })
                .PrimaryKey(t => t.Guard_ID)
                .ForeignKey("dbo.AspNetUsers", t => t.Parent_Id)
                .ForeignKey("dbo.Students", t => t.Student_Student_Number)
                .Index(t => t.Parent_Id)
                .Index(t => t.Student_Student_Number);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Student_Number = c.Int(nullable: false, identity: true),
                        Stud_F_Name = c.String(),
                        Stud_L_Name = c.String(),
                        Date_Of_Birth = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Student_Number);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Guardianships", "Student_Student_Number", "dbo.Students");
            DropForeignKey("dbo.Guardianships", "Parent_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Guardianships", new[] { "Student_Student_Number" });
            DropIndex("dbo.Guardianships", new[] { "Parent_Id" });
            DropTable("dbo.Students");
            DropTable("dbo.Guardianships");
        }
    }
}
