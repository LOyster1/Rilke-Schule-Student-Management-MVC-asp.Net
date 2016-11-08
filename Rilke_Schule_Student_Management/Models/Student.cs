using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rilke_Schule_Student_Management.Models
{
    public class Student
    {
        [Key]
        [Column(Order =2)]
        public int Student_Number { get; set; }
        public String Stud_F_Name { get; set; }
        public String Stud_L_Name { get; set; }
        public DateTime Date_Of_Birth { get; set; }
    }
}