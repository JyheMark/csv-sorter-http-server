using csv_sorter.server.Models;

namespace csv_sorter.server.Utils;

/// <summary>
///     Basic CSV writer.
///     Probably better to use a package for this, but rolled a basic one for test task.
/// </summary>
public static class CsvWriter
{
    public static string WriteCsv(CsvTable csvTable)
    {
        var result = WriteCsvHeader(csvTable.Fields, csvTable.Delimiter);

        foreach (var row in csvTable.Rows)
            result += WriteRecordRow(row, csvTable.Delimiter);

        return result;
    }

    private static string WriteRecordRow(string[] record, char delimiter)
    {
        var result = Environment.NewLine;

        result = AddDelimitedData(record, delimiter, result);

        return result;
    }

    private static string WriteCsvHeader(string[] fields, char delimiter)
    {
        var result = AddDelimitedData(fields, delimiter);

        return result;
    }

    private static string AddDelimitedData(string[] dataToAdd, char delimiter, string appendTo = "")
    {
        for (var i = 0; i < dataToAdd.Length; i++)
        {
            appendTo += $"{dataToAdd[i]}";

            if (i < dataToAdd.Length - 1)
                appendTo += delimiter;
        }

        return appendTo;
    }
}