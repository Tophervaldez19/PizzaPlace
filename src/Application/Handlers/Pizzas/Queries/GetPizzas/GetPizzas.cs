using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PizzaPlace.Application.Common.Interfaces;
using PizzaPlace.Application.Handlers.Pizzas.Dtos;
using PizzaPlace.Application.Handlers.PizzaTypes.Dtos;

namespace PizzaPlace.Application.Handlers.Pizzas.Queries.GetPizzas;
public class GetPizzasQuery : IRequest<Result<List<PizzaDto>>>
{

}

public class GetPizzasQueryHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetPizzasQuery, Result<List<PizzaDto>>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    public async Task<Result<List<PizzaDto>>> Handle(GetPizzasQuery request, CancellationToken cancellationToken)
    {
        var pizzas = await _context.Pizzas.Include(x => x.PizzaType).ToListAsync();

        var pizzasDto = _mapper.Map<List<PizzaDto>>(pizzas);

        return Result<List<PizzaDto>>.Success(pizzasDto);
    }
}
