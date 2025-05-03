using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Todos;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;
internal class TodoRepository: Repository<TodoItem>
{
    public TodoRepository(ApplicationDbContext context) : base(context)
    {
    }

    public override async Task<List<TodoItem>> GetAllAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await  _context.TodoItems.Where(x => x.UserId == userId).ToListAsync(cancellationToken);
    }
}
