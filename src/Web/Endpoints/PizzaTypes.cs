using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaPlace.Application.Common.Helpers;
using PizzaPlace.Application.Handlers.PizzaTypes.Commands.CreatePizzaType;
using PizzaPlace.Application.Handlers.PizzaTypes.Commands.DeletePizzaType;
using PizzaPlace.Application.Handlers.PizzaTypes.Commands.UpdatePizzaType;
using PizzaPlace.Application.Handlers.PizzaTypes.Commands.UploadPizzaType;
using PizzaPlace.Application.Handlers.PizzaTypes.Dtos;
using PizzaPlace.Application.Handlers.PizzaTypes.Queries.GetPizzaTypeById;
using PizzaPlace.Application.Handlers.PizzaTypes.Queries.GetPizzaTypes;
using CsvParser = PizzaPlace.Application.Common.Helpers.CsvParser;
namespace PizzaPlace.Web.Endpoints;

public class PizzaTypes : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .DisableAntiforgery() //Temporary disable antiforgery
            .MapGet(GetPizzaTypes)
            .MapGet(GetPizzaTypeById, "GetPizzaTypeById/{id}")
            .MapPost(CreatePizzaType)
            .MapPost(UploadPizzaType, nameof(UploadPizzaType))
            .MapDelete(DeletePizzaType, "DeletePizzaType/{id}")
            .MapPut(UpdatePizzaType, "UpdatePizzaType");
    }

    public async Task<Result<List<PizzaTypeDto>>> GetPizzaTypes(ISender sender)
    {
        return await sender.Send(new GetPizzaTypesQuery());
    }

    public async Task<Result<PizzaTypeDto>> GetPizzaTypeById(ISender sender, string id)
    {
        return await sender.Send(new GetPizzaTypeByIdQuery() { Id = id });
    }

    public async Task<Result> CreatePizzaType(ISender sender, CreatePizzaTypeCommand command)
    {
        return await sender.Send(command);
    }

    public async Task<Result> DeletePizzaType(ISender sender, string id)
    {
        return await sender.Send(new DeletePizzaTypeCommand() { Id = id });
    }

    public async Task<Result> UploadPizzaType(ISender sender, IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return Result.Failure("No file uploaded.");
        }

        try
        {
            using var stream = file.OpenReadStream();
            var pizzaTypeDtos = CsvParser.ParsePizzaTypeDtoFromCsv(stream);

            return await sender.Send(new UploadPizzaTypeCommand() { PizzaTypes = pizzaTypeDtos });
        }
        catch (Exception ex)
        {
            return Result.Failure($"Error saving CSV data... {ex.InnerException!.Message}");
        }
    }

    public async Task<Result> UpdatePizzaType(ISender sender, UpdatePizzaTypeCommand command)
    {
        return await sender.Send(command);
    }
}
