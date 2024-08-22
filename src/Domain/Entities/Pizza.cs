using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaPlace.Domain.Entities;
public class Pizza : BaseAuditableEntity<string>
{
    public string PizzaTypeId { get; set; } = String.Empty;
    public PizzaType PizzaType { get; set; } = new();
    public string Size { get; set; } = String.Empty;
    public double Price { get; set; }
}
