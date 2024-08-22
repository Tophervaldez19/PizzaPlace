using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using PizzaPlace.Application.Common.Helpers.CsvDtos;
using PizzaPlace.Application.Common.Helpers.CsvMappings;

namespace PizzaPlace.Application.Common.Helpers;
public static class CsvParser
{
    public static List<PizzaTypeCsvDto> ParsePizzaTypeDtoFromCsv(Stream csvStream)
    {
        using var reader = new StreamReader(csvStream);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        csv.Context.RegisterClassMap<PizzaTypeMapping>();

        return csv.GetRecords<PizzaTypeCsvDto>().ToList();
    }

    public static List<PizzaCsvDto> ParsePizzaDtoFromCsv(Stream csvStream)
    {
        using var reader = new StreamReader(csvStream);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        csv.Context.RegisterClassMap<PizzaMapping>();

        return csv.GetRecords<PizzaCsvDto>().ToList();
    }
}
