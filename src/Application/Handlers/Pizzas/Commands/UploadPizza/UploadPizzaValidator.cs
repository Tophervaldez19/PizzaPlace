using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaPlace.Application.Handlers.Pizzas.Commands.UploadPizza;
public class UploadPizzaValidator  :AbstractValidator<UploadPizzaCommand>
{
    public UploadPizzaValidator()
    {
        RuleFor(x => x.Pizzas).NotEmpty();
    }
}
