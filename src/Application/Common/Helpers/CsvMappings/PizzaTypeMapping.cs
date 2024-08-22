using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using PizzaPlace.Application.Common.Helpers.CsvDtos;

namespace PizzaPlace.Application.Common.Helpers.CsvMappings;
public class PizzaTypeMapping : ClassMap<PizzaTypeCsvDto>
{
    public PizzaTypeMapping()
    {
        Map(x => x.Id)
            .Name("pizza_type_id")
            .Validate(field =>
            {
                if (string.IsNullOrEmpty(field.Field))
                    throw new Exception($"Id is required");
                return true;
            });

        Map(x => x.Name)
            .Name("name")
            .Validate(field =>
            {
                if (string.IsNullOrEmpty(field.Field))
                    throw new Exception($"Name is required");
                return true;
            });

        Map(x => x.Category)
            .Name("category")
            .Validate(field =>
            {
                if (string.IsNullOrEmpty(field.Field))
                    throw new Exception($"Category is required");
                return true;
            });

        Map(x => x.Ingredients)
            .Name("ingredients")
            .Validate(field =>
            {
                if (string.IsNullOrEmpty(field.Field))
                    throw new Exception($"Ingredients is required");
                return true;
            });
    }
}
