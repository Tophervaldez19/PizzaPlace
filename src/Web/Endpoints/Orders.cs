
using PizzaPlace.Application.Handlers.Orders.Commands.CreateOrder;
using PizzaPlace.Application.Handlers.Orders.Commands.DeleteOrder;

namespace PizzaPlace.Web.Endpoints;

public class Orders : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapPost(CreateOrder)
            .MapDelete(DeleteOrder, "DeleteOrder/{id}");
    }

    public async Task<Result> CreateOrder(ISender sender, CreateOrderCommand command)
    {
        return await sender.Send(command);
    }

    public async Task<Result> DeleteOrder(ISender sender, int id)
    {
        return await sender.Send(new DeleteOrderCommand() { Id = id });
    }
}
