using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaPlace.Application.Handlers.PizzaTypes.Dtos;
using PizzaPlace.Domain.Enums;

namespace PizzaPlace.Application.Handlers.Pizzas.Dtos;
public class PizzaDto
{
    public string Id { get; set; } = String.Empty;
    public string PizzaTypeId { get; set; } = String.Empty;
    public PizzaTypeDto PizzaType { get; set; } = default!;
    public PizzaSize Size { get; set; }
    public double Price { get; set; }
}
