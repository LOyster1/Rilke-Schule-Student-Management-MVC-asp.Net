using System.ComponentModel.DataAnnotations;

namespace Rilke_Schule_Student_Management.ViewModels
{
    public class StudentFormViewModel
    {
        [Required]
        [Display(Name = "Student First Name")]
        public string Student_First_Name { get; set; }

        [Required]
        [Display(Name = "Student Last Name")]
        public string Student_Last_Name { get; set; }

        [Required]
        [Display(Name = "Date Of Birth")]
        public string Date_Of_Birth { get; set; }
    }
}