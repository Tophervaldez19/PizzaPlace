using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaPlace.Application.Common.Interfaces;

namespace PizzaPlace.Application.Handlers.PizzaTypes.Commands.UpdatePizzaType;
public class UpdatePizzaTypeCommand : IRequest<Result>
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Ingredients { get; set; } = string.Empty;
}

public class UpdatePizzaTypeCommandHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<UpdatePizzaTypeCommand, Result>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<Result> Handle(UpdatePizzaTypeCommand request, CancellationToken cancellationToken)
    {
        var pizzaType = await _context.PizzaTypes.FindAsync(request.Id, cancellationToken);

        var pizzaTypeToUpdate = _mapper.Map(request, pizzaType);

        _context.PizzaTypes.Update(pizzaTypeToUpdate!);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
