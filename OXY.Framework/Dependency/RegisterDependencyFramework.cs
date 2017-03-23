using Autofac;
using Autofac.Features.Variance;

//using EasyNetQ;
using Autofac.Core;


namespace OXY.Net.Framework.DependencyManager
{
    public class RegisterDependencyFramework : RegisterDependencyBase
    {
        public override void Register(ContainerBuilder builder)
        {
            base.Register(builder);
          //  RegisterRabbitConfig(builder);
        }

        //private void RegisterRabbitConfig(ContainerBuilder builder)
        //{
        //    builder.RegisterType<RabbitMqConfig>().AsSelf().WithProperty(new NamedPropertyParameter("ConnectionString", "global.rabbitMqConnectionString".GetStringAppSettingsConfig())).InstancePerDependency();
        //    builder.RegisterType<AdvancedBusBuilder>().AsSelf().InstancePerDependency();
        //    builder.Register<IAdvancedBus>(advanced => advanced.Resolve<AdvancedBusBuilder>().Build()).InstancePerDependency();            
        //}
    }
}