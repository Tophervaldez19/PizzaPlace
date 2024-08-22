using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaPlace.Application.Common.Interfaces;
using PizzaPlace.Domain.Enums;

namespace PizzaPlace.Application.Handlers.Pizzas.Commands.UpdatePizza;
public class UpdatePizzaCommand : IRequest<Result>
{
    public string Id { get; set; } = String.Empty;
    public string PizzaTypeId { get; set; } = String.Empty;
    public PizzaSize Size { get; set; }
    public double Price { get; set; }
}

public class UpdatePizzaCommandHandler(IApplicationDbContext context,IMapper mapper) : IRequestHandler<UpdatePizzaCommand, Result>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    public async Task<Result> Handle(UpdatePizzaCommand request, CancellationToken cancellationToken)
    {
        var pizza = await _context.Pizzas.FindAsync(request.Id, cancellationToken);

        var pizzaToUpdate = _mapper.Map(request, pizza);

        _context.Pizzas.Update(pizzaToUpdate!);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
