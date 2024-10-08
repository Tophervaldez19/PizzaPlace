﻿
using PizzaPlace.Application.Handlers.Orders.Commands.CreateOrder;
using PizzaPlace.Application.Handlers.Orders.Commands.DeleteOrder;
using PizzaPlace.Application.Handlers.Orders.Commands.UpdateOrder;
using PizzaPlace.Application.Handlers.Orders.Commands.UploadOrder;
using PizzaPlace.Application.Handlers.Orders.Dtos;
using PizzaPlace.Application.Handlers.Orders.Queries.GetOrderById;
using PizzaPlace.Application.Handlers.Orders.Queries.GetOrders;
using PizzaPlace.Application.Handlers.Pizzas.Commands.UploadPizza;
using PizzaPlace.Application.Handlers.Pizzas.Dtos;
using PizzaPlace.Application.Handlers.Pizzas.Queries.GetPizzaById;
using PizzaPlace.Application.Handlers.Pizzas.Queries.GetPizzas;
using CsvParser = PizzaPlace.Application.Common.Helpers.CsvParser;

namespace PizzaPlace.Web.Endpoints;

public class Orders : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .DisableAntiforgery()
            .MapGet(GetOrders)
            .MapGet(GetOrderById, "GetOrderById/{id}")
            .MapPost(CreateOrder)
            .MapPost(UploadOrder, nameof(UploadOrder))
            .MapDelete(DeleteOrder, "DeleteOrder/{id}")
            .MapPut(UpdateOrder, "UpdateOrder");
    }

    public async Task<Result<List<OrderDto>>> GetOrders(ISender sender)
    {
        return await sender.Send(new GetOrdersQuery());
    }

    public async Task<Result<OrderDto>> GetOrderById(ISender sender, int id)
    {
        return await sender.Send(new GetOrderByIdQuery() { Id = id });
    }

    public async Task<Result> CreateOrder(ISender sender, CreateOrderCommand command)
    {
        return await sender.Send(command);
    }

    public async Task<Result> DeleteOrder(ISender sender, int id)
    {
        return await sender.Send(new DeleteOrderCommand() { Id = id });
    }

    public async Task<Result> UpdateOrder(ISender sender, UpdateOrderCommand command)
    {
        return await sender.Send(command);
    }

    public async Task<Result> UploadOrder(ISender sender, IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return Result.Failure("No file uploaded.");
        }

        try
        {
            using var stream = file.OpenReadStream();
            var orderDtos = CsvParser.ParseOrderDtoFromCsv(stream);

            return await sender.Send(new UploadOrderCommand() { Orders = orderDtos });
        }
        catch (Exception ex)
        {
            return Result.Failure($"Error saving CSV data... {ex.InnerException!.Message}");
        }
    }
}
