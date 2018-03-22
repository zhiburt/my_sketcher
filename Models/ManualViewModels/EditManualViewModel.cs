using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace lastVersionAuthe.Models.ManualViewModels
{
    public class EditManualViewModel
    {
        [Display(Name = "Title")]
        public String Title { get; set; }

        [Display(Name = "Description")]
        public String ShortDescription { get; set; }

        public String Content { get; set; }

        [Display(Name = "Themes")]
        public String Themes { get; set; }

        [Display(Name = "Tags")]
        public String Tags { get; set; }
    }
}
