using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestfac.DTOs
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public string ExternalId { get; set; }
        public string Description { get; set; }
        public double CurrentPrice { get; set; }

    }
}
