using OXY.Net.Framework.MongoDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace OXY.Teste.Veiculos.Data
{
    public class VeiculoEN : IMongoDBEntity<string>
    {
       public  VeiculoEN()
        {
            FotosVeiculos = new List<string>();
        }
        public string Id
        {
            get;
            set;
        }

        public string Placa
        {
            get; set;
        }
        public string Renavam
        {
            get; set;
        }

        public string NomeProprietario
        {
            get; set;
        }
       public List<String> FotosVeiculos
        {
            get; set;
        }
        public string Cpf
        {
            get; set;
        }
    }
}
