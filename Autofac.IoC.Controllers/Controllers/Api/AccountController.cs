using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Autofac.IoC.BusinessServices;

namespace Autofac.IoC.Controllers.Controllers.Api
{
    public class AccountController : ApiController
    {
        LoggerService _loggerService;

        public AccountController(LoggerService loggerService)
        {
            _loggerService = loggerService;
        }
    }
}
