using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PizzaPlace.Application.Common.Interfaces;
using PizzaPlace.Application.Handlers.PizzaTypes.Commands.UploadPizzaType;

namespace PizzaPlace.Application.Handlers.Orders.Commands.UploadOrder;
public class UploadOrderCommand : IRequest<Result>
{
    public List<OrderCsvDto> Orders { get; set; } = new();
}

public class UploadOrderCommandHandler(IApplicationDbContext context,IMapper mapper): IRequestHandler<UploadOrderCommand, Result>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    public async Task<Result> Handle(UploadOrderCommand request, CancellationToken cancellationToken)
    {
        var orders = _mapper.Map<List<Order>>(request.Orders);

        var orderForInsert = new List<Order>();

        foreach (var order in orders)
        {
            if (!await OrderExist(order.Id) && !orderForInsert.Any(x => x.Id == order.Id))
            {
                orderForInsert.Add(order); // Only add Not existing Id
            }
        }

        await _context.Orders.AddRangeAsync(orderForInsert);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<bool> OrderExist(int id)
    {
        return await _context.Orders.AnyAsync(x => x.Id == id);
    }
}   
