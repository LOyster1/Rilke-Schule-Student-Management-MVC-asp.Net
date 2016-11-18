using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rilke_Schule_Student_Management.Models
{
    public class Teacher
    {
        [Key]
        [Column(Order = 2)]
        public int Teacher_Id { get; set; }

        [Required]
        [Display(Name = "Teacher Name")]
        public String Name;
    }
}