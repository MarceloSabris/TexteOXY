using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OXY.Net.Framework.DependencyManager
{
    public class ReferencedAssemblyLoader : IAssemblyLoader
    {
        public IList<Assembly> GetAssemblies()
        {
            return (from file in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory)
                    where Path.GetExtension(file) == ".dll"
                    select Assembly.LoadFrom(file)).ToList();
        }
    }
}
