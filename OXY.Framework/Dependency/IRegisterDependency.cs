using Autofac;

namespace OXY.Net.Framework.DependencyManager
{
    public interface IRegisterDependency
    {
        void Register(ContainerBuilder builder);

        int Order { get; }
    }
}
