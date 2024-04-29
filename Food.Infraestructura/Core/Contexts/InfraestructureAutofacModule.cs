using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Food.Infraestructura.Core.Contexts
{
    public class InfraestructureAutofacModule : Autofac.Module //un módulo de configuración de Autofac
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            // estás registrando todas las clases que implementan interfaces como implementaciones de esas interfaces.
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            .AsImplementedInterfaces()
            //Autofac proporcionará la misma instancia de una clase registrada en lugar de crear una nueva instancia cada vez que se solicite.
            .InstancePerLifetimeScope();
        }

    }
}
