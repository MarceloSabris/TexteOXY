using Autofac;
using OXY.Net.Framework.DependencyManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OXY.Teste.RegisterAutoFac
{
    public class RegisterDependencyOXYLibrary : RegisterDependencyBase
    {
        public override void Register(ContainerBuilder builder)
        {
            base.Register(builder);

        }
    }
}
