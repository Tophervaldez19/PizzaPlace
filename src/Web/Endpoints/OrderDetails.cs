
using PizzaPlace.Application.Handlers.OrderDetails.Commands.CreateOrderDetail;
using PizzaPlace.Application.Handlers.OrderDetails.Commands.UploadOrderDetail;
using PizzaPlace.Application.Handlers.Orders.Commands.CreateOrder;
using PizzaPlace.Application.Handlers.Orders.Commands.UploadOrder;
using CsvParser = PizzaPlace.Application.Common.Helpers.CsvParser;


namespace PizzaPlace.Web.Endpoints;

public class OrderDetails : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .DisableAntiforgery()
            .MapPost(CreateOrderDetail)
            .MapPost(UploadOrderDetail, nameof(UploadOrderDetail));
    }

    public async Task<Result> CreateOrderDetail(ISender sender, CreateOrderDetailCommand command)
    {
        return await sender.Send(command);
    }

    public async Task<Result> UploadOrderDetail(ISender sender, IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return Result.Failure("No file uploaded.");
        }

        try
        {
            using var stream = file.OpenReadStream();
            var orderDetailDtos = CsvParser.ParseOrderDetailDtoFromCsv(stream);

            return await sender.Send(new UploadOrderDetailCommand() { OrderDetails = orderDetailDtos });
        }
        catch (Exception ex)
        {
            return Result.Failure($"Error saving CSV data... {ex.InnerException!.Message}");
        }
    }
}
