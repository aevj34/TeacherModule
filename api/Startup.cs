using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            String secretKey = "ahbasshfbsahjfbshajbfhjasbfashjbfsajhfvashjfashfbsahfbsahfksdjf";
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "mysite.com",
                    ValidAudience = "mysite.com",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddCors(
options => options.AddPolicy("AllowCors",
builder =>
{
 builder
 .WithOrigins("http://localhost:4200") //AllowSpecificOrigins;
                                       //.WithOrigins("http://localhost:57872", "http://localhost:57872") //AllowMultipleOrigins;
 .AllowAnyOrigin() //AllowAllOrigins;
                   //.WithMethods("GET") //AllowSpecificMethods;
                   //.WithMethods("GET", "PUT") //AllowSpecificMethods;
                   //.WithMethods("GET", "PUT", "POST") //AllowSpecificMethods;
 .WithMethods("GET", "PUT", "POST", "DELETE") //AllowSpecificMethods;
                                              //.AllowAnyMethod() //AllowAllMethods;

 //.WithHeaders("Accept", "Content-type", "Origin", "X-Custom-Header");  //AllowSpecificHeaders;
 .AllowAnyHeader(); //AllowAllHeaders;
   })
);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseMvc();
            app.UseCors("AllowCors");
        }
    }
}
