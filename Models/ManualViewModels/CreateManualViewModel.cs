using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace lastVersionAuthe.Models.ManualViewModels
{
    public class CreateManualViewModel
    {
        [Required]
        [Display(Name = "Title")]
        [StringLength(50, ErrorMessage = "NameLength", MinimumLength = 6)]
        public String Title { get; set; }

        [Required]
        [Display(Name = "Description")]
        public String ShortDescription { get; set; }

        [Display(Name = "Image")]
        public String Image { get; set; }

        public String Content { get; set; }

        [Required]
        [Display(Name = "Themes")]
        public String Themes { get; set; }

        [Display(Name = "Tags")]
        public String Tags { get; set; }
    }
}
