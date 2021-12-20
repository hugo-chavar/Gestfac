using Gestfac.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestfac.Exceptions
{
    public class ExistingProductException : Exception
    {
        private readonly Product product;

        public ExistingProductException(Product product)
        {
            this.product = product;
        }
    }
}
