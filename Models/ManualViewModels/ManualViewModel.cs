using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lastVersionAuthe.Models.ManualViewModels
{
    public class ManualViewModel
    {
        public String Creator { get; set; }

        public String Title { get; set; }

        public String ShortDescription { get; set; }

        public Double Rating { get; set; }

        public String Body { get; set; }

        public String AmountLike { get; set; }

        public String ImageManual { get; set; }

        public String Themes { get; set; }

        public IEnumerable<String> Tags { get; set; }

        public IEnumerable<string> OtherManualsUser { get; set; }

    }
}
