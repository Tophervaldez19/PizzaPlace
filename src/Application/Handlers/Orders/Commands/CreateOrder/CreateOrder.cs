using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PizzaPlace.Application.Common.Interfaces;

namespace PizzaPlace.Application.Handlers.Orders.Commands.CreateOrder;
public class CreateOrderCommand : IRequest<Result>
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
}

public class CreateOrderCommandHandler(IApplicationDbContext context,IMapper mapper) : IRequestHandler<CreateOrderCommand, Result>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    public async Task<Result> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = _mapper.Map<Order>(request);

        if(order.Id == 0)
        {
            order.Id = await GetNextOrderIdAsync();
        }

        await _context.Orders.AddAsync(order);

        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }

    public async Task<int> GetNextOrderIdAsync()
    {
        // Get the highest Id currently in the database and add 1
        var maxId = await _context.Orders.MaxAsync(o => (int?)o.Id) ?? 0;
        return maxId + 1;
    }
}
