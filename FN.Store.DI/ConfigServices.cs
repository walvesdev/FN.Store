using FN.Store.Data.EF;
using FN.Store.Data.EF.Repositories;
using FN.Store.Domain.Contracts.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FN.Store.DI
{
    public static class ConfigServices
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            //por requisição
            services.AddScoped<StoreDataContext>();
       
            //por chamada
            //services.AddTransient<IProdutoRepository, ProdutoRepositoryADO>();
            services.AddTransient<ICategoriaRepository, CategoriaRepositoryADO>();
            services.AddTransient<DbInitializer>(); 
            services.AddTransient<ProdutoRepositoryADO>();

        }
    }
}
