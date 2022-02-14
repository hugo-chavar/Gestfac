using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gestfac.Models
{
    public class Product
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ExternalId { get; set; }
        [Required]
        public string Description { get; set; }

        public int CurrentPriceUpdateId { get; set; }

        public PriceUpdate CurrentPriceUpdate { get; set; }

        public List<string> Tags { get; set; }
        public List<PriceUpdate> PriceUpdates { get; set; }

        public bool IsActive { get; set; }

        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return ExternalId == ((Product)obj).ExternalId;
        }

        public override int GetHashCode()
        {
            return ExternalId.GetHashCode();
        }

        public void UpdatePrice(double currentPrice)
        {
            CurrentPriceUpdate = new PriceUpdate() { Date = DateTime.Now, Price = currentPrice };
            PriceUpdates.Add(CurrentPriceUpdate);
        }
    }
}
