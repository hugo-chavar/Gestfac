using System;

namespace Gestfac.Models
{
    public class PriceUpdate
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}