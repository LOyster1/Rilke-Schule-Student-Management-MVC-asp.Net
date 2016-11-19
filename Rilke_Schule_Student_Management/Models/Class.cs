using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rilke_Schule_Student_Management.Models
{
    public class Class
    {
        [Key]
        [Required]
        public int Class_Id { get; set; }

        [Required]
        public int Teacher_Id { get; set; }
        [ForeignKey("Teacher_Id"), Column(Order = 1)]
        public virtual Teacher Teacher { get; set; }

        [Required]
        public int Student_Number { get; set; }
        [ForeignKey("Student_Number"), Column(Order = 2)]
        public virtual Student Student { get; set; }
    }
}