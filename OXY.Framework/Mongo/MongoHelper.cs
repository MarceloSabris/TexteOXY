using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using OXY.Net.Framework.Extender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace OXY.Net.Framework.MongoDB
{
    //criado uma classe facede para encapsular as funcionalidades do mongo 
    // caso um dia troque o driver do mongo fica fácil para adputar o sistema 
    // ela implementa a interface imongohelper 
    public class MongoHelper<T, ID> : IMongoHelper<T, ID> where T : global::OXY.Net.Framework.MongoDB.IMongoDBEntity<ID>
    {
        private const string _ConstMongoDBConnectionString = "ConexaoMongo";

        public static MongoDatabase _mongoDB;
        private static MongoClient mongoClient;
        private static MongoServer mongoServer;

        public MongoCollection<T> MongoDBColletion { get; set; }

        public string _collectionName
        {
            get
            {
                var discriminator = typeof(T).Name;
                var attr = typeof(T).GetCustomAttributes(typeof(BsonDiscriminatorAttribute), true).FirstOrDefault();
                if(attr != null)
                {
                    discriminator = ((BsonDiscriminatorAttribute)attr).Discriminator;
                }
                return discriminator;
            }
        }

       

        public MongoHelper()
        {
            this.Init(_ConstMongoDBConnectionString.ConnectionString());
        }

        private void Init(string pConnectionstring)
        {
            if (String.IsNullOrEmpty(pConnectionstring))
                throw new ArgumentNullException("Connectionstring do mongo, não configurada.");
            try
            {
                if (mongoClient == null)
                {
                    var mongoURL = MongoUrl.Create(pConnectionstring);

                    mongoClient = new MongoClient(pConnectionstring);
                    mongoServer = mongoClient.GetServer();
                    _mongoDB = mongoServer.GetDatabase(mongoURL.DatabaseName);
                }
                MongoDBColletion = _mongoDB.GetCollection<T>(_collectionName);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao inicializar as configurações do MongoDB: " + e.Message, e.InnerException);
            }
        }

        public virtual void CreateIndex(string[] keyNames)
        {
            MongoDBColletion.CreateIndex(keyNames);
        }

        public virtual void CreateIndex(string[] keyNames, bool descending)
        {
            CreateIndex(keyNames, descending, false);
        }

        public virtual void CreateIndex(string[] keyNames, bool descending, bool unique)
        {
            var options = IndexOptions.SetUnique(unique);
            if (descending)
                MongoDBColletion.CreateIndex(IndexKeys.Descending(keyNames), options);
            else
                MongoDBColletion.CreateIndex(IndexKeys.Ascending(keyNames), options);
        }

        public virtual T GetByCondition(Expression<Func<T, bool>> condition)
        {
            return MongoDBColletion.AsQueryable().Where(condition).FirstOrDefault();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return MongoDBColletion.FindAll();
        }

        public IEnumerable<T> GetAll(int maxresult)
        {
            return MongoDBColletion.AsQueryable().Take(maxresult);
        }

        public virtual IEnumerable<T> GetAll(Expression<Func<T, bool>> condition)
        {
            return MongoDBColletion.AsQueryable().Where(condition).ToList();
        }

        public virtual IEnumerable<T> GetAll(Expression<Func<T, bool>> condition, int maxresult)
        {
            return this.GetAll(condition, maxresult, false);
        }

        public virtual IEnumerable<T> GetAll(Expression<Func<T, bool>> condition, int maxresult, bool orderByDescending)
        {
            var query = this.GetAll(condition);

            if (orderByDescending)
                return query.OrderByDescending(x => x.Id).Take(maxresult);
            else
                return query.OrderBy(x => x.Id).Take(maxresult);
        }

        public virtual IEnumerable<T> GetAll(int page, int pagesize, out long foundedRecords, IList<string> orderByFieldAsc, IList<string> orderByFieldDesc)
        {
            var cursor = MongoDBColletion.FindAll();

            foundedRecords = MongoDBColletion.FindAll().Count();

            return OrderAndPaginateCursor(cursor, orderByFieldAsc, orderByFieldDesc, page, pagesize);
        }

        protected void Validate(T mongoObject)
        {
            if (mongoObject == null)
                throw new ArgumentNullException("mongoObject");
        }

        public virtual T Save(T mongoObject)
        {
            this.Validate(mongoObject);
            
            MongoDBColletion.Save(mongoObject);
            return mongoObject;
        }

        public virtual void Delete()
        {
            MongoDBColletion.RemoveAll();
        }

        public virtual void Delete(IMongoQuery condition)
        {
            MongoDBColletion.Remove(condition);
        }

        public virtual long Count(Expression<Func<T, bool>> condition)
        {            
            return MongoDBColletion.AsQueryable().Where(condition).LongCount();
        }

        public virtual long Count()
        {
            return MongoDBColletion.Count();
        }

        private IEnumerable<T> OrderByQuery<Tkey>(int pagesize, int page, Func<T, Tkey> orderByClause, bool orderByDescending, IQueryable<T> query)
        {
            if (orderByClause == null)
            {
                if (orderByDescending)
                    return query.OrderByDescending(x => x.Id).Skip(pagesize * (page - 1)).Take(pagesize);
                else
                    return query.OrderBy(x => x.Id).Skip(pagesize * (page - 1)).Take(pagesize);
            }
            else
            {
                if (orderByDescending)
                    return query.OrderByDescending(orderByClause).Skip(pagesize * (page - 1)).Take(pagesize);
                else
                    return query.OrderBy(orderByClause).Skip(pagesize * (page - 1)).Take(pagesize);
            }
        }

        private IEnumerable<T> Paginate(IEnumerable<T> list, int page, int pagesize)
        {
            return list.Skip(pagesize * (page - 1)).Take(pagesize);
        }

        public virtual IEnumerable<T> Paginate<Tkey>(Expression<Func<T, bool>> condition, int pagesize, int page, Func<T, Tkey> orderByClause, bool orderByDescending)
        {
            var query = MongoDBColletion.AsQueryable().Where(condition);
            return OrderByQuery<Tkey>(pagesize, page, orderByClause, orderByDescending, query);
        }

        public virtual IEnumerable<T> Paginate<Tkey>(Expression<Func<T, bool>> condition, int pagesize, int page, Func<T, Tkey> orderByClause)
        {
            return this.Paginate<Tkey>(condition, pagesize, page, orderByClause, false);
        }

        public virtual IEnumerable<T> Paginate<Tkey>(Expression<Func<T, bool>> condition, int pagesize, int page)
        {
            return this.Paginate<Tkey>(condition, pagesize, page, null, false);
        }

        public virtual IEnumerable<T> Paginate<Tkey>(int pagesize, int page, Func<T, Tkey> orderByClause, bool orderByDescending)
        {
            var query = MongoDBColletion.AsQueryable();
            return OrderByQuery<Tkey>(pagesize, page, orderByClause, orderByDescending, query);
        }

        public virtual IEnumerable<T> Paginate<Tkey>(int pagesize, int page, Func<T, Tkey> orderByClause)
        {
            return this.Paginate<Tkey>(pagesize, page, orderByClause, false);
        }

        public virtual IEnumerable<T> Paginate<Tkey>(int pagesize, int page)
        {
            return this.Paginate<Tkey>(pagesize, page, null, false);
        }

        public IEnumerable<T> Search(string field, string search, int page, int pagesize, out long foundedRecords)
        {
            var query = Query.Matches(field, new BsonRegularExpression(search, "i"));

            var totallist = MongoDBColletion.Find(query).ToList();

            foundedRecords = totallist.Count;

            return totallist.Skip(pagesize * (page - 1)).Take(pagesize).ToList();
        }

        protected virtual IEnumerable<T> InternalSearch<Tkey>(IList<IMongoQuery> search, int page, int pagesize, out long foundedRecords, Func<T, Tkey> orderByClause, bool orderByDescending, MongoDBQueryCondition queryCondition)
        {
            var query = queryCondition.Equals(MongoDBQueryCondition.eQueryOr) ? Query.Or(search) : Query.And(search);
            var totallist = MongoDBColletion.Find(query).AsQueryable();

            foundedRecords = totallist.ToList().Count();
            return OrderByQuery<Tkey>(pagesize, page, orderByClause, orderByDescending, totallist);
        }

        public IEnumerable<T> SearchAnd<Tkey>(IList<IMongoQuery> search, int page, int pagesize, out long foundedRecords, Func<T, Tkey> orderByClause, bool orderByDescending)
        {
            return this.InternalSearch<Tkey>(search, page, pagesize, out foundedRecords, orderByClause, orderByDescending, MongoDBQueryCondition.eQueryAnd);
        }

        public IEnumerable<T> SearchAnd<Tkey>(IList<IMongoQuery> search, int page, int pagesize, out long foundedRecords, Func<T, Tkey> orderByClause)
        {
            return this.SearchAnd<Tkey>(search, page, pagesize, out foundedRecords, orderByClause, false);
        }

        public IEnumerable<T> SearchAnd<Tkey>(IList<IMongoQuery> search, int page, int pagesize, out long foundedRecords)
        {
            return this.SearchAnd<Tkey>(search, page, pagesize, out foundedRecords, null, false);
        }

        public IEnumerable<T> SearchAnd<Tkey>(IDictionary<string, string> keysAndValues, int page, int pagesize, out long foundedRecords, Func<T, Tkey> orderByClause, bool orderByDescending)
        {
            return this.SearchAnd<Tkey>(GetQueries(keysAndValues), page, pagesize, out foundedRecords, orderByClause, orderByDescending);
        }

        public IEnumerable<T> SearchAnd<Tkey>(IDictionary<string, string> keysAndValues, int page, int pagesize, out long foundedRecords, Func<T, Tkey> orderByClause)
        {
            return this.SearchAnd<Tkey>(keysAndValues, page, pagesize, out foundedRecords, orderByClause, false);
        }

        public IEnumerable<T> SearchAnd<Tkey>(IDictionary<string, string> keysAndValues, int page, int pagesize, out long foundedRecords)
        {
            return this.SearchAnd<Tkey>(keysAndValues, page, pagesize, out foundedRecords, null, false);
        }

        public IEnumerable<T> SearchAnd<Tkey>(IDictionary<string, string> keysAndValues, int page, int pagesize,
            out long foundedRecords, IList<IMongoQuery> additionalCondition)
        {
            var query = GetQueries(keysAndValues, additionalCondition);
            return this.InternalSearch<Tkey>(query, page, pagesize, out foundedRecords, null, false, MongoDBQueryCondition.eQueryAnd);
        }
               
        public IEnumerable<T> SearchAnd<Tkey>(IDictionary<string, string> keysAndValues, int page, int pagesize,
            out long foundedRecords, IList<IMongoQuery> additionalCondition, IList<string> orderByFieldAsc, IList<string> orderByFieldDesc)
        {
            var query = GetQueries(keysAndValues, additionalCondition);

            foundedRecords = MongoDBColletion.Find(Query.And(query)).Count();

            var cursor = MongoDBColletion.Find(Query.And(query));
            return OrderAndPaginateCursor(cursor, orderByFieldAsc, orderByFieldDesc, page, pagesize);
        }

       

        public IEnumerable<T> SearchAnd<Tkey>(IList<IMongoQuery> search, int page, int pagesize,
            out long foundedRecords,IList<string> orderByFieldAsc, IList<string> orderByFieldDesc)
        {
            bool fieldDesc = ((orderByFieldDesc != null) && (orderByFieldDesc.Count > 0));
            bool fieldAsc = ((orderByFieldAsc != null) && (orderByFieldAsc.Count > 0));

            IMongoSortBy orderby = null;

            if ((fieldAsc) && (fieldDesc))
                orderby = SortBy.Descending(orderByFieldDesc.ToArray()).Ascending(orderByFieldAsc.ToArray());
            else if (fieldAsc)
                orderby = SortBy.Ascending(orderByFieldAsc.ToArray());
            else if (fieldDesc)
                orderby = SortBy.Descending(orderByFieldDesc.ToArray());
            else
                orderby = SortBy.Null;


            var cursor = MongoDBColletion.Find(Query.And(search)).SetSkip(pagesize * (page - 1)).SetLimit(pagesize).SetSortOrder(orderby);

            foundedRecords = MongoDBColletion.Find(Query.And(search)).Count();

            return cursor;
        }

       

        public IEnumerable<T> SearchAnd<Tkey>(IDictionary<string, string> keysAndValues,
            IList<IMongoQuery> additionalCondition, IList<string> orderByFieldAsc, IList<string> orderByFieldDesc,
            int maxLines, out int rowsAffected)
        {
            var query = GetQueries(keysAndValues, additionalCondition);
            rowsAffected = MongoDBColletion.Find(Query.And(query)).AsQueryable().Count();

            var cursor = MongoDBColletion.Find(Query.And(query));

            if (maxLines > 0)
                return OrderCursor(cursor, orderByFieldAsc, orderByFieldDesc).Take(maxLines);
            else            
                return OrderCursor(cursor, orderByFieldAsc, orderByFieldDesc);
        }

        private MongoCursor<T> OrderCursor(MongoCursor<T> cursor, IList<string> orderByFieldAsc, IList<string> orderByFieldDesc)
        {            
            bool fieldDesc = ((orderByFieldDesc != null) && (orderByFieldDesc.Count > 0));
            bool fieldAsc = ((orderByFieldAsc != null) && (orderByFieldAsc.Count > 0));
            
            if ((fieldAsc) && (fieldDesc))
                return cursor.SetSortOrder(SortBy.Descending(orderByFieldDesc.ToArray()).Ascending(orderByFieldAsc.ToArray()));
            else if (fieldAsc)
                return cursor.SetSortOrder(SortBy.Ascending(orderByFieldAsc.ToArray()));
            else if (fieldDesc)
                return cursor.SetSortOrder(SortBy.Descending(orderByFieldDesc.ToArray()));
            else
                return cursor;
        }

        private IEnumerable<T> OrderAndPaginateCursor(MongoCursor<T> cursor, IList<string> orderByFieldAsc, IList<string> orderByFieldDesc, int page, int pagesize)
        {
            return Paginate(OrderCursor(cursor, orderByFieldAsc, orderByFieldDesc), page, pagesize);
        }

        public IEnumerable<T> SearchOr<Tkey>(IList<IMongoQuery> search, int page, int pagesize, out long foundedRecords, Func<T, Tkey> orderByClause, bool orderByDescending)
        {
            return this.InternalSearch<Tkey>(search, page, pagesize, out foundedRecords, orderByClause, orderByDescending, MongoDBQueryCondition.eQueryOr);
        }

        public IEnumerable<T> SearchOr<Tkey>(IList<IMongoQuery> search, int page, int pagesize, out long foundedRecords, Func<T, Tkey> orderByClause)
        {
            return this.SearchOr<Tkey>(search, page, pagesize, out foundedRecords, orderByClause, false);
        }

        public IEnumerable<T> SearchOr<Tkey>(IList<IMongoQuery> search, int page, int pagesize, out long foundedRecords)
        {
            return this.SearchOr<Tkey>(search, page, pagesize, out foundedRecords, null, false);
        }

        public IEnumerable<T> SearchOr<Tkey>(IDictionary<string, string> keysAndValues, int page, int pagesize, out long foundedRecords, Func<T, Tkey> orderByClause, bool orderByDescending)
        {
            return this.SearchOr<Tkey>(GetQueries(keysAndValues), page, pagesize, out foundedRecords, orderByClause, orderByDescending);
        }

        public IEnumerable<T> SearchOr<Tkey>(IDictionary<string, string> keysAndValues, int page, int pagesize, out long foundedRecords, Func<T, Tkey> orderByClause)
        {
            return this.SearchOr<Tkey>(keysAndValues, page, pagesize, out foundedRecords, orderByClause, false);
        }

        public IEnumerable<T> SearchOr<Tkey>(IDictionary<string, string> keysAndValues, int page, int pagesize, out long foundedRecords)
        {
            return this.SearchOr<Tkey>(keysAndValues, page, pagesize, out foundedRecords, null, false);
        }

        public IEnumerable<T> SearchOr<Tkey>(IDictionary<string, string> keysAndValues, int page, int pagesize, out long foundedRecords, IList<IMongoQuery> additionalCondition)
        {
            var query = GetQueries(keysAndValues, additionalCondition);
            return this.InternalSearch<Tkey>(query, page, pagesize, out foundedRecords, null, false, MongoDBQueryCondition.eQueryOr);
        }

        public List<IMongoQuery> GetQueries(IDictionary<string, string> keysAndValues, IList<IMongoQuery> additionalCondition)
        {
            List<IMongoQuery> query = GetQueries(keysAndValues);
            if ((additionalCondition != null) && (additionalCondition.Count > 0))
                query.AddRange(additionalCondition);
            return query;
        }

         public List<IMongoQuery> GetQueries(IDictionary<string, string> keysAndValues)
        {
            return GetQueries(keysAndValues, MongoDBQueryCondition.eQueryEQ);
        }

        //Preparado para utilizar todas as condições ("keysAndValues") com LessThan, GreaterThan e Matches. Por enquanto utilizando somente chamada com Equals.        
        public List<IMongoQuery> GetQueries(IDictionary<string, string> keysAndValues, MongoDBQueryCondition eQuery)
        {
            var queries = new List<IMongoQuery>();
            foreach (var key in keysAndValues.Keys)
            {
                switch (eQuery)
                {
                    case MongoDBQueryCondition.eQueryEQ:
                        queries.Add(Query.EQ(key, keysAndValues[key]));
                        break;
                    case MongoDBQueryCondition.eQueryLT:
                        queries.Add(Query.LT(key, keysAndValues[key]));
                        break;
                    case MongoDBQueryCondition.eQueryGT:
                        queries.Add(Query.GT(key, keysAndValues[key]));
                        break;
                    case MongoDBQueryCondition.eQueryMatches:
                        queries.Add(Query.Matches(key, new BsonRegularExpression(keysAndValues[key], "i")));
                        break;
                    default: continue;
                }
            }
            return queries;
        }

        public virtual IEnumerable<T> GreaterThan(string field, string value)
        {
            var query = Query.GT(field, value);

            return MongoDBColletion.Find(query);
        }

        public virtual IEnumerable<T> LessThan(string field, DateTime value)
        {
            var query = Query.LT(field, new DateTime(value.Year, value.Month, 1, 0, 0, 0, 0, DateTimeKind.Utc));

            return MongoDBColletion.Find(query);
        }

        public T GetByID(ID id)
        {
            return GetByCondition(i => i.Id.Equals(id));
        }       
    }
}
