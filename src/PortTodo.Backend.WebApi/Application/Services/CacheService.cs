using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using PortTodo.Backend.WebApi.Extensions;

namespace PortTodo.Backend.WebApi.Application.Services
{
    public interface ICacheService
    {
        Task SetCache<T>(string recordId, T data);
        Task<T> GetCache<T>(string recordId);
        void ClearCache(string recordId);
    }
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _cache;

        public CacheService(IDistributedCache cache)
        {
            _cache = cache;
        }
        
        public async Task SetCache<T>(string recordId, T data)
        {
            await _cache.SetRecordAsync(recordId, data);
        }

        public async Task<T> GetCache<T>(string recordId)
        {
            return await _cache.GetRecordAsync<T>(recordId);
        }

        public void ClearCache(string recordId)
        {
            _cache.Remove(recordId);
        }
    }
}