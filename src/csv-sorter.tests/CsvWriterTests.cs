using csv_sorter.server.Models;
using csv_sorter.server.Utils;
using FluentAssertions;

namespace csv_sorter.tests;

public class CsvWriterTests
{
    private readonly string _expectedCsvString = "Score,Name,Age\r\n10,Test,25\r\n99,Test2,56";
    private readonly CsvTable _testCsvTable;

    public CsvWriterTests()
    {
        _testCsvTable = new CsvTable
        (
            new[] { "Score", "Name", "Age" },
            new[]
            {
                new[] { "10", "Test", "25" },
                new[] { "99", "Test2", "56" }
            },
            ','
        );
    }

    [Fact]
    public void CsvWriter_should_return_csv()
    {
        var csvString = CsvWriter.WriteCsv(_testCsvTable);
        csvString.Should().BeEquivalentTo(_expectedCsvString);
    }
}