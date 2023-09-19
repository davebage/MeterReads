namespace MeterReads.Services;

public interface ICsvService
{
    IEnumerable<T> ReadCsv<T>(Stream file);
}