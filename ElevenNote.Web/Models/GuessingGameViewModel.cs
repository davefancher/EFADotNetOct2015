using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElevenNote.Web.Models
{
    public class GuessingGameViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [MinLength(2, ErrorMessage = "Too short")]
        [MaxLength(20, ErrorMessage = "Too long")]
        [Display(Name = "Your name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Guess is required")]
        [Range(1, 10, ErrorMessage = "Guess must be between 1 and 10")]
        public int Guess { get; set; }

        public override string ToString()
        {
            return String.Format("{0} ({1})", Name, Guess);
        }
    }
}
