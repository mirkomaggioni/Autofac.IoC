using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autofac.IoC.CoreServices
{
    public interface ITokenService
    {
        Guid GetToken();
    }
}
