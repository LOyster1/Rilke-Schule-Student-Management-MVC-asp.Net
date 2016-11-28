using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rilke_Schule_Student_Management.Models
{
    public class Class
    {
        [Key]
        [Required]
        [Display(Name = "Teacher Name")]
        public string Class_Id { get; set; }


        [Required]
        public int Student_Number { get; set; }
        [ForeignKey("Student_Number"), Column(Order = 2)]
        public virtual Student Student { get; set; }
    }
}