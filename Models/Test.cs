using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sample_webproject.Models
{
    public class Test
    {
        public int ID { get; set; }
        public string Test_Name { get; set; }
        [Display(Name = "Date Edit")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? Test_Date { get; set; }
        public Decimal Test_Price { get; set; }
        public string Test_Result { get; set; }
        public string Doctor_Remarks { get; set; }
        public List<Test> usersinfo { get; set; }

    }
    
}