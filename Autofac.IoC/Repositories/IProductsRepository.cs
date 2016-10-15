using System;
using System.Collections.Generic;
using System.Linq;
using Autofac.IoC.BusinessServices;

namespace Autofac.IoC.Repositories
{
    public class Product
    {
        public int Code { get; set; }
        public string Description { get; set; }
    }

    public interface IProductsRepository
    {
        List<Product> GetProducts();
    }

    public class ProductsRepository : IProductsRepository
    {
        private string _connectionString;
        private List<Product> _products { get; set; }

        public ProductsRepository(string connectionString)
        {
            _connectionString = connectionString;

            _products = new List<Product>()
            {
                new Product()
                {
                    Code = 1,
                    Description = "Product1"
                },
                new Product()
                {
                    Code = 2,
                    Description = "Product2"
                }
            };
        }

        public List<Product> GetProducts()
        {
            return _products;
        }
    }
}
