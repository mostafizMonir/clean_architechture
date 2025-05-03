using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Repositories;
using Domain.Todos;
using Infrastructure.Database;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace Infrastructure.Repository;

internal class CachedTodoRepository : TodoRepository
{
    // private readonly IMemoryCache _memoryCache;
     private readonly IDistributedCache _distributedCache;

    public CachedTodoRepository(ApplicationDbContext context, 
       // IMemoryCache memoryCache, 
        IDistributedCache distributedCache
        ) : base(context)
    {
        // _memoryCache = memoryCache;
         _distributedCache = distributedCache;
    }
    public override async Task<List<TodoItem>> GetAllAsync(Guid userId, CancellationToken cancellationToken)
    {
        string key = $"Todos_{userId}";
       
        string cachedTodo = await _distributedCache.GetStringAsync(key,cancellationToken); // redis
       
        List<TodoItem> todos;


        if (string.IsNullOrEmpty(cachedTodo))
        {
              todos =  await base.GetAllAsync(userId, cancellationToken);

             await _distributedCache.SetStringAsync(key, System.Text.Json.JsonSerializer.Serialize(todos), cancellationToken); //redis

            return todos;
        }

        return JsonConvert.DeserializeObject<List<TodoItem>>(cachedTodo);

        // return await  _memoryCache.GetOrCreateAsync(
        //     key,
        //     async entry =>
        //     {
        //         // Set cache expiration options
        //         entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));
        //         // Fetch data from the database
        //         return await base.GetAllAsync(userId);
        //          
        //     }
        // );
       
   }
}
