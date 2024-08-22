using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaPlace.Application.Handlers.Orders.Dtos;
public class OrderDto
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
}
