using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaPlace.Application.Handlers.Orders.Commands.CreateOrder;
using PizzaPlace.Application.Handlers.Orders.Commands.UpdateOrder;
using PizzaPlace.Application.Handlers.Pizzas.Commands.CreatePizza;
using PizzaPlace.Application.Handlers.Pizzas.Commands.UpdatePizza;
using PizzaPlace.Application.Handlers.Pizzas.Dtos;
using PizzaPlace.Application.Handlers.PizzaTypes.Commands.CreatePizzaType;
using PizzaPlace.Application.Handlers.PizzaTypes.Commands.UpdatePizzaType;
using PizzaPlace.Application.Handlers.PizzaTypes.Dtos;
using PizzaPlace.Domain.Entities;
using PizzaPlace.Domain.Enums;

namespace PizzaPlace.Application.Common.Mappings;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // PizzaType
        CreateBidirectionalMap<CreatePizzaTypeCommand, PizzaType>();
        CreateBidirectionalMap<UpdatePizzaTypeCommand, PizzaType>();
        CreateBidirectionalMap<PizzaTypeCsvDto, PizzaType>();
        CreateBidirectionalMap<PizzaTypeDto, PizzaType>();


        // Pizza
        CreateBidirectionalMap<CreatePizzaCommand, Pizza>();
        CreateBidirectionalMap<UpdatePizzaCommand, Pizza>();
        CreateBidirectionalMap<PizzaDto,Pizza>();   
        CreateMap<PizzaCsvDto, Pizza>()
            .ForMember(dest => dest.Size, opt => opt.MapFrom(src => MapPizzaSize(src.Size)))
            .ReverseMap();

        // Orders
        CreateBidirectionalMap<CreateOrderCommand, Order>();
        CreateBidirectionalMap<UpdateOrderCommand, Order>();
        CreateBidirectionalMap<OrderCsvDto, Order>();
    }

    private (IMappingExpression<T, T2> map1, IMappingExpression<T2, T> map2) CreateBidirectionalMap<T, T2>()
    {
        var map1 = CreateMap<T, T2>().IgnoreAllPropertiesWithAnInaccessibleSetter();
        var map2 = CreateMap<T2, T>().IgnoreAllPropertiesWithAnInaccessibleSetter();
        return (map1, map2);
    }
    private PizzaSize MapPizzaSize(string size)
    {
        // Optionally, you could make this case-insensitive by using Enum.TryParse
        return size.ToLower() switch
        {
            "s" => PizzaSize.Small,
            "m" => PizzaSize.Medium,
            "l" => PizzaSize.Large,
            "xl" => PizzaSize.ExtraLarge,
            "xxl" => PizzaSize.DoubleExtraLarge,
            _ => throw new ArgumentException("Invalid pizza size")
        };
    }
}
