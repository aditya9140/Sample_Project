using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Sample_webproject.Models
{
    public class Patient
    {
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
        [Display(Name ="Date Edit")]
        [DisplayFormat(ApplyFormatInEditMode =true,DataFormatString ="{0:yyyy-MM-dd HH:mm}")]
        public DateTime? DOB { get; set; }
        [Display(Name = "Date Edit")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime? Admission_Date { get; set; }
        public string Hospital { get; set; }
        [Display(Name = "Date Edit")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime? Discharge_Date { get; set; }
        public Decimal Total_Amount { get; set; }
       
      }
}