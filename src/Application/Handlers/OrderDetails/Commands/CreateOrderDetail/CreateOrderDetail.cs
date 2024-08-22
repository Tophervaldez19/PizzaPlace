using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PizzaPlace.Application.Common.Interfaces;
using PizzaPlace.Application.Handlers.Orders.Commands.CreateOrder;

namespace PizzaPlace.Application.Handlers.OrderDetails.Commands.CreateOrderDetail;
public class CreateOrderDetailCommand : IRequest<Result>
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public string PizzaId { get; set; } = String.Empty;
    public double Quantity { get; set; }
}

public class CreateOrderDetailCommandHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<CreateOrderDetailCommand, Result>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    
    public async Task<Result> Handle(CreateOrderDetailCommand request, CancellationToken cancellationToken)
    {
        var orderDetail = _mapper.Map<OrderDetail>(request);

        if (orderDetail.Id == 0)
        {
            orderDetail.Id = await GetNextOrderDetailIdAsync();
        }

        await _context.OrderDetails.AddAsync(orderDetail);

        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }

    public async Task<int> GetNextOrderDetailIdAsync()
    {
        // Get the highest Id currently in the database and add 1
        var maxId = await _context.OrderDetails.MaxAsync(o => (int?)o.Id) ?? 0;
        return maxId + 1;
    }
}
