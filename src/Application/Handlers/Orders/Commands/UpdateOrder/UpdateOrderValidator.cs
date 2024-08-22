using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaPlace.Application.Common.Interfaces;

namespace PizzaPlace.Application.Handlers.Orders.Commands.UpdateOrder;
public class UpdateOrderValidator : AbstractValidator<UpdateOrderCommand>
{
    private readonly IApplicationDbContext _context;
    public UpdateOrderValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(x => x.Id).NotEmpty()
            .MustAsync(IdExist)
            .WithMessage((command, id) => $"The Id:{id} does not exists.");

        RuleFor(x => x.Date).NotEmpty();
        RuleFor(x => x.Time).NotEmpty();
    }

    private async Task<bool> IdExist(int id, CancellationToken cancellationToken)
    {
        // checks if a pizza type with the given Id already exists
        return await _context.Orders.AnyAsync(x => x.Id == id, cancellationToken);
    }

}
