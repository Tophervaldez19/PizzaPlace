using PizzaPlace.Application.Common.Interfaces;

namespace PizzaPlace.Application.Handlers.PizzaTypes.Commands.UploadPizzaType;
public class UploadPizzaTypeCommand : IRequest<Result>
{
    public List<PizzaTypeCsvDto> PizzaTypes { get; set; } = new();
}

public class UploadPizzaTypeCommandHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<UploadPizzaTypeCommand, Result>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    public async Task<Result> Handle(UploadPizzaTypeCommand request, CancellationToken cancellationToken)
    {
        var pizzaTypes = _mapper.Map<List<PizzaType>>(request.PizzaTypes);

        var pizzaTypeForInsert = new List<PizzaType>();

        foreach(var pizzaType in pizzaTypes)
        {
            if (!await PizzaTypeExist(pizzaType.Id) && !pizzaTypeForInsert.Any(x=>x.Id == pizzaType.Id))
            {
                pizzaTypeForInsert.Add(pizzaType); // Only add Not existing Id
            }
        }

        await _context.PizzaTypes.AddRangeAsync(pizzaTypeForInsert);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<bool> PizzaTypeExist(string id)
    {
        return await _context.PizzaTypes.AnyAsync(x => x.Id == id);
    }
}
