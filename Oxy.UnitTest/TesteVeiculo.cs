using NUnit.Framework;
using OXY.Net.Framework.DependencyManager;
using OXY.Teste.Veiculos.Business;
using OXY.Teste.Veiculos.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oxy.UnitTest
{
    [TestFixture]
    public class TesteVeiculo
    {
        [SetUp]
        public void SetUp()
        {
            ContainerManager.Initialize(ContainerRegisterAll.RegistrarDependenciasAll());

        }

        [Test, TestCaseSource("ListInsertVeiculos")]
        public void InsertVeiculoSemImagemMongo(VeiculoEN veiculo)
        {

            var veiculoBU = ContainerManager.GetInstance<IVeiculoBU>();
            veiculoBU.Save(veiculo);

        }

        static object[] ListInsertVeiculos =
       {
        new VeiculoEN[] {new VeiculoEN(){ Cpf = "11111", Id = "1", NomeProprietario = "111", Placa = "111", Renavam = "123", FotosVeiculos= new List<string>() {"aa","bb" } } },
        new VeiculoEN[] {new VeiculoEN(){ Cpf ="22222", Id = "2", NomeProprietario = "2222", Placa = "222", Renavam = "222" } }

       };

        [TestCase("1")]
        [TestCase("2")]
        public void consulatarProdutoTeste(string id)
        {
            var veiculoBU = ContainerManager.GetInstance<IVeiculoBU>();
            var veiculoEN = veiculoBU.GetVeiculo(id);
            if (veiculoEN == null)
                   Assert.Fail("Não foi feito a pesquisa");
        }

        [Test]
        public void ConsultarLista()
        {
            var veiculoBU = ContainerManager.GetInstance<IVeiculoBU>();
            var lstVeiculoEN = veiculoBU.GetVeiculos();
            if (lstVeiculoEN.Count == 0)
                Assert.Fail("Não foi encontrado nada ");
        } 



    }
}
