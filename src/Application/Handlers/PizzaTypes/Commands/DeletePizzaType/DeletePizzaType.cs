using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaPlace.Application.Common.Interfaces;

namespace PizzaPlace.Application.Handlers.PizzaTypes.Commands.DeletePizzaType;
public class DeletePizzaTypeCommand : IRequest<Result>
{
    public string Id { get; set; } = string.Empty;
}


public class DeletePizzaTypeCommandHandler(IApplicationDbContext context) : IRequestHandler<DeletePizzaTypeCommand, Result>
{
    private readonly IApplicationDbContext _context = context;
    public async Task<Result> Handle(DeletePizzaTypeCommand request, CancellationToken cancellationToken)
    {
        var pizzaType = await _context.PizzaTypes.FindAsync(request.Id);
        
        _context.PizzaTypes.Remove(pizzaType!);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
