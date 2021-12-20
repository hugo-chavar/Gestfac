using Gestfac.Exceptions;
using Gestfac.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Gestfac
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Catalog catalog = new Catalog();

            try
            {
                catalog.AddProduct(new Product() { Id = 1, CurrentPrice = 12.5, Description = "Tornillo", ExternalId = "AAA111" });
                catalog.AddProduct(new Product() { Id = 2, CurrentPrice = 34.2, Description = "Arandela para PVC", ExternalId = "BBB222" });
                catalog.AddProduct(new Product() { Id = 3, CurrentPrice = 7.0, Description = "Pegamento PVC", ExternalId = "BBB222" });

                IEnumerable<Product> products = catalog.GetProductsByDescription("PVC");

            }
            catch (ExistingProductException ex)
            {

                throw;
            }


            base.OnStartup(e);
        }
    }
}
