using Autofac;
using DataLayer;
using DataLayer.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Services;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Infrastruture
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, IConfiguration configuration)
        {
            // CustomerPortal Context
            var customerPortalConnectionString = configuration.GetConnectionString("DataContext");
            var optionBuilder = new DbContextOptionsBuilder<DataContext>();
            optionBuilder.UseSqlServer(customerPortalConnectionString, sqlServerOptions => sqlServerOptions.CommandTimeout(120));
            builder.Register<IDbContext>(c => new DataContext(optionBuilder.Options)).InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            builder.RegisterType<BookServices>().As<IBookServices>().InstancePerLifetimeScope();
            builder.RegisterType<BorrowerServices>().As<IBorrowerServices>().InstancePerLifetimeScope();
        }
    }
}
