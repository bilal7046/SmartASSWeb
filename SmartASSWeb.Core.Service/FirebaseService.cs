using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace SmartASSWeb.Core.Service
{
    public class FirebaseService
        : IFirebaseService
    {
        private readonly FirestoreDb _db;

        public FirebaseService(IKeyFileResolver fileResolver)
        {
            //Firestore Connect
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", fileResolver.GetKeyFilePath());

            _db = FirestoreDb.Create("smartass-28ae3");
        }

        /// <summary>
        /// Get a collection of documents
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetCollection<T>(string name)
        {
            var col = _db.Collection(name);
            QuerySnapshot snapshot = await col.GetSnapshotAsync();
            var data = new List<T>();
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                var doc = document.ConvertTo<T>();
                data.Add(doc);
            }

            return data;
        }

        /// <summary>
        /// Get a collection of documents
        /// </summary>
        /// <param name="name"></param>
        /// <param name="fieldPath"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetCollection<T>(string name, string fieldPath, string value)
        {
            var col = _db.Collection(name);
            Query query = col.WhereEqualTo(fieldPath, value);
            QuerySnapshot snapshot = await query.GetSnapshotAsync();
            var data = new List<T>();
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                var doc = document.ConvertTo<T>();
                data.Add(doc);
            }

            return data;
        }

        /// <summary>
        /// Get a collection of documents
        /// </summary>
        /// <param name="name"></param>
        /// <param name="arrayName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetCollectionWhereArrayContains<T>(string name, string arrayName, string value)
        {
            var col = _db.Collection(name);
            Query query = col.WhereArrayContains(arrayName, value);
            QuerySnapshot snapshot = await query.GetSnapshotAsync();
            var data = new List<T>();
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                var doc = document.ConvertTo<T>();
                data.Add(doc);
            }

            return data;
        }

        /// <summary>
        /// Get a collection of documents
        /// </summary>
        /// <param name="name"></param>
        /// <param name="arrayName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetCollectionWhereArrayContainsAny<T>(string name, string arrayName, IEnumerable<string> values)
        {
            var col = _db.Collection(name);
            Query query = col.WhereArrayContainsAny(arrayName, values);
            QuerySnapshot snapshot = await query.GetSnapshotAsync();
            var data = new List<T>();
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                var doc = document.ConvertTo<T>();
                data.Add(doc);
            }

            return data;
        }

        /// <summary>
        /// Get a matching document by Id
        /// </summary>
        /// <param name="name">Firestore Table</param>
        /// <param name="id">Document ID</param>
        /// <returns></returns>
        public async Task<T> Get<T>(string name, string id)
        {
            var docRef = _db.Collection(name).Document(id);
            DocumentSnapshot document = await docRef.GetSnapshotAsync();
            return document.ConvertTo<T>();
        }

        /// <summary>
        /// Get a matching document
        /// </summary>
        /// <param name="name"></param>
        /// <param name="fieldPath"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<T> Get<T>(string name, string fieldPath, string value)
        {
            var col = _db.Collection(name);
            Query query = col.WhereEqualTo(fieldPath, value);
            QuerySnapshot snapshot = await query.GetSnapshotAsync();
            var data = new List<T>();
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                var doc = document.ConvertTo<T>();
                data.Add(doc);
            }

            return data.FirstOrDefault();
        }

        /// <summary>
        /// Adds new object with AutoID
        /// </summary>
        /// <param name="name"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task Add(string name, object obj)
        {
            var col = _db.Collection(name);
            await col.AddAsync(obj);
        }

        /// <summary>
        /// Adds new object with CustomID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task Add(string id, string name, object obj)
        {
            try
            {
                var col = _db.Collection(name).Document(id);
                await col.SetAsync(obj);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Adds new collection to document
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        public async Task AddCollection(string id, string name, IEnumerable<object> collection)
        {
            var col = _db.Collection(name).Document(id);
            await col.SetAsync(collection);
        }

        /// <summary>
        /// Update object by ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task Update(string id, string name, Dictionary<string, object> obj)
        {
            var col = _db.Collection(name).Document(id);
            await col.UpdateAsync(obj);
        }

        /// <summary>
        /// Update objects by ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="fieldPath"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task Update(string id, string name, string fieldPath, string value)
        {
            var col = _db.Collection(name).Document(id);
            await col.UpdateAsync(fieldPath, value);
        }

        /// <summary>
        /// Update objects by ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="fieldPath"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task Update(string id, string name, string fieldPath, object value)
        {
            var col = _db.Collection(name).Document(id);
            await col.UpdateAsync(fieldPath, value);
        }

        /// <summary>
        /// Delete object by ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task Delete(string id, string name)
        {
            var col = _db.Collection(name).Document(id);
            await col.DeleteAsync();
        }
    }
}
