using PizzaPlace.Domain.Entities;

namespace PizzaPlace.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }

    DbSet<PizzaType> PizzaTypes { get; }

    DbSet<Pizza> Pizzas { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
