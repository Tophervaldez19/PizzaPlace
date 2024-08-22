using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaPlace.Application.Common.Interfaces;

namespace PizzaPlace.Application.Handlers.Pizzas.Commands.CreatePizza;
public class CreatePizzaValidator : AbstractValidator<CreatePizzaCommand>
{
    private readonly IApplicationDbContext _context;
    public CreatePizzaValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(x => x.Id).NotEmpty()
            .MustAsync(IdDoesNotExist)
            .WithMessage((command, id) => $"The Id:{id} already exists.");

        RuleFor(x => x.PizzaTypeId).NotEmpty()
            .MustAsync(TypeIdExist)
            .WithMessage((command) => $"The Pizza Type Id:{command.PizzaTypeId} does not exists.");

        RuleFor(x => x.Size).NotEmpty();
        RuleFor(x => x.Price).NotEmpty();
    }

    private async Task<bool> IdDoesNotExist(string id, CancellationToken cancellationToken)
    {
        // checks if a pizza type with the given Id already exists
        return !await _context.Pizzas.AnyAsync(x => x.Id == id, cancellationToken);
    }

    private async Task<bool> TypeIdExist(string typeId, CancellationToken cancellationToken)
    {
        // checks if a pizza type with the given Id already exists
        return await _context.PizzaTypes.AnyAsync(x => x.Id == typeId, cancellationToken);
    }
}
