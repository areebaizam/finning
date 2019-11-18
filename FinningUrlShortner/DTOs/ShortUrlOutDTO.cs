using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinningUrlShortner.DTOs
{
    public class ShortUrlOutDTO
    {
        public string OriginalURL { get; set; }
        public string ShortURL { get; set; }
    }
}
