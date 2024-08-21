using PizzaPlace.Application.Handlers.PizzaTypes.Commands.CreatePizzaType;
using PizzaPlace.Application.Handlers.PizzaTypes.Commands.DeletePizzaType;

namespace PizzaPlace.Web.Endpoints;

public class PizzaTypes :EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapPost(CreatePizzaType)
            .MapPost(UploadPizzaType, nameof(UploadPizzaType))
            .MapDelete(DeletePizzaType, "DeletePizzaType/{id}");
            
    }

    public async Task<Result> CreatePizzaType(ISender sender,CreatePizzaTypeCommand command)
    {
        return await sender.Send(command);
    }

    public async Task<Result> DeletePizzaType(ISender sender, string id)
    {
        return await sender.Send(new DeletePizzaTypeCommand() { Id = id });
    }

    public Result UploadPizzaType(ISender sender,IFormFile file)
    {
        return Result.Success();
    }
}
