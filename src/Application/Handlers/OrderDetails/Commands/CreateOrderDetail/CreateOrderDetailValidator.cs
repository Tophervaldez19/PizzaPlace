using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaPlace.Application.Common.Interfaces;

namespace PizzaPlace.Application.Handlers.OrderDetails.Commands.CreateOrderDetail;
public class CreateOrderDetailValidator : AbstractValidator<CreateOrderDetailCommand>
{
    private readonly IApplicationDbContext _context;
    public CreateOrderDetailValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(x => x.Id).NotNull()
            .MustAsync(IdDoesNotExist)
            .WithMessage((command, id) => $"The Id:{id} already exists.");

        RuleFor(x => x.OrderId).NotEmpty()
            .MustAsync(OrderIdExist)
                .WithMessage((command, id) => $"The OrderId:{id} does not exists.");

        RuleFor(x => x.PizzaId).NotEmpty()
            .MustAsync(PizzaIdExist)
                .WithMessage((command, id) => $"The PizzaId:{id} does not exists.");

        RuleFor(x => x.Quantity).NotEmpty();

    }

    private async Task<bool> IdDoesNotExist(int id, CancellationToken cancellationToken)
    {
        // checks if a orderDetail with the given Id already exists
        return !await _context.OrderDetails.AnyAsync(x => x.Id == id, cancellationToken);
    }

    private async Task<bool> OrderIdExist(int id, CancellationToken cancellationToken)
    {
        // checks if a order with the given Id already exists
        return await _context.Orders.AnyAsync(x => x.Id == id, cancellationToken);
    }

    private async Task<bool> PizzaIdExist(string id, CancellationToken cancellationToken)
    {
        // checks if a pizza with the given Id already exists
        return await _context.Pizzas.AnyAsync(x => x.Id == id, cancellationToken);
    }
}
