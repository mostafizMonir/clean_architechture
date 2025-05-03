 
using Domain.Todos;
using Infrastructure.Database;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Hybrid;


namespace Infrastructure.Repository;
internal class HybridCacheRepository : CachedTodoRepository
{
    private readonly Microsoft.Extensions.Caching.Hybrid.HybridCache _hybridCache;
    public HybridCacheRepository(ApplicationDbContext context,
        IDistributedCache distributedCache,
        HybridCache hybridCache) : base(context,  distributedCache)
    {
        _hybridCache = hybridCache;
    }

    public override async Task<List<TodoItem>> GetAllAsync(Guid userId, CancellationToken cancellationToken)
    {
        string key = $"Todos_{userId}";

        return await _hybridCache.GetOrCreateAsync(
            key,
            async cancel => await base.GetAllAsync(userId, cancel),
            new HybridCacheEntryOptions
            {
                Expiration = TimeSpan.FromMinutes(30),
                LocalCacheExpiration = TimeSpan.FromMinutes(5)
            },
            null,
            cancellationToken
        );

    }
}
