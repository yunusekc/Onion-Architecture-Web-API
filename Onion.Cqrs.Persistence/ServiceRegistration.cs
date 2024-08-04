using Microsoft.Extensions.DependencyInjection;
using Onion.Cqrs.Application.Interface;
using Onion.Cqrs.Application.Interface.Context;
using Onion.Cqrs.Persistence.Context;
using Onion.Cqrs.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Cqrs.Persistence
{
    public class ServiceRegistration
    {
        public static void AddPersistenceRegistration(IServiceCollection services)
        {
            services.AddTransient<IDapperContext, DapperContext>();

        } 
    }
}
