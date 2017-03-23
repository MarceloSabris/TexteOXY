using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Compilation;

namespace OXY.Net.Framework.DependencyManager
{
    public class BuildManagerLoader : IAssemblyLoader
    {
        public IList<Assembly> GetAssemblies()
        {
            return BuildManager.GetReferencedAssemblies().Cast<Assembly>().ToList();
        }
    }
}
