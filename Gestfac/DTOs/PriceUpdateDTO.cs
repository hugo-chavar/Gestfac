using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestfac.DTOs
{
    public class PriceUpdateDTO
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }

        [Column("ProductId")]
        public int ProductDTOId { get; set; }

        public ProductDTO ProductDTO { get; set; }
    }
}
