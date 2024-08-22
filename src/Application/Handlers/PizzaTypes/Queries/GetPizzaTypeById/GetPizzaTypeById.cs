using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaPlace.Application.Common.Interfaces;
using PizzaPlace.Application.Handlers.PizzaTypes.Dtos;

namespace PizzaPlace.Application.Handlers.PizzaTypes.Queries.GetPizzaTypeById;
public class GetPizzaTypeByIdQuery : IRequest<Result<PizzaTypeDto>>
{
    public string Id { get; set; } = string.Empty;
}

public class GetPizzaTypeByIdQueryHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetPizzaTypeByIdQuery, Result<PizzaTypeDto>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    public async Task<Result<PizzaTypeDto>> Handle(GetPizzaTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var pizzaType = await _context.PizzaTypes.FindAsync(request.Id, cancellationToken);

        var dto = _mapper.Map<PizzaTypeDto>(pizzaType);

        return Result<PizzaTypeDto>.Success(dto);
    }
}
