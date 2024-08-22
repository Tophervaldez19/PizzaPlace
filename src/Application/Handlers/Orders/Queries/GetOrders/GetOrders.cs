using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaPlace.Application.Common.Interfaces;
using PizzaPlace.Application.Handlers.Orders.Dtos;
using PizzaPlace.Application.Handlers.PizzaTypes.Dtos;

namespace PizzaPlace.Application.Handlers.Orders.Queries.GetOrders;
public class GetOrdersQuery : IRequest<Result<List<OrderDto>>>
{
}

public class GetOrdersQueryHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetOrdersQuery, Result<List<OrderDto>>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    public async Task<Result<List<OrderDto>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await _context.Orders.ToListAsync();

        var orderDto = _mapper.Map<List<OrderDto>>(orders);

        return Result<List<OrderDto>>.Success(orderDto);
    }
}
