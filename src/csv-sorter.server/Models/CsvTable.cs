namespace csv_sorter.server.Models;

public class CsvTable
{
    public CsvTable(string[] fields, string[][] rows, char delimiter)
    {
        Fields = fields;
        Rows = rows;
        Delimiter = delimiter;
    }

    public string[] Fields { get; }
    public string[][] Rows { get; private set; }
    public char Delimiter { get; }
    public int Count => Rows.Length;
    public string[] this[int index] => Rows[index];

    public void SortOnColumn(int columnNumber)
    {
        if (!ColumnNumberIsValid(columnNumber))
            throw new InvalidOperationException("Column number is invalid");

        if (ColumnIsNumeric(columnNumber))
            SortNumeric(columnNumber);
        else SortAlphabetic(columnNumber);
    }

    public bool ColumnNumberIsValid(int columnNumber)
    {
        return columnNumber <= Fields.Length && columnNumber >= 0;
    }

    private void SortAlphabetic(int columnNumber)
    {
        var sortList = Rows.Select((strings, i) => new
        {
            Index = i,
            Value = strings[columnNumber]
        }).OrderBy(arg => arg.Value);

        Rows = sortList.Select(entry => Rows[entry.Index]).ToArray();
    }

    private void SortNumeric(int columnNumber)
    {
        var sortList = Rows.Select((strings, i) => new
        {
            Index = i,
            Value = double.Parse(strings[columnNumber])
        }).OrderBy(arg => arg.Value);

        Rows = sortList.Select(entry => Rows[entry.Index]).ToArray();
    }

    private bool ColumnIsNumeric(int columnNumber)
    {
        return double.TryParse(Rows[0][columnNumber], out _);
    }
}