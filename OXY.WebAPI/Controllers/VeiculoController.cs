using OXY.Net.Framework.DependencyManager;
using OXY.Teste.Veiculos.Business;
using OXY.Teste.Veiculos.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OXY.WebAPI.Controllers
{
    public class VeiculoController : ApiController
    {
        IVeiculoBU veiculoBu;

        VeiculoController()
        {
            
            veiculoBu = ContainerManager.GetInstance<IVeiculoBU>();

        }

        // GET api/values
        public List<VeiculoEN> Get()
        {
            return veiculoBu.GetVeiculos();
        }

        // GET api/values/5
        public VeiculoEN Get(string id)
        {
            return veiculoBu.GetVeiculo(id);
        }

        // POST api/values
        public void Post( VeiculoEN veiculo)
        {
            veiculoBu.Save(veiculo);
        }

    }
}
