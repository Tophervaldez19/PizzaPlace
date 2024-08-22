using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaPlace.Application.Common.Interfaces;
using PizzaPlace.Application.Handlers.PizzaTypes.Commands.DeletePizzaType;

namespace PizzaPlace.Application.Handlers.Pizzas.Commands.DeletePizza;
public class DeletePizzaCommand : IRequest<Result>
{
    public string Id { get; set; } = string.Empty;
}

public class DeletePizzaCommandHandler(IApplicationDbContext context) : IRequestHandler<DeletePizzaCommand, Result>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Result> Handle(DeletePizzaCommand request, CancellationToken cancellationToken)
    {
        var pizza = await _context.Pizzas.FindAsync(request.Id);

        _context.Pizzas.Remove(pizza!);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
