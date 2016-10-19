using System;
using System.Collections.Generic;
using System.Linq;
using Autofac.IoC.CoreServices;

namespace Autofac.IoC.BusinessServices
{
    public class AccountService
    {
        private ITokenService _tokenService;

        public AccountService(PerRequestTokenService tokenService)
        {
            _tokenService = tokenService;
        }
    }
}
