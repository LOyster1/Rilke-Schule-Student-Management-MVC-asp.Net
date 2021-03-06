﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Rilke_Schule_Student_Management.Models
{
    public class SignUp
    {
        [Key]
        public int Sign_Up_Id { get; set; }

        [Required]
        public int FieldTrip_Id { get; set; }
        [ForeignKey("FieldTrip_Id"), Column(Order = 1)]
        public virtual FieldTrip FieldTrip { get; set; }

        [Required]
        public int Class_Id { get; set; }
        [ForeignKey("Class_Id"), Column(Order = 2)]
        public virtual Class Class { get; set; }

        [Required]
        [Display(Name = "Slip Signed")]
        public bool Signed_Bit { get; set; }
        

        [StringLength(100, MinimumLength = 1)]
        [DataType(DataType.Text)]
        [Display(Name = "Chaperone Name")]
        public string Chaperone_Name { get; set; }
        
        [StringLength(100, MinimumLength = 1)]
        [DataType(DataType.Text)]
        [Display(Name = "Chaperone Contact Number")]
        public string Chaperone_Contact_Number { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Must enter a Emergency Contact Name", MinimumLength = 1)]
        [DataType(DataType.Text)]
        [Display(Name = "Emergency Contact Name")]
        public string Emergency_Contact_Name { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Must enter a Emergency Contact Number", MinimumLength = 1)]
        [DataType(DataType.Text)]
        [Display(Name = "Emergency Contact Number")]
        public string Emergency_Contact_Number { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date Signed")]
        public DateTime? Date_Signed { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Printed Name")]
        public string Printed_Name { get; set; }
    }
}