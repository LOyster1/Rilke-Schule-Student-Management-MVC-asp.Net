namespace Rilke_Schule_Student_Management.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateGuardianTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Guardianships", "Student_Student_Number", "dbo.Students");
            DropIndex("dbo.Guardianships", new[] { "Parent_Id" });
            DropIndex("dbo.Guardianships", new[] { "Student_Student_Number" });
            RenameColumn(table: "dbo.Guardianships", name: "Parent_Id", newName: "UserName");
            RenameColumn(table: "dbo.Guardianships", name: "Student_Student_Number", newName: "Student_Number");
            DropPrimaryKey("dbo.Guardianships");
            AlterColumn("dbo.Guardianships", "UserName", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Guardianships", "Student_Number", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Guardianships", "UserName");
            CreateIndex("dbo.Guardianships", "UserName");
            CreateIndex("dbo.Guardianships", "Student_Number");
            AddForeignKey("dbo.Guardianships", "Student_Number", "dbo.Students", "Student_Number", cascadeDelete: true);
            DropColumn("dbo.Guardianships", "Guard_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Guardianships", "Guard_ID", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Guardianships", "Student_Number", "dbo.Students");
            DropIndex("dbo.Guardianships", new[] { "Student_Number" });
            DropIndex("dbo.Guardianships", new[] { "UserName" });
            DropPrimaryKey("dbo.Guardianships");
            AlterColumn("dbo.Guardianships", "Student_Number", c => c.Int());
            AlterColumn("dbo.Guardianships", "UserName", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.Guardianships", "Guard_ID");
            RenameColumn(table: "dbo.Guardianships", name: "Student_Number", newName: "Student_Student_Number");
            RenameColumn(table: "dbo.Guardianships", name: "UserName", newName: "Parent_Id");
            CreateIndex("dbo.Guardianships", "Student_Student_Number");
            CreateIndex("dbo.Guardianships", "Parent_Id");
            AddForeignKey("dbo.Guardianships", "Student_Student_Number", "dbo.Students", "Student_Number");
        }
    }
}
