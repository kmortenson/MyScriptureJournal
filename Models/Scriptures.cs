using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyScriptureJournal.Models
{
    public class Scriptures
    {
        public int ID { get; set; }
        public string Scripture { get; set; }

        [Display(Name = "Date Entered")]
        [DataType(DataType.Date)]
        public DateTime DateEntered { get; set; }
        public string Note { get; set; }
       
    }
}