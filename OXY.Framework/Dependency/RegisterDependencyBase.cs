using System.Linq;
using Autofac;
using Autofac.Features.Variance;

namespace OXY.Net.Framework.DependencyManager
{
    public abstract class RegisterDependencyBase : IRegisterDependency
    {
        public int Order
        {
            get
            {
                return 1;
            }
        }

        public virtual void Register(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(GetType().Assembly).Where(t => t.Name.EndsWith("BU")).AsImplementedInterfaces().AsSelf().InstancePerDependency();
            builder.RegisterAssemblyTypes(GetType().Assembly).Where(t => t.Name.EndsWith("DA")).AsSelf().InstancePerDependency();
             builder.RegisterAssemblyTypes(GetType().Assembly).Where(t => t.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerDependency();
               builder.RegisterAssemblyTypes(GetType().Assembly).Where(t => t.Name.EndsWith("Listener")).AsSelf().SingleInstance();
            builder.RegisterAssemblyTypes(GetType().Assembly).Where(t => t.Name.EndsWith("Parser")).AsSelf().InstancePerDependency();
            builder.RegisterAssemblyTypes(GetType().Assembly).Where(t => t.Name.EndsWith("Helper")).AsImplementedInterfaces().AsSelf().InstancePerDependency();
            builder.RegisterAssemblyTypes(GetType().Assembly).Where(t => t.Name.EndsWith("Builder")).AsSelf().InstancePerDependency();

            builder.RegisterAssemblyTypes(GetType().Assembly).Where(t => t.Name.EndsWith("MapperConfiguration")).AsSelf().InstancePerDependency();
            builder.RegisterSource(new ContravariantRegistrationSource());
            
        }
    }
}
