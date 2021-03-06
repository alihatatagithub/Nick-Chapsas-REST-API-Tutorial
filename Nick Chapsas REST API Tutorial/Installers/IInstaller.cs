using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nick_Chapsas_REST_API_Tutorial.Installers
{
     public interface IInstaller
    {
        void InstallServices(IServiceCollection service, IConfiguration configuration);
    }
}
