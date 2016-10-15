using System;
using System.Collections.Generic;
using System.Linq;
using Autofac.IoC.CoreServices;
using Autofac.IoC.Repositories;

namespace Autofac.IoC.BusinessServices
{
    public class ProductsService
    {
        private IProductsRepository _productsRepository;
        private ITokenService _tokenService;

        public ProductsService(IProductsRepository productsRepository, ITokenService tokenService)
        {
            this._productsRepository = productsRepository;
            this._tokenService = tokenService;
        }

        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();

            if (_tokenService.GetToken() != Guid.Empty)
                products = _productsRepository.GetProducts();

            return products;
        }
    }
}
