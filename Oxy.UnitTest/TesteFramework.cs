using NUnit.Framework;
using OXY.Net.Framework.DependencyManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oxy.UnitTest
{
    [TestFixture]
    public class TesteFramework
    {
      
        [Test]
        public void TestRegistroDependencias()
        {
            ContainerManager.Initialize(ContainerRegisterAll.RegistrarDependenciasAll());
        }
    }
    
}
