using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac.IoC.BusinessServices;

namespace Autofac.IoC.Controllers.Controllers
{
    public class HomeController : Controller
    {
        LoggerService _loggerService;

        public HomeController(LoggerService loggerService)
        {
            _loggerService = loggerService;
        }
    }
}
