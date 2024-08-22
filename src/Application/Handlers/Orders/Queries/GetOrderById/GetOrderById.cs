using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PizzaPlace.Application.Common.Interfaces;
using PizzaPlace.Application.Handlers.Orders.Dtos;
using PizzaPlace.Application.Handlers.PizzaTypes.Dtos;

namespace PizzaPlace.Application.Handlers.Orders.Queries.GetOrderById;
public class GetOrderByIdQuery : IRequest<Result<OrderDto>>
{
    public int Id { get; set; }
}

public class GetOrderByIdQueryHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetOrderByIdQuery, Result<OrderDto>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    public async Task<Result<OrderDto>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _context.Orders.FindAsync(request.Id, cancellationToken);

        var dto = _mapper.Map<OrderDto>(order);

        return Result<OrderDto>.Success(dto);
    }
}
