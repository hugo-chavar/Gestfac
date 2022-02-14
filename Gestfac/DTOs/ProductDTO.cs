using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestfac.DTOs
{

    public class ProductDTO
    {
        [Key]
        public int ProductId { get; set; }
        public string ExternalId { get; set; }
        public string Description { get; set; }

        public int? CurrentPriceUpdateId { get; set; }

        [ForeignKey("CurrentPriceUpdateId")]
        public virtual PriceUpdateDTO CurrentPriceUpdate { get; set; }

        public string TagsSerialized { get; set; }

        public List<PriceUpdateDTO> PriceUpdates { get; set; }

    }
}
