using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SpeechText.Models
{
    public class SpeechFile
    {
        public int Id { get; set; }
        [Display(Name="Audio's URl")]
        public string URI { get; set; }
        [Display(Name = "Json's audio")]
        public string Text { get; set; }
    }
}