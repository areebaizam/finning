using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinningUrlShortner.Models
{
    public class ShortUrl : BaseEntity
    {
        [Required]
        public string OriginalURL { get; set; }
        public string ShortURL { get; set; }
        public string Token { get; set; }
    }
}