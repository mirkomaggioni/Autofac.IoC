using System;
using System.Collections.Generic;
using System.Linq;
using Autofac.IoC.CoreServices;
using Autofac.IoC.Repositories;

namespace Autofac.IoC.BusinessServices
{
    public class OrdersService
    {
        private ITokenService _tokenService;

        public OrdersService(ITokenService tokenService)
        {
            this._tokenService = tokenService;
        }

    }
}
