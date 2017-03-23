using OXY.Teste.Veiculos.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OXY.Teste.Veiculos.Business
{
   public  interface IVeiculoBU
    {
       void Save(VeiculoEN veiculo);
        VeiculoEN GetVeiculo(string idVeiculo);
        List<VeiculoEN> GetVeiculos();
    }
}
