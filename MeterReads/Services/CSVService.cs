using System.Globalization;
using CsvHelper;

namespace MeterReads.Services;

public class CsvService : ICsvService
{
    public IEnumerable<T> ReadCsv<T>(Stream file)
    {
        var reader = new StreamReader(file);
        var csv = new CsvReader(reader, CultureInfo.GetCultureInfo("en-GB"));

        var records = csv.GetRecords<T>();
        return records;
    }
}