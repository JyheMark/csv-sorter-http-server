using csv_sorter.server.Models;
using csv_sorter.server.Utils;
using Microsoft.AspNetCore.Mvc;

namespace csv_sorter.server.Controllers;

[ApiController]
[Route("sort")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class CsvSortController : ControllerBase
{
    [HttpPost]
    [Route("{sortColumnIndex:int}")]
    public async Task<ActionResult<string>> SortCsvByColumn(int sortColumnIndex)
    {
        var csvAsString = await ReadCsvStringFromRequest();

        if (string.IsNullOrWhiteSpace(csvAsString))
            return BadRequest();

        CsvTable csvTable = CsvReader.ReadFromString(csvAsString);

        if (!csvTable.ColumnNumberIsValid(sortColumnIndex))
            return BadRequest();

        csvTable.SortOnColumn(sortColumnIndex);

        return Ok(CsvWriter.WriteCsv(csvTable));
    }

    private async Task<string> ReadCsvStringFromRequest()
    {
        using var reader = new StreamReader(Request.Body);
        return await reader.ReadToEndAsync();
    }
}