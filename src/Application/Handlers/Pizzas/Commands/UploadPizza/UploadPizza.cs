using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PizzaPlace.Application.Common.Interfaces;
using PizzaPlace.Domain.Entities;

namespace PizzaPlace.Application.Handlers.Pizzas.Commands.UploadPizza;
public class UploadPizzaCommand : IRequest<Result>
{
    public List<PizzaCsvDto> Pizzas { get; set; } = new();
}

public class UploadPizzaCommandHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<UploadPizzaCommand, Result>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    public async Task<Result> Handle(UploadPizzaCommand request, CancellationToken cancellationToken)
    {
        var pizzas = _mapper.Map<List<Pizza>>(request.Pizzas);

        var pizzaForInsert = new List<Pizza>();

        List<string> errors = new();

        foreach (var pizza in pizzas)
        {
            if (!await PizzaTypeExist(pizza.PizzaTypeId))
            {
                errors.Add($"Pizza Type Id:{pizza.PizzaTypeId} does not exists.");
            }

            if (!await PizzaExist(pizza.Id) && !pizzaForInsert.Any(x => x.Id == pizza.Id))
            {
                pizzaForInsert.Add(pizza); // Only add Not existing Id
            }
        }

        if (errors.Any())
        {
            return Result.Failure(errors);
        }

        await _context.Pizzas.AddRangeAsync(pizzaForInsert);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<bool> PizzaTypeExist(string typeId)
    {
        return await _context.PizzaTypes.AnyAsync(x => x.Id == typeId);
    }

    public async Task<bool> PizzaExist(string id)
    {
        return await _context.Pizzas.AnyAsync(x => x.Id == id);
    }

}
