using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaPlace.Domain.Entities;
public class OrderDetail : BaseAuditableEntity<int>
{
    public int OrderId { get; set; }
    public Order Order { get; set; } = default!;
    public string PizzaId { get; set; } = String.Empty;
    public Pizza Pizza { get; set; } = default!;
    public double Quantity { get; set; }
}
