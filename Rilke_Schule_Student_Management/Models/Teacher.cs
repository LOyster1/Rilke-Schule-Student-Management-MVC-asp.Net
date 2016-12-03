using System.ComponentModel.DataAnnotations;



namespace Rilke_Schule_Student_Management.Models
{
    public class Teacher
    {
        [Key]
        [Required]
        [Display(Name = "Teacher Name")]
        public string Teacher_Id { get; set; }
    }
}