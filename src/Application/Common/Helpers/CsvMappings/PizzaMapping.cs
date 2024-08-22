using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;

namespace PizzaPlace.Application.Common.Helpers.CsvMappings;
public class PizzaMapping : ClassMap<PizzaCsvDto>
{
    public PizzaMapping()
    {
        Map(x => x.Id)
            .Name("pizza_id")
            .Validate(field =>
            {
                if (string.IsNullOrEmpty(field.Field))
                    throw new Exception($"Id is required");
                return true;
            });

        Map(x => x.PizzaTypeId)
            .Name("pizza_type_id")
            .Validate(field =>
            {
                if (string.IsNullOrEmpty(field.Field))
                    throw new Exception($"Name is required");
                return true;
            });

        Map(x => x.Size)
            .Name("size")
            .Validate(field =>
            {
                if (string.IsNullOrEmpty(field.Field))
                    throw new Exception($"Category is required");
                return true;
            });

        Map(x => x.Price)
            .Name("price")
            .Validate(field =>
            {
                if (string.IsNullOrEmpty(field.Field))
                    throw new Exception($"Ingredients is required");

                TryParseDouble("Dollars", field.Field);

                return true;
            });
    }

    private static void TryParseDouble(string fieldName, string fieldValue)
    {
        if (!string.IsNullOrEmpty(fieldValue) && !double.TryParse(fieldValue, out _))
            throw new Exception($"{fieldName} value {fieldValue} is not in the correct decimal format");
    }
}
