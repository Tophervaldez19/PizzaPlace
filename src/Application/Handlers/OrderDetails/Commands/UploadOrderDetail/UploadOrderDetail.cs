using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PizzaPlace.Application.Common.Interfaces;
using PizzaPlace.Domain.Entities;

namespace PizzaPlace.Application.Handlers.OrderDetails.Commands.UploadOrderDetail;
public class UploadOrderDetailCommand : IRequest<Result>
{
    public List<OrderDetailCsvDto> OrderDetails { get; set; } = new();
}

public class UploadOrderDetailCommandHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<UploadOrderDetailCommand, Result>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    public async Task<Result> Handle(UploadOrderDetailCommand request, CancellationToken cancellationToken)
    {
        var orderDetails = _mapper.Map<List<OrderDetail>>(request.OrderDetails);

        var orderDetailsForInsert = new List<OrderDetail>();

        List<string> errors = new();

        foreach (var orderDetail in orderDetails)
        {
            if(!await OrderExist(orderDetail.OrderId))
            {
                errors.Add($"Order Id:{orderDetail.OrderId} does not exists.");
                break;
            }

            if (!await PizzaExist(orderDetail.PizzaId))
            {
                errors.Add($"Pizza Id:{orderDetail.PizzaId} does not exists.");
                break;
            }

            if (!await OrderDetailExist(orderDetail.Id) && !orderDetailsForInsert.Any(x => x.Id == orderDetail.Id))
            {
                orderDetailsForInsert.Add(orderDetail); // Only add Not existing Id
            }

        }

        if (errors.Any())
        {
            return Result.Failure(errors);
        }

        await _context.OrderDetails.AddRangeAsync(orderDetailsForInsert);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<bool> OrderDetailExist(int id)
    {
        return await _context.OrderDetails.AnyAsync(x => x.Id == id);
    }

    public async Task<bool> OrderExist(int id)
    {
        return await _context.Orders.AnyAsync(x => x.Id == id);
    }

    public async Task<bool> PizzaExist(string id)
    {
        return await _context.Pizzas.AnyAsync(x => x.Id == id);
    }

    
}
