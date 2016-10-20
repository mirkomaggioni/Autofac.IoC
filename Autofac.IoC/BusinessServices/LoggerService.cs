using Autofac.IoC.CoreServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Autofac.IoC.BusinessServices
{
    public class LoggerService
    {
        private ITokenService _tokenService;

        public LoggerService(SingletonTokenService tokenService)
        {
            _tokenService = tokenService;
        }
    }
}
