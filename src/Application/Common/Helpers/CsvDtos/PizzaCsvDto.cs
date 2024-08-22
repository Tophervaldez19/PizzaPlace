using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaPlace.Application.Common.Helpers.CsvDtos;
public class PizzaCsvDto
{
    public string Id { get; set; } = String.Empty;
    public string PizzaTypeId { get; set; } = String.Empty;
    public string Size { get; set; } = String.Empty;
    public double Price { get; set; }
}
