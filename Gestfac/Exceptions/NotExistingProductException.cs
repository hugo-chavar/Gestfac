using Gestfac.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestfac.Exceptions
{
    public class NotExistingProductException : Exception
    {
        private readonly Product product;

        public NotExistingProductException(Product product)
        {
            this.product = product;
        }
    }
}
