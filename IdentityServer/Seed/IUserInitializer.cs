using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Seed
{
    public interface IUserInitializer
    {
        public void Initialize();
    }
}
