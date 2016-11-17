using System;
using System.ComponentModel.DataAnnotations;


namespace Rilke_Schule_Student_Management.Models
{
    public class FieldTripViewModel
    {
      
            [Required]
            [StringLength(100, ErrorMessage = "Must enter a Trip Name", MinimumLength = 1)]
            [DataType(DataType.Text)]
            [Display(Name = "Trip Name")]
            public string TripName { get; set; }

            [Required]
            //[DisplayFormat(ApplyFormatInEditMode = true,
            // DataFormatString = "{0:MM/dd/yyyy}")]
            [DataType(DataType.Date)]
            //[RegularExpression(@"^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$"
            //    , ErrorMessage = "Invalid date.")]
            [Display(Name = "Submit By Date")]
            public DateTime? SubmitByDate { get; set; }

            [Required]
            [DisplayFormat(ApplyFormatInEditMode = true,
             DataFormatString = "{0:MM/dd/yyyy}")]
            [DataType(DataType.Date)]
            [RegularExpression(@"^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$"
                , ErrorMessage = "Invalid date.")]
            [Display(Name = "Trip Date")]
            public DateTime? TripDate { get; set; }

            [Required]
            [DisplayFormat(ApplyFormatInEditMode = true,
             DataFormatString = "{0:HH:mm}")]
            [RegularExpression(@"^(?:0?[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Invalid time.")]
            [DataType(DataType.Time)]
            [Display(Name = "Chapperone Arrival Time")]
            public DateTime? ChapperoneArrivalTime { get; set;  }

            [Required]
            [DisplayFormat(ApplyFormatInEditMode = true,
            DataFormatString = "{0:HH:mm}")]
            [RegularExpression(@"^(?:0?[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Invalid time.")]
            [DataType(DataType.Time)]
            [Display(Name = "Departure Time")]
            public DateTime? DepartureTime { get; set; }

            [Required]
            [DisplayFormat(ApplyFormatInEditMode = true,
            DataFormatString = "{0:HH:mm}")]
            [RegularExpression(@"^(?:0?[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Invalid time.")]
            [DataType(DataType.Time)]
            [Display(Name = "Return Time")]
            public DateTime? ReturnTime { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "Name Not Found", MinimumLength = 1)]
            [DataType(DataType.Text)]
            [Display(Name = "Transportation Provided By")]
            public string Transportation { get; set; }

        
    }
}