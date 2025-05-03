using Application.Abstractions.Authentication;
using Application.Abstractions.Messaging;
using Application.Repositories;
using Domain.Todos;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Todos.Get;

internal sealed class GetTodosQueryHandler(
    IUserContext userContext,
    IRepository<TodoItem> repository)
    : IQueryHandler<GetTodosQuery, List<TodoResponse>>
{

    public async Task<Result<List<TodoResponse>>> Handle(GetTodosQuery query, CancellationToken cancellationToken)
    {
        if (query.UserId != userContext.UserId)
        {
            return Result.Failure<List<TodoResponse>>(UserErrors.Unauthorized());
        }

        List<TodoItem> todos = await repository.GetAllAsync(query.UserId, cancellationToken);
        var response = todos.Select(todoItem => new TodoResponse
        {
            Id = todoItem.Id,
            UserId = todoItem.UserId,
            Description = todoItem.Description,
            DueDate = todoItem.DueDate,
            Labels = todoItem.Labels,
            IsCompleted = todoItem.IsCompleted,
            CreatedAt = todoItem.CreatedAt,
            CompletedAt = todoItem.CompletedAt
        }).ToList();

        // Return as a successful result
        return Result.Success(response);

        // var response = todos.Select(t => new TodoResponse
        // {
        //     Id = t.Id,
        //     Title = t.Title,
        //     IsCompleted = t.IsCompleted
        // }).ToList();
        //
        // return Result.Ok(response);


        // todos = await context.TodoItems
        //     .Where(todoItem => todoItem.UserId == query.UserId)
        //     .Select(todoItem => new TodoResponse
        //     {
        //         Id = todoItem.Id,
        //         UserId = todoItem.UserId,
        //         Description = todoItem.Description,
        //         DueDate = todoItem.DueDate,
        //         Labels = todoItem.Labels,
        //         IsCompleted = todoItem.IsCompleted,
        //         CreatedAt = todoItem.CreatedAt,
        //         CompletedAt = todoItem.CompletedAt
        //     })
        //     .ToListAsync(cancellationToken);
        //
        // return todos;
    }
}
