using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestfac.DTOs
{
    public class PriceUpdateDTO
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }

        public int ProductId { get; set; }

        public ProductDTO Product { get; set; }
    }
}
