using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaPlace.Domain.Entities;
public class PizzaType : BaseAuditableEntity<string>
{    
    public string Name { get; set; } = string.Empty;
    public string Category { get; set;} = string.Empty;
    public string Ingredients { get; set;} = string.Empty;

    public virtual List<Pizza> Pizzas { get; set; } = new();
}
