using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using FN.Store.Data.EF;
using FN.Store.DI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FN.Store.Api
{
    public class Startup
    {
        
        public void ConfigureServices(IServiceCollection services)
        {
            //string connectionSrting = Configuration.GetConnectionString("ApplicationContext");
            //services.AddDbContext<StoreDataContext>((optBuilder) => { optBuilder.UseSqlServer(connectionSrting); });
            services.AddMvc().AddJsonOptions( options => 
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            services.AddDependencies();

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            //serviceProvider.GetService<DbInitializer>().PreencherBanco();
        }
    }
}
