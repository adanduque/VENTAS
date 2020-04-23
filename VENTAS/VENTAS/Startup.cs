using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using VENTAS.Models;
using AutoMapper;
using VENTAS.RequestDTO;

namespace VENTAS
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
            services.AddAutoMapper(configuration =>
            configuration.CreateMap<Categoria,CategoriaDTO>
                
                typeof(Startup));

            services.AddDbContext<DBSVentasContext>(options => 
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, ApplicationRole>(options=>
            {
                //si alguien intenta loguearse con mi cuenta de manera no satisfactoria esta se bloquea
                //máximo numero de intentos
                options.Lockout.MaxFailedAccessAttempts = 5;
                //Longuitud requerida del password
                options.Password.RequiredLength = 6;
                //Password requiere numeros
                options.Password.RequireDigit = true;
                //Password requiere letras minusculas
                options.Password.RequireLowercase = true;
                //Password requiere letras mayusculas
                options.Password.RequireUppercase = true;
                //require tres caracteres unicos ex AAA 
                options.Password.RequiredUniqueChars = 3;

                options.Password.RequireNonAlphanumeric = false;

                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<DBSVentasContext>()
                .AddDefaultTokenProviders();
         /*   services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddNewtonosoftJson(options => options.SerializerSetting.
            services.AddControllers();*/
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
