 
using Application.Abstractions.Data;
using Application.Repositories;
using Domain.Todos;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    public readonly ApplicationDbContext _context;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<T> AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        return entity;
    }

    public void UpdateAsync(T entity) => _context.Set<T>().Update(entity);
    public void DeleteAsync(T entity) => _context.Set<T>().Remove(entity);
    public virtual Task<List<T>> GetAllAsync(Guid userId, CancellationToken cancellationToken)
    {
        return  _context.Set<T>().ToListAsync(cancellationToken);
    }

    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
}
