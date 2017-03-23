using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace OXY.Net.Framework.MongoDB
{
    //a interface padrao para ser implementada 
    // toda a classe passada como generics tem que ser do tipo 
    // imongoDBentity pois deve implementar o id
    // ai com o id consigo fazer as buscas necessárias 
    public interface IMongoHelper<T, ID> where T : IMongoDBEntity<ID>
    {
        MongoCollection<T> MongoDBColletion { get; set; }

        T Save(T mongoObject);
        T GetByID(ID id);
        T GetByCondition(Expression<Func<T, bool>> condition);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(int maxresult);
        IEnumerable<T> GetAll(Expression<Func<T, bool>> condition);
        IEnumerable<T> GetAll(Expression<Func<T, bool>> condition, int maxresult);
        IEnumerable<T> GetAll(Expression<Func<T, bool>> condition, int maxresult, bool orderByDescending);
        IEnumerable<T> GetAll(int page, int pagesize, out long foundedRecords, IList<string> orderByFieldAsc, IList<string> orderByFieldDesc);

        IEnumerable<T> Paginate<Tkey>(Expression<Func<T, bool>> condition, int pagesize, int page,
            Func<T, Tkey> orderByClause, bool orderByDescending);

        IEnumerable<T> Paginate<Tkey>(Expression<Func<T, bool>> condition, int pagesize, int page,
            Func<T, Tkey> orderByClause);

        IEnumerable<T> Paginate<Tkey>(Expression<Func<T, bool>> condition, int pagesize, int page);
        IEnumerable<T> Paginate<Tkey>(int pagesize, int page, Func<T, Tkey> orderByClause, bool orderByDescending);
        IEnumerable<T> Paginate<Tkey>(int pagesize, int page, Func<T, Tkey> orderByClause);
        IEnumerable<T> Paginate<Tkey>(int pagesize, int page);
        IEnumerable<T> Search(string field, string search, int page, int pagesize, out long foundedRecords);

        IEnumerable<T> SearchAnd<TKey>(IDictionary<string, string> keysAndValues, int page, int pagesize,
            out long foundedRecords, Func<T, TKey> orderByClause, bool orderByDescending);
              
        IEnumerable<T> SearchAnd<TKey>(IDictionary<string, string> keysAndValues, int page, int pagesize,
            out long foundedRecords, Func<T, TKey> orderByClause);

        IEnumerable<T> SearchAnd<TKey>(IDictionary<string, string> keysAndValues, int page, int pagesize,
            out long foundedRecords);

        IEnumerable<T> SearchAnd<TKey>(IList<IMongoQuery> search, int page, int pagesize, out long foundedRecords,
            Func<T, TKey> orderByClause, bool orderByDescending);

        IEnumerable<T> SearchAnd<TKey>(IList<IMongoQuery> search, int page, int pagesize, out long foundedRecords,
            Func<T, TKey> orderByClause);

        IEnumerable<T> SearchAnd<TKey>(IList<IMongoQuery> search, int page, int pagesize, out long foundedRecords);

        IEnumerable<T> SearchAnd<Tkey>(IDictionary<string, string> keysAndValues, int page, int pagesize,
            out long foundedRecords, IList<IMongoQuery> additionalCondition);

        IEnumerable<T> SearchAnd<Tkey>(IDictionary<string, string> keysAndValues, int page, int pagesize,
            out long foundedRecords, IList<IMongoQuery> additionalCondition, IList<string> orderByFieldAsc, IList<string> orderByFieldDesc);

       
        IEnumerable<T> SearchAnd<Tkey>(IList<IMongoQuery> search, int page, int pagesize,
            out long foundedRecords, IList<string> orderByFieldAsc, IList<string> orderByFieldDesc);

       

        IEnumerable<T> SearchAnd<Tkey>(IDictionary<string, string> keysAndValues,
            IList<IMongoQuery> additionalCondition, IList<string> orderByFieldAsc, IList<string> orderByFieldDesc,
            int maxLines, out int rowsAffected);

        IEnumerable<T> SearchOr<TKey>(IDictionary<string, string> keysAndValues, int page, int pagesize,
            out long foundedRecords, Func<T, TKey> orderByClause, bool orderByDescending);

        IEnumerable<T> SearchOr<TKey>(IDictionary<string, string> keysAndValues, int page, int pagesize,
            out long foundedRecords, Func<T, TKey> orderByClause);

        IEnumerable<T> SearchOr<TKey>(IDictionary<string, string> keysAndValues, int page, int pagesize,
            out long foundedRecords);

        IEnumerable<T> SearchOr<TKey>(IList<IMongoQuery> search, int page, int pagesize, out long foundedRecords,
            Func<T, TKey> orderByClause, bool orderByDescending);

        IEnumerable<T> SearchOr<TKey>(IList<IMongoQuery> search, int page, int pagesize, out long foundedRecords,
            Func<T, TKey> orderByClause);

        IEnumerable<T> SearchOr<TKey>(IList<IMongoQuery> search, int page, int pagesize, out long foundedRecords);

        IEnumerable<T> SearchOr<Tkey>(IDictionary<string, string> keysAndValues, int page, int pagesize,
            out long foundedRecords, IList<IMongoQuery> additionalCondition);

        void Delete(IMongoQuery condition);
        //void Delete(T mongoObject);
        //void Delete(Expression<Func<T, bool>> condition);
        void CreateIndex(string[] keyNames);
        void CreateIndex(string[] keyNames, bool descending);
        void CreateIndex(string[] keyNames, bool descending, bool unique);
        long Count(Expression<Func<T, bool>> condition);
        long Count();
        IEnumerable<T> GreaterThan(string field, string value);
        IEnumerable<T> LessThan(string field, DateTime value);

        List<IMongoQuery> GetQueries(IDictionary<string, string> keysAndValues);
        List<IMongoQuery> GetQueries(IDictionary<string, string> keysAndValues, IList<IMongoQuery> additionalCondition);
        //List<IMongoQuery> GetQueries(IList<IMongoQuery> search, IList<IMongoQuery> additionalCondition);
        List<IMongoQuery> GetQueries(IDictionary<string, string> keysAndValues, MongoDBQueryCondition eQuery);
    }
}