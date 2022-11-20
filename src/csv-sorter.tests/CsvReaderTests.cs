using csv_sorter.server.Models;
using csv_sorter.server.Utils;
using FluentAssertions;

namespace csv_sorter.tests;

public class CsvReaderTests
{
    private const string _filePathComma = "./test_comma.csv";
    private const string _filePathSemicolon = "./test_semicolon.csv";
    private const string _filePathTab = "./test_tab.csv";

    [Fact]
    public void ReadScoreRecordsFromCsv_should_detect_delimiter_comma()
    {
        CsvTable result = CsvReader.ReadFromFile(_filePathComma);

        result.Should().BeOfType<CsvTable>();
        result.Delimiter.Should().Be(',');

        result.Fields.Should().HaveCount(3);
        result.Fields[0].Should().Be("Score");
        result.Fields[1].Should().Be("Name");
        result.Fields[2].Should().Be("Age");

        result.Count.Should().Be(5);
        result[0][0].Should().Be("49");
        result[0][1].Should().Be("Vincenzo");
        result[0][2].Should().Be("32");
    }

    [Fact]
    public void ReadScoreRecordsFromCsv_should_detect_delimiter_semicolon()
    {
        CsvTable result = CsvReader.ReadFromFile(_filePathSemicolon);

        result.Should().BeOfType<CsvTable>();
        result.Delimiter.Should().Be(';');

        result.Fields.Should().HaveCount(3);
        result.Fields[0].Should().Be("Score");
        result.Fields[1].Should().Be("Name");
        result.Fields[2].Should().Be("Age");

        result.Count.Should().Be(5);
        result[0][0].Should().Be("49");
        result[0][1].Should().Be("Vincenzo");
        result[0][2].Should().Be("32");
    }

    [Fact(Skip = "Tab delimiter not supported")]
    public void ReadScoreRecordsFromCsv_should_detect_delimiter_tab()
    {
        CsvTable result = CsvReader.ReadFromFile(_filePathTab);

        result.Should().BeOfType<CsvTable>();
        result.Delimiter.Should().Be(' ');

        result.Fields.Should().HaveCount(3);
        result.Fields[0].Should().Be("Score");
        result.Fields[1].Should().Be("Name");
        result.Fields[2].Should().Be("Age");

        result.Count.Should().Be(5);
        result[0][0].Should().Be("49");
        result[0][1].Should().Be("Vincenzo");
        result[0][2].Should().Be("32");
    }
}