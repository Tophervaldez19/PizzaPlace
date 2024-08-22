using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;

namespace PizzaPlace.Application.Common.Helpers.CsvMappings;
public class OrderDetailMapping : ClassMap<OrderDetailCsvDto>
{
    public OrderDetailMapping()
    {
        Map(x => x.Id)
            .Name("order_details_id")
            .Validate(field =>
            {
                if (string.IsNullOrEmpty(field.Field))
                    throw new Exception($"Id is required");
                return true;
            });

        Map(x => x.OrderId)
            .Name("order_id")
            .Validate(field =>
            {
                if (string.IsNullOrEmpty(field.Field))
                    throw new Exception($"order_id is required");
                return true;
            });

        Map(x=>x.PizzaId)
            .Name("pizza_id")
            .Validate(field =>
            {
                if (string.IsNullOrEmpty(field.Field))
                    throw new Exception($"pizza_id is required");
                return true;
            });

        Map(x => x.Quantity)
            .Name("quantity")
            .Validate(field =>
            {
                if (string.IsNullOrEmpty(field.Field))
                    throw new Exception($"quantity is required");

                TryParseDouble("quantity", field.Field);

                return true;
            });
    }
    private static void TryParseDouble(string fieldName, string fieldValue)
    {
        if (!string.IsNullOrEmpty(fieldValue) && !double.TryParse(fieldValue, out _))
            throw new Exception($"{fieldName} value {fieldValue} is not in the correct decimal format");
    }
}
