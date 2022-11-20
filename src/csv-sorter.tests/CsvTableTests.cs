using csv_sorter.server.Models;
using csv_sorter.server.Utils;
using FluentAssertions;

namespace csv_sorter.tests;

public class CsvTableTests
{
    private const string _filePath = "./test_comma.csv";
    private readonly CsvTable _sut;

    public CsvTableTests()
    {
        _sut = CsvReader.ReadFromFile(_filePath);
    }

    [Fact]
    public void Sort_on_string_field_should_sort_alphabetically()
    {
        _sut.SortOnColumn(1);

        _sut.Rows[0][0].Should().Be("63");
        _sut.Rows[0][1].Should().Be("Aydin");
        _sut.Rows[0][2].Should().Be("60");

        _sut.Rows[4][0].Should().Be("49");
        _sut.Rows[4][1].Should().Be("Vincenzo");
        _sut.Rows[4][2].Should().Be("32");
    }

    [Fact]
    public void Sort_on_numeric_field_should_sort_numerically()
    {
        _sut.SortOnColumn(0);

        _sut.Rows[0][0].Should().Be("49");
        _sut.Rows[0][1].Should().Be("Vincenzo");
        _sut.Rows[0][2].Should().Be("32");

        _sut.Rows[4][0].Should().Be("95");
        _sut.Rows[4][1].Should().Be("Jacques");
        _sut.Rows[4][2].Should().Be("38");
    }
}