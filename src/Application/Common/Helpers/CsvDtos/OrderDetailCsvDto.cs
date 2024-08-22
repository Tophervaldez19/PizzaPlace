using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaPlace.Application.Common.Helpers.CsvDtos;
public class OrderDetailCsvDto
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public string PizzaId { get; set; } = String.Empty;
    public double Quantity { get; set; }
}
