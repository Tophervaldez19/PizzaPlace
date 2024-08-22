using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaPlace.Application.Handlers.PizzaTypes.Commands.CreatePizzaType;
using PizzaPlace.Application.Handlers.PizzaTypes.Commands.UpdatePizzaType;
using PizzaPlace.Domain.Entities;

namespace PizzaPlace.Application.Common.Mappings;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateBidirectionalMap<CreatePizzaTypeCommand, PizzaType>();
        CreateBidirectionalMap<UpdatePizzaTypeCommand, PizzaType>();
        CreateBidirectionalMap<PizzaTypeCsvDto, PizzaType>();
    }

    private (IMappingExpression<T, T2> map1, IMappingExpression<T2, T> map2) CreateBidirectionalMap<T, T2>()
    {
        var map1 = CreateMap<T, T2>().IgnoreAllPropertiesWithAnInaccessibleSetter();
        var map2 = CreateMap<T2, T>().IgnoreAllPropertiesWithAnInaccessibleSetter();
        return (map1, map2);
    }
}
