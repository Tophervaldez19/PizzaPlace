using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PizzaPlace.Application.Common.Interfaces;

namespace PizzaPlace.Application.Handlers.Pizzas.Commands.DeletePizza;
public class DeletePizzaValidator : AbstractValidator<DeletePizzaCommand>
{
    private readonly IApplicationDbContext _context;
    public DeletePizzaValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(x => x.Id).NotEmpty()
            .MustAsync(IdExist)
            .WithMessage((command, id) => $"The Id:{id} does not exists.");
    }

    private async Task<bool> IdExist(string id, CancellationToken cancellationToken)
    {
        // checks if a pizza type with the given Id already exists
        return await _context.Pizzas.AnyAsync(x => x.Id == id, cancellationToken);
    }
}
