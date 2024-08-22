using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaPlace.Application.Common.Interfaces;

namespace PizzaPlace.Application.Handlers.PizzaTypes.Commands.DeletePizzaType;
public class DeletePizzaTypeValidator : AbstractValidator<DeletePizzaTypeCommand>
{
    private readonly IApplicationDbContext _context;
    public DeletePizzaTypeValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(x => x.Id).NotEmpty()
            .MustAsync(IdExist)
            .WithMessage((command, id) => $"The Id:{id} does not exists.");
    }

    private async Task<bool> IdExist(string id, CancellationToken cancellationToken)
    {
        // checks if a pizza type with the given Id already exists
        return await _context.PizzaTypes.AnyAsync(x => x.Id == id, cancellationToken);
    }
}
