using OXY.Net.Framework.MongoDB;
using OXY.Teste.Veiculos.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OXY.Teste.Veiculos.DAO
{
    /// <summary>
    /// Classe veiculosDa feita como abstrata para não ser chamada direta
    /// </summary>
    public abstract class VeiculoDA
    {
        
        private const int QUANTIDADE_MAXIMA_REGISTROS = 1000;
        IMongoHelper<VeiculoEN, String> mongoHelper;
        public VeiculoDA()
        {
             mongoHelper =
               new MongoHelper<VeiculoEN, String>();
        }

        public void Save(VeiculoEN veiculo) => mongoHelper.Save(veiculo);

        public VeiculoEN GetVeiculo(string idVeiculo) => mongoHelper.GetByID(idVeiculo);

        public List<VeiculoEN> GetVeiculos() => mongoHelper.GetAll().ToList();

    }
}
