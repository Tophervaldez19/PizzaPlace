using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaPlace.Application.Common.Interfaces;
using PizzaPlace.Application.Handlers.Pizzas.Commands.DeletePizza;

namespace PizzaPlace.Application.Handlers.Orders.Commands.DeleteOrder;
public class DeleteOrderCommand : IRequest<Result>
{
    public int Id { get; set; }
}

public class DeleteOrderCommandHandler(IApplicationDbContext context) : IRequestHandler<DeleteOrderCommand, Result>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Result> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _context.Orders.FindAsync(request.Id);

        _context.Orders.Remove(order!);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
