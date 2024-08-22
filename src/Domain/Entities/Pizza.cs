using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaPlace.Domain.Entities;
public class Pizza : BaseAuditableEntity<string>
{
    public string PizzaTypeId { get; set; } = String.Empty;
    public PizzaType PizzaType { get; set; } = default!;
    public PizzaSize Size { get; set; }
    public double Price { get; set; }

    public virtual List<OrderDetail> OrderDetails { get; set; } = new();
}
