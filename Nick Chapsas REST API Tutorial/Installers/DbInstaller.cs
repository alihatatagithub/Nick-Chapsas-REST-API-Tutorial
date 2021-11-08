using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nick_Chapsas_REST_API_Tutorial.Data;
using Nick_Chapsas_REST_API_Tutorial.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nick_Chapsas_REST_API_Tutorial.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            //services.AddAuthentication(AzureADDefaults.BearerAuthenticationScheme)
            //  .AddAzureADBearer(options => configuration.Bind("AzureAd", options));

            services.AddDbContext<DataContext>(op =>
            op.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<DataContext>();
            //services.AddScoped<IPostService, PostService>();
            services.AddSingleton<IPostService, CosomosPostService>();

        }
    }
}