using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OXY.Net.Framework.DependencyManager
{
    public interface IAssemblyLoader
    {
        IList<Assembly> GetAssemblies();
    }
}
