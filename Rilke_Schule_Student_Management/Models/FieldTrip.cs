using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Rilke_Schule_Student_Management.Models
{
    public class FieldTrip
    {
        [Key]
        public int FieldTrip_Id { get; set; }

        [Required]
        [Display(Name = "Class Id")]
        public string Class_Id { get; set; }//---Changed to string
        [ForeignKey("Class_Id"), Column(Order = 1)]
        public virtual Class Class { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Must enter a Trip Name", MinimumLength = 1)]
        [DataType(DataType.Text)]
        [Display(Name = "Trip Name")]
        public string TripName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Submit By Date")]
        public DateTime? SubmitByDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Trip Date")]
        public DateTime? TripDate { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Chapperone Arrival Time")]
        public DateTime? ChapperoneArrivalTime { get; set;  }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Departure Time")]
        public DateTime? DepartureTime { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Return Time")]
        public DateTime? ReturnTime { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Name Not Found", MinimumLength = 1)]
        [DataType(DataType.Text)]
        [Display(Name = "Transportation Provided By")]
        public string Transportation { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Chapperone Cost")]
        public float? ChapperoneCost { get; set; }
    }
}