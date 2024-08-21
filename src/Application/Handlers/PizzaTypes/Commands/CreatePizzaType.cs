using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaPlace.Application.Common.Interfaces;
using PizzaPlace.Domain.Entities;

namespace PizzaPlace.Application.Handlers.PizzaTypes.Commands;
public class CreatePizzaTypeCommand : IRequest<Result>
{
    public string Id { get; set; } = String.Empty;
    public string Name { get; set; } = String.Empty;
    public string Category { get; set; } = String.Empty;
    public string Ingredients { get; set; } = String.Empty;
}

public class CreatePizzaTypeCommandHandler(IApplicationDbContext context,IMapper mapper) : IRequestHandler<CreatePizzaTypeCommand, Result>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    public async Task<Result> Handle(CreatePizzaTypeCommand request, CancellationToken cancellationToken)
    {
        var pizzaType = _mapper.Map<PizzaType>(request);

        await _context.PizzaTypes.AddAsync(pizzaType);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
