using Cosmonaut;
using Cosmonaut.Extensions.Microsoft.DependencyInjection;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nick_Chapsas_REST_API_Tutorial.Domain;
using Nick_Chapsas_REST_API_Tutorial.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nick_Chapsas_REST_API_Tutorial.Installers
{
    public class CosomosInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection service, IConfiguration configuration)
        {
            var cosomsoStoreSettings = new CosmosStoreSettings(
                configuration["CosomosSettings:DatebaseName"],
                configuration["CosomosSettings:AccountUri"],
                configuration["CosomosSettings:AccountKey"],
                new ConnectionPolicy { ConnectionMode = ConnectionMode.Direct, ConnectionProtocol = Protocol.Tcp}
                
                );

            service.AddCosmosStore<CosomosPostDto>(cosomsoStoreSettings);
        }
    }
}
