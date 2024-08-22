
using PizzaPlace.Application.Handlers.Pizzas.Commands.CreatePizza;
using PizzaPlace.Application.Handlers.Pizzas.Commands.DeletePizza;
using PizzaPlace.Application.Handlers.Pizzas.Commands.UpdatePizza;
using PizzaPlace.Application.Handlers.Pizzas.Commands.UploadPizza;
using PizzaPlace.Application.Handlers.Pizzas.Dtos;
using PizzaPlace.Application.Handlers.Pizzas.Queries.GetPizzaById;
using PizzaPlace.Application.Handlers.Pizzas.Queries.GetPizzas;
using CsvParser = PizzaPlace.Application.Common.Helpers.CsvParser;

namespace PizzaPlace.Web.Endpoints;

public class Pizzas : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .DisableAntiforgery() //Temporary disable antiforgery
            .MapGet(GetPizzas)
            .MapGet(GetPizzaById, "GetPizzaById/{id}")
            .MapPost(CreatePizza)
            .MapPost(UploadPizza, nameof(UploadPizza))
            .MapDelete(DeletePizza, "DeletePizza/{id}")
            .MapPut(UpdatePizza, "UpdatePizza");
    }

    public async Task<Result<List<PizzaDto>>> GetPizzas(ISender sender)
    {
        return await sender.Send(new GetPizzasQuery());
    }

    public async Task<Result<PizzaDto>> GetPizzaById(ISender sender, string id)
    {
        return await sender.Send(new GetPizzaByIdQuery() { Id = id });
    }

    public async Task<Result> CreatePizza(ISender sender, CreatePizzaCommand command)
    {
        return await sender.Send(command);
    }

    public async Task<Result> DeletePizza(ISender sender, string id)
    {
        return await sender.Send(new DeletePizzaCommand() { Id = id });
    }

    public async Task<Result> UpdatePizza(ISender sender, UpdatePizzaCommand command)
    {
        return await sender.Send(command);
    }

    public async Task<Result> UploadPizza(ISender sender, IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return Result.Failure("No file uploaded.");
        }

        try
        {
            using var stream = file.OpenReadStream();
            var pizzaDtos = CsvParser.ParsePizzaDtoFromCsv(stream);

            return await sender.Send(new UploadPizzaCommand() { Pizzas = pizzaDtos });
        }
        catch (Exception ex)
        {
            return Result.Failure($"Error saving CSV data... {ex.InnerException!.Message}");
        }
    }
}
