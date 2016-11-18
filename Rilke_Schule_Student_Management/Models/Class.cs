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
        public string UserName { get; set; }
        [ForeignKey("UserName"), Column(Order = 1)]
        public virtual Teacher Teacher { get; set; }

        [Required]
        public int Student_Number { get; set; }
        [ForeignKey("Student_Number"), Column(Order = 2)]
        public virtual Student Student { get; set; }
    }
}