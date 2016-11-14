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

        [Required]
        [Display(Name ="Student First Name")]
        public String Stud_F_Name { get; set; }

        [Required]
        [Display(Name = "Student Last Name")]
        public String Stud_L_Name { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true,
         DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        [RegularExpression(@"^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$"
                , ErrorMessage = "Invalid date.")]
        [Display(Name = "Date of Birth")]
        public DateTime Date_Of_Birth { get; set; }
    }
}