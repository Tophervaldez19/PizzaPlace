using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaPlace.Application.Common.Interfaces;
using PizzaPlace.Application.Handlers.Pizzas.Dtos;

namespace PizzaPlace.Application.Handlers.Pizzas.Queries.GetPizzaById;
public class GetPizzaByIdQuery : IRequest<Result<PizzaDto>>
{
    public string Id { get; set; } = String.Empty;
}

public class GetPizzaByIdQueryHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetPizzaByIdQuery, Result<PizzaDto>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    public async Task<Result<PizzaDto>> Handle(GetPizzaByIdQuery request, CancellationToken cancellationToken)
    {
        var pizza = await _context.Pizzas.FindAsync(request.Id, cancellationToken);

        var dto = _mapper.Map<PizzaDto>(pizza);

        return Result<PizzaDto>.Success(dto);
    }
}
