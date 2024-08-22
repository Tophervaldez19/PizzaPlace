using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaPlace.Domain.Entities;
public class Order : BaseAuditableEntity<int>
{
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }

    public virtual List<OrderDetail> OrderDetails { get; set; } = new();
}
