using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinningUrlShortner.Models
{
    public class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public bool IsDelete { get; set; } = false;
    }
}
