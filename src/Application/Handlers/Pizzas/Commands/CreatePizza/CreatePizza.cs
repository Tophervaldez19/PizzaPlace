using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaPlace.Application.Common.Interfaces;
using PizzaPlace.Domain.Enums;

namespace PizzaPlace.Application.Handlers.Pizzas.Commands.CreatePizza;
public class CreatePizzaCommand : IRequest<Result>
{
    public string Id { get; set; } = String.Empty;
    public string PizzaTypeId { get; set; } = String.Empty;
    public PizzaSize Size { get; set; }
    public double Price { get; set; }
}

public class CreatePizzaCommandHandler(IApplicationDbContext context,IMapper mapper) : IRequestHandler<CreatePizzaCommand, Result>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    public async Task<Result> Handle(CreatePizzaCommand request, CancellationToken cancellationToken)
    {
        var pizza = _mapper.Map<Pizza>(request);

        await _context.Pizzas.AddAsync(pizza);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
