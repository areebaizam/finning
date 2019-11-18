using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinningUrlShortner.DTOs
{
    public class ShortUrlInDTO
    {
        [Required]
        public string OriginalURL { get; set; }
    }
}
