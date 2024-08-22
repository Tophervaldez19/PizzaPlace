using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaPlace.Application.Handlers.PizzaTypes.Commands.UploadPizzaType;

namespace PizzaPlace.Application.Handlers.Orders.Commands.UploadOrder;
public class UploadOrderValidator : AbstractValidator<UploadOrderCommand>
{
    public UploadOrderValidator()
    {
        RuleFor(x => x.Orders).NotEmpty();
    }
}
