using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace lastVersionAuthe.Models
{
    public class UserRatings
    {
        [Key]
        public String UserId { get; set; }

        public String ManulsRating { get; set; }
    }
}
