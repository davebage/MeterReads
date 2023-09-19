using MeterReads.Models;
using MeterReads.Services;
using Microsoft.AspNetCore.Mvc;

namespace MeterReads.Controllers;

[ApiController]
[Route("meter-reading-uploads")]
public class MeterReadingFileController : ControllerBase
{
    private readonly IMeterReadFileService _fileService;

    public MeterReadingFileController(IMeterReadFileService fileService)
    {
        _fileService = fileService;
    }

    [HttpPost]
    public async Task<ActionResult> PostSingleFile([FromForm] FileUploadModel model)
    {
        try
        {
            var result = await _fileService.ProcessMeterReadFileAsync(model.FileDetails);

            return Ok(result);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }
}