using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartASSWeb.Core.Service
{
    public interface IFirebaseService
    {
        Task<IEnumerable<T>> GetCollection<T>(string name);

        /// <summary>
        /// Adds new object with AutoID
        /// </summary>
        /// <param name="name"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task Add(string name, object obj);

        /// <summary>
        /// Adds new object with CustomID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task Add(string id, string name, object obj);

        /// <summary>
        /// Adds new collection to document
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        Task AddCollection(string id, string name, IEnumerable<object> collection);

        /// <summary>
        /// Get a collection of documents
        /// </summary>
        /// <param name="name"></param>
        /// <param name="fieldPath"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetCollection<T>(string name, string fieldPath, string value);
        /// <summary>
        /// Get a collection of documents where an array contains an element
        /// </summary>
        /// <param name="name"></param>
        /// <param name="arrayName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetCollectionWhereArrayContains<T>(string name, string arrayName, string value);

        /// <summary>
        /// Get a collection of documents where an array contains an element
        /// </summary>
        /// <param name="name"></param>
        /// <param name="arrayName"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetCollectionWhereArrayContainsAny<T>(string name, string arrayName, IEnumerable<string> values);

        /// <summary>
        /// Get a matching document by Id
        /// </summary>
        /// <param name="name">Firestore Table</param>
        /// <param name="id">Document ID</param>
        /// <returns></returns>
        Task<T> Get<T>(string name, string id);

        /// <summary>
        /// Get a matching document
        /// </summary>
        /// <param name="name"></param>
        /// <param name="fieldPath"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<T> Get<T>(string name, string fieldPath, string value);
        
        /// <summary>
        /// Update object by ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task Update(string id, string name, Dictionary<string, object> obj);

        /// <summary>
        /// Update objects by ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="fieldPath"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task Update(string id, string name, string fieldPath, string value);
        /// <summary>
        /// Update objects by ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="fieldPath"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task Update(string id, string name, string fieldPath, object value);

        /// <summary>
        /// Delete object by ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        Task Delete(string id, string name);
    }
}