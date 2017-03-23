using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OXY.Net.Framework.DependencyManager
{
    /// <summary>
    /// classe para registrar todas as classes de uma maneira automatica 
    /// sem ter que se preucar com registro uma a uma 
    /// </summary>
    public static class ContainerRegisterAll
    {
        private static IContainer RegistrarDependencias(IList<IAssemblyLoader> loaders)
        {
            ContainerBuilder builder = new ContainerBuilder();
            AppTypeFinder typeFinder = new AppTypeFinder(loaders);
            var drTypes = typeFinder.FindClassesOfType<IRegisterDependency>();
            var drInstances = new List<IRegisterDependency>();
            foreach (var drType in drTypes)
                drInstances.Add((IRegisterDependency)Activator.CreateInstance(drType));

            drInstances = drInstances.AsQueryable().OrderBy(t => t.Order).ToList();
            foreach (var dependencyRegistrar in drInstances)
                dependencyRegistrar.Register(builder);

            return builder.Build();
        }



        public static IContainer RegistrarDependenciasAll()
        {
            IList<IAssemblyLoader> loaders = new List<IAssemblyLoader>();
            loaders.Add(new AppDomainLoader());
            loaders.Add(new ReferencedAssemblyLoader());
            
            return RegistrarDependencias(loaders);
        }

      
    }
}
