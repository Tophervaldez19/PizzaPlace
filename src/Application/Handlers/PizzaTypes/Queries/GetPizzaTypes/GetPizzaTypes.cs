using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaPlace.Application.Common.Interfaces;
using PizzaPlace.Application.Handlers.PizzaTypes.Dtos;

namespace PizzaPlace.Application.Handlers.PizzaTypes.Queries.GetPizzaTypes;
public class GetPizzaTypesQuery : IRequest<Result<List<PizzaTypeDto>>>
{
}

public class GetPizzaTypesQueryHandler(IApplicationDbContext context,IMapper mapper) : IRequestHandler<GetPizzaTypesQuery, Result<List<PizzaTypeDto>>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    public async Task<Result<List<PizzaTypeDto>>> Handle(GetPizzaTypesQuery request, CancellationToken cancellationToken)
    {
        var pizzaTypes = await _context.PizzaTypes.ToListAsync();

        var pizzaTypesDto = _mapper.Map<List<PizzaTypeDto>>(pizzaTypes);

        return Result<List<PizzaTypeDto>>.Success(pizzaTypesDto);
    }
}
