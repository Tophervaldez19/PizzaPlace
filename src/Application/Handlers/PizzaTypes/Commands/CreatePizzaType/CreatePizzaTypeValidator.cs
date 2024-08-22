using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using PizzaPlace.Application.Common.Interfaces;

namespace PizzaPlace.Application.Handlers.PizzaTypes.Commands.CreatePizzaType;
public class CreatePizzaTypeValidator : AbstractValidator<CreatePizzaTypeCommand>
{
    private readonly IApplicationDbContext _context;
    public CreatePizzaTypeValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(x => x.Id).NotEmpty()
            .MustAsync(IdDoesNotExist)
            .WithMessage((command, id) => $"The Id:{id} already exists.");

        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Category).NotEmpty();
        RuleFor(x => x.Ingredients).NotEmpty();
    }

    private async Task<bool> IdDoesNotExist(string id,CancellationToken cancellationToken)
    {
        // checks if a pizza type with the given Id already exists
        return !await _context.PizzaTypes.AnyAsync(x => x.Id == id, cancellationToken);
    }
}
