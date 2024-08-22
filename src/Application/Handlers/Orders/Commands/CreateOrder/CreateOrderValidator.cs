using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PizzaPlace.Application.Common.Interfaces;

namespace PizzaPlace.Application.Handlers.Orders.Commands.CreateOrder;
public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
{
    private readonly IApplicationDbContext _context;
    public CreateOrderValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(x => x.Id).NotNull()
            .MustAsync(IdDoesNotExist)
            .WithMessage((command, id) => $"The Id:{id} already exists.");

        RuleFor(x => x.Date).NotEmpty();
        RuleFor(x => x.Time).NotEmpty();

    }

    private async Task<bool> IdDoesNotExist(int id, CancellationToken cancellationToken)
    {
        // checks if a pizza type with the given Id already exists
        return !await _context.Orders.AnyAsync(x => x.Id == id, cancellationToken);
    }
}
