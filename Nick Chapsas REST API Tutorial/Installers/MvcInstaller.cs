using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Nick_Chapsas_REST_API_Tutorial.Options;
using Nick_Chapsas_REST_API_Tutorial.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nick_Chapsas_REST_API_Tutorial.Installers
{
    public class MvcInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var jwtSettings = new JwtSettings();
            configuration.Bind(nameof(jwtSettings), jwtSettings);
            services.AddSingleton(jwtSettings);
            services.AddScoped<IIdentityService,IdentityService>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                

            }).AddJwtBearer(op =>
            {
                op.SaveToken = false;
                op.TokenValidationParameters = new TokenValidationParameters
                {
                    //Authentication
                    ValidateIssuerSigningKey = true, //This Will Validate the last bit of out JWT  using secret 


                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                    ValidateIssuer = false, //Will explain This After Video 10
                    ValidateAudience = false, //Will explain This After Video 10
                    RequireExpirationTime = false,
                    ValidateLifetime = true,


                };

            });
        }
    }
}
