using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PizzaPlace.Application.Common.Interfaces;

namespace PizzaPlace.Application.Handlers.Pizzas.Commands.UpdatePizza;
public class UpdatePizzaValidator : AbstractValidator<UpdatePizzaCommand>
{
    private readonly IApplicationDbContext _context;
    public UpdatePizzaValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(x => x.Id).NotEmpty()
            .MustAsync(IdExist)
            .WithMessage((command, id) => $"The Id:{id} does not exists.");

        RuleFor(x => x.PizzaTypeId).NotEmpty()
            .MustAsync(TypeIdExist)
            .WithMessage((command) => $"The Pizza Type Id:{command.PizzaTypeId} does not exists.");

        RuleFor(x => x.Size).NotEmpty();
        RuleFor(x => x.Price).NotEmpty();
    }

    private async Task<bool> IdExist(string id, CancellationToken cancellationToken)
    {
        // checks if a pizza type with the given Id already exists
        return await _context.Pizzas.AnyAsync(x => x.Id == id, cancellationToken);
    }

    private async Task<bool> TypeIdExist(string typeId, CancellationToken cancellationToken)
    {
        // checks if a pizza type with the given Id already exists
        return await _context.PizzaTypes.AnyAsync(x => x.Id == typeId, cancellationToken);
    }
}
