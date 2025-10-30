using Microsoft.AspNetCore.Mvc;

namespace LMTestFileParser.Controllers;


[Route("api/[controller]")]
[ApiController]
public class CSVParserController : ControllerBase
{

    [HttpPost]
    public IActionResult Post(string bankName, IFormFile csvFile)
    {
        return Ok();
    }
}