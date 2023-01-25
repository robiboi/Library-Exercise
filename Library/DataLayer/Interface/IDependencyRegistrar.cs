using Autofac;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Interface
{
    public interface IDependencyRegistrar
    { 
        /// <summary>
      /// Register services and interfaces
      /// </summary>
      /// <param name="builder">Container builder</param>
        void Register(ContainerBuilder builder, IConfiguration configuration);
    }
}
