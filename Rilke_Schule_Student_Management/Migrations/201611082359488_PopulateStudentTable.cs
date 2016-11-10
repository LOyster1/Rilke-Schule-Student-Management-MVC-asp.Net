namespace Rilke_Schule_Student_Management.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateStudentTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Students (Stud_F_Name, Stud_L_Name,Date_Of_Birth) VALUES ('Jimmy','Wilkerson',2000/12/12)");
            Sql("INSERT INTO Students ( Stud_F_Name,Stud_L_Name,Date_Of_Birth) VALUES ( 'Toby','MacCarthy',2001/01/15)");
            Sql("INSERT INTO Students ( Stud_F_Name,Stud_L_Name,Date_Of_Birth) VALUES ( 'Crysal','Robles',2001/05/12)");
            Sql("INSERT INTO Students ( Stud_F_Name,Stud_L_Name,Date_Of_Birth) VALUES ( 'Matthew','Holmes',1999/12/28)");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM Students WHERE Student_Number IN (1,2,3,4)");
        }
    }
}
