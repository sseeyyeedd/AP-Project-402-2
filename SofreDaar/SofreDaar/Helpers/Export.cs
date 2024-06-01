using System.Globalization;
using System.IO;

namespace SofreDaar.Helpers;
public class CsvWriter : IDisposable
{
    private StreamWriter _streamWriter;
    private bool _disposedValue;

    public CsvWriter(StreamWriter streamWriter)
    {
        _streamWriter = streamWriter;
    }

    public void WriteRecords<T>(IEnumerable<T> records)
    {
        var properties = typeof(T).GetProperties();
        var header = string.Join(",", properties.Select(p => p.Name));
        _streamWriter.WriteLine(header);

        foreach (var record in records)
        {
            var line = string.Join(",", properties.Select(p => p.GetValue(record)));
            _streamWriter.WriteLine(line);
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                _streamWriter.Dispose();
            }

            _disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
public class Export
{
    public static void ExportToCsv<T>(IEnumerable<T> records, string path)
    {
        using (var streamWriter = new StreamWriter(path))
        {
            using (var csvWriter = new CsvWriter(streamWriter))
            {
                csvWriter.WriteRecords(records);
            }
        }
    }
}