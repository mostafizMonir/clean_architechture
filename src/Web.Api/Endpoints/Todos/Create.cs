using Application.Abstractions.Messaging;
using Application.Todos.Create;
using Domain.Todos;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Todos;

internal sealed class Create : IEndpoint, IEndpointWithoutMediatR
{
    public sealed class Request
    {
        public Guid UserId { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public List<string> Labels { get; set; } = [];
        public int Priority { get; set; }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("todos", async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new CreateTodoCommand
            {
                UserId = request.UserId,
                Description = request.Description,
                DueDate = request.DueDate,
                Labels = request.Labels,
                Priority = (Priority)request.Priority
            };

            Result<Guid> result = (Result<Guid>)await sender.Send(command, cancellationToken);
            if (result == null)
            {
                return Results.Problem("Unexpected null result from command handler.");
            }
            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Todos)
        .RequireAuthorization();
    }

    public void MapEndpointWithoutMediatR(IEndpointRouteBuilder app)
    {
        app.MapPost("todosWithoutMediatr", async (Request request,CancellationToken cancellationToken, ICommandHandler<CreateTodoCommand> createTodoCommandHandler) => {

                var command = new CreateTodoCommand
                {
                    UserId = request.UserId,
                    Description = request.Description,
                    DueDate = request.DueDate,
                    Labels = request.Labels,
                    Priority = (Priority)request.Priority
                };

                await createTodoCommandHandler.Handle(command, cancellationToken);

            }).WithTags(Tags.TodosWithMediatr)
            .RequireAuthorization();
    }
}
