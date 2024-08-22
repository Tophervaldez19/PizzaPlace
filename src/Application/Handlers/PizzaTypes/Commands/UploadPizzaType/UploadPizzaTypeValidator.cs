using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PizzaPlace.Application.Common.Interfaces;

namespace PizzaPlace.Application.Handlers.PizzaTypes.Commands.UploadPizzaType;
public class UploadPizzaTypeValidator : AbstractValidator<UploadPizzaTypeCommand>
{
    private readonly IApplicationDbContext _context;
    public UploadPizzaTypeValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(x => x.PizzaTypes).NotEmpty();

        //RuleForEach(x => x.PizzaTypes).SetValidator(new PizzaTypeCsvDtoValidator(context));
    }
}


public class PizzaTypeCsvDtoValidator : AbstractValidator<PizzaTypeCsvDto>
{
    private readonly IApplicationDbContext _context;
    public PizzaTypeCsvDtoValidator(IApplicationDbContext context)
    {
        _context = context;
        RuleFor(x => x.Id).NotEmpty()
            .MustAsync(IdDoesNotExist)
            .WithMessage((command, id) => $"The Id:{id} already exists.");

        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Category).NotEmpty();
        RuleFor(x => x.Ingredients).NotEmpty();
    }
    private async Task<bool> IdDoesNotExist(string id, CancellationToken cancellationToken)
    {
        // checks if a pizza type with the given Id already exists
        return !await _context.PizzaTypes.AnyAsync(x => x.Id == id, cancellationToken);
    }

}
