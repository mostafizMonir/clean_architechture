using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Todos;

namespace Application.Repositories;

public interface IRepository<T> where T: class
{
    Task<T> AddAsync(T entity);
    void UpdateAsync(T entity);
    void DeleteAsync(T entity);

    Task<List<T>> GetAllAsync(Guid userId, CancellationToken cancellationToken);
    Task SaveChangesAsync();

    
}
