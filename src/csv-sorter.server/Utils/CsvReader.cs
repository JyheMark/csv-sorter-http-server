using csv_sorter.server.Models;

namespace csv_sorter.server.Utils;

public interface ICsvReader
{
    void ReadFromFile(string filePath);
}

/// <summary>
///     Basic CSV reader.
///     Probably better to use a package for this, but rolled a basic one for test task.
/// </summary>
public static class CsvReader
{
    private static readonly char[] _possibleDelimiters = { ',', ';' };

    public static CsvTable ReadFromFile(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentException();

        if (!File.Exists(filePath))
            throw new IOException($"No file exists at {filePath}");

        var csvStrings = File.ReadAllLines(filePath);
        return Read(csvStrings);
    }

    public static CsvTable ReadFromString(string csvString)
    {
        if (string.IsNullOrWhiteSpace(csvString))
            throw new ArgumentException();

        //No validation, assumes CSV is well formed
        var csvStrings = csvString.Split(Environment.NewLine);
        return Read(csvStrings);
    }

    private static CsvTable Read(string[] csvStrings)
    {
        var delimiter = DetectDelimiter(csvStrings);
        var csvAsArray = BuildCsvAsArray(csvStrings, delimiter);

        return new CsvTable(csvAsArray[0], csvAsArray.Skip(1).ToArray(), delimiter);
    }

    private static string[][] BuildCsvAsArray(IReadOnlyCollection<string> csvString, char delimiter)
    {
        return csvString.Select(row =>
                row.Split(delimiter).ToArray())
            .ToArray();
    }

    private static char DetectDelimiter(IReadOnlyCollection<string> csvRows)
    {
        Dictionary<char, int> delimiterCount = CountInstancesOfValidDelimiter(csvRows); //Assuming that the delimiter type with highest frequency is the delimiter

        return delimiterCount.ToList().MaxBy(pair => pair.Value).Key;
    }

    private static Dictionary<char, int> CountInstancesOfValidDelimiter(IReadOnlyCollection<string> csvRows)
    {
        const int rowsToProcess = 10;
        var delimiterCount = new Dictionary<char, int>();

        foreach (var row in csvRows.Take(rowsToProcess))
        foreach (var delimiter in _possibleDelimiters)
            if (delimiterCount.ContainsKey(delimiter))
                delimiterCount[delimiter] += row.Count(c => c == delimiter);
            else
                delimiterCount.Add(delimiter, row.Count(c => c == delimiter));

        return delimiterCount;
    }
}