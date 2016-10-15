using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autofac.IoC.CoreServices
{
    public class PerDependencyTokenService : ITokenService
    {
        private Guid _token { get; set; }

        public Guid GetToken()
        {
            if (_token == Guid.Empty)
                _token = Guid.NewGuid();

            return _token;
        }
    }
}
