using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace lastVersionAuthe.Models
{
    public class Manual
    {
        [Key]
        public int Id { get; set; }
        public String UserCreator { get; set; }
        public String Title { get; set; }
        public String ShortDescription { get; set; }
        public Double Rating { get; set; }
        public String Body { get; set; }
        public String AmountLike { get; set; }
        public String Tags { get; set; }
        public DateTime CreatedDate { get; set; }
        public String Themes { get; set; }
        public String ManualImage { get; set; }
        public Double AmountLook { get; set; }
        public Double AmountRatingChange { get; set; }
    }
}
