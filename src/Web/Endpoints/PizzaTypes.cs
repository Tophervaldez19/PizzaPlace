using PizzaPlace.Application.Handlers.PizzaTypes.Commands;

namespace PizzaPlace.Web.Endpoints;

public class PizzaTypes :EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapPost(CreatePizzaType);
    }

    public async Task<Result> CreatePizzaType(ISender sender,CreatePizzaTypeCommand command)
    {
        return await sender.Send(command);
    }
}
