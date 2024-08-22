using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PizzaPlace.Application.Common.Interfaces;

namespace PizzaPlace.Application.Handlers.Orders.Commands.UpdateOrder;
public class UpdateOrderCommand : IRequest<Result>
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
}

public class UpdateOrderCommandHandler(IApplicationDbContext context,IMapper mapper) : IRequestHandler<UpdateOrderCommand, Result>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    public async Task<Result> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _context.Orders.FindAsync(request.Id, cancellationToken);

        var orderToUpdate = _mapper.Map(request, order);

        _context.Orders.Update(orderToUpdate!);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
