using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OXY.Net.Framework.Extender
{
    public static class MongoExtender
    {
        public static void AddCondicaoFlags(this IList<IMongoQuery> condicoes, string campo, bool? flag)
        {
            if (flag.HasValue)
            {
                condicoes.Add(Query.EQ(campo, new BsonBoolean(flag.Value)));
            }
        }

        public static void AddCondicaoData(this IList<IMongoQuery> condicoes, string campo, string valorData, bool maior)
        {
            if (valorData != null && valorData != string.Empty)
            {
                if (maior)
                {
                    condicoes.Add(Query.GTE(campo, valorData.FormatStringToDateJson()));
                }
                else
                {
                    condicoes.Add(Query.LTE(campo, valorData.FormatStringToDateJson()));
                }
            }
        }

        public static void AddCondicaoInteiro(this IList<IMongoQuery> condicoes, string campo, int? valor)
        {
            if (valor.HasValue)
            {
                if (valor.Value > 0)
                {
                    condicoes.Add(Query.EQ(campo, valor));
                }
            }
        }

        public static void AddCondicaoValor(this IList<IMongoQuery> condicoes, string campo, double? valor, bool maior)
        {
            if (valor.HasValue)
            {
                if (valor.Value > 0)
                {
                    if (maior)
                    {
                        condicoes.Add(Query.GT(campo, valor));
                    }
                    else
                    {
                        condicoes.Add(Query.LT(campo, valor));
                    }
                }
            }
        }

        public static void AddCondicaoNotIn(this IList<IMongoQuery> condicoes, string campo, IEnumerable<BsonValue> listaValores)
        {
            condicoes.Add(Query.NotIn(campo, listaValores));
        }

        public static void AddCondicaoIn(this IList<IMongoQuery> condicoes, string campo, IEnumerable<BsonValue> listaValores)
        {
            condicoes.Add(Query.In(campo, listaValores));
        }

        public static void AddCondicaoDataVazia(this IList<IMongoQuery> condicoes, string campo)
        {
            condicoes.Add(Query.EQ(campo, DateTime.MinValue));
        }

        public static void AddFiltro(this IList<IMongoQuery> condicoes, string campo, string valor, bool isCaseIn = false)
        {
            if (!string.IsNullOrEmpty(valor))
            {
                if (isCaseIn)
                {
                    condicoes.Add(Query.Matches(campo, new BsonRegularExpression(valor, "i")));
                }
                else
                {
                    condicoes.Add(Query.EQ(campo, valor));
                }
            }
            
        }

        public static void AddFiltro(this IDictionary<string, string> condicoes, string campo, string valor)
        {
            if (!string.IsNullOrEmpty(valor))
                condicoes.Add(campo, valor);
        }
    }
}
