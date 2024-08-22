using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;

namespace PizzaPlace.Application.Common.Helpers.CsvMappings;
public class OrderMapping : ClassMap<OrderCsvDto>
{
    public OrderMapping()
    {
        Map(x => x.Id)
            .Name("order_id")
            .Validate(field =>
            {
                if (string.IsNullOrEmpty(field.Field))
                    throw new Exception($"Id is required");
                return true;
            });

        Map(x => x.Date)
          .Name("date")
          .Validate(field =>
          {
              if (string.IsNullOrEmpty(field.Field))
                  throw new Exception($"Id is required");
              return true;
          });

        Map(x => x.Time)
          .Name("time")
          .Validate(field =>
          {
              if (string.IsNullOrEmpty(field.Field))
                  throw new Exception($"Id is required");
              return true;
          });
    }
}
