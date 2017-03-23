using Autofac;
using Autofac.Core;
using System;
using System.Collections.Generic;

namespace OXY.Net.Framework.DependencyManager
{
    /// <summary>
    /// mapper da classe container do Autofac 
    /// caso um dia queira trocar o componente de container é 
    /// só alterar na classe o container , não havendo a necessidade de alterar todo o projeto 
    /// </summary>
    public sealed class ContainerManager
    {
        private static IContainer _container;

        public static IContainer Container
        {
            get
            {
                if (_container == null)
                {
                    throw new Exception("Container não foi inicializado. É necessário chamar o método Initialize antes de utilizar o Container.");
                }
                return _container;
            }
        }

        public static void Initialize(IContainer container)
        {

            _container = container;
        }

        public static ContainerBuilder GetContainerBuilder()
        {
            return new ContainerBuilder();
        }

        public static void RegisterDependency(IRegisterDependency registerDependency)
        {
            var builder = GetContainerBuilder();
            registerDependency.Register(builder);
            builder.Update(Container);
        }

        public static void RegisterDependency(ContainerBuilder builder)
        {
            builder.Update(Container);
        }
       
        public static T GetInstance<T>()
        {
            return Container.Resolve<T>();
        }

        public static T GetInstance<T>(Parameter[] parameters)
        {
            return Container.Resolve<T>(parameters);
        }

        public static T GetInstance<T>(Parameter parameter)
        {
            return Container.Resolve<T>(new List<Parameter>() { parameter });
        }

        public static T GetInstance<T>(IEnumerable<Parameter> parameters)
        {
            return Container.Resolve<T>(parameters);
        }

        public static bool IsRegistered<T>()
        {
            return Container.IsRegistered<T>();
        }
    }
}
