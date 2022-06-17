using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Humanizer;
using SmartASSWeb.Core.Service;

namespace SmartASSWeb.Bll._Mock
{
    public class MockFirebaseService
        : IFirebaseService
    {
        private readonly MockDb _db = new MockDb();
        public async Task<IEnumerable<T>> GetCollection<T>(string name)
        {
            return _db.GetTable<T>(name);
        }

        public async Task Add(string name, object obj)
        {
            _db.GetTable(name).Add(obj);
        }

        public async Task Add(string id, string name, object obj)
        {
            throw new System.NotImplementedException();
        }

        public async Task AddCollection(string id, string name, IEnumerable<object> collection)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetCollection<T>(string name, string fieldPath, string value)
        {
            return _db.GetTable<T>(name, t => ((string)t.GetType().GetProperty(fieldPath.Pascalize())?.GetValue(t)) == value);
        }

        public Task<IEnumerable<T>> GetCollectionWhereArrayContains<T>(string name, string arrayName, string value)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetCollectionWhereArrayContainsAny<T>(string name, string arrayName, IEnumerable<string> values)
        {
            throw new NotImplementedException();
        }

        public async Task<T> Get<T>(string name, string id)
        {
            return _db.GetTable<T>(name, t => t.GetType().GetProperties().First(c=> c.Name.EndsWith("Id")).GetValue(t) == id).FirstOrDefault();
        }

        public async Task<T> Get<T>(string name, string fieldPath, string value)
        {
            return _db.GetTable<T>(name, t => ((string)t.GetType().GetProperty(fieldPath.Pascalize())?.GetValue(t)) == value).FirstOrDefault();
        }

        public async Task Update(string id, string name, Dictionary<string, object> obj)
        {
            throw new System.NotImplementedException();
        }

        public async Task Update(string id, string name, string fieldPath, string value)
        {
            throw new System.NotImplementedException();
        }

        public async Task Update(string id, string name, string fieldPath, object value)
        {
            throw new System.NotImplementedException();
        }

        public async Task Delete(string id, string name)
        {
            throw new System.NotImplementedException();
        }
    }
}
