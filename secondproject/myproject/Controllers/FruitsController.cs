using Microsoft.AspNetCore.Mvc;
namespace StudentApi.Controllers;
 
[ApiController]
[Route("[controller]")]
public class FruitController : ControllerBase
{
    [HttpGet]
    public string[] Get()
    {
        return ["Apple","Bana","Orange"];
    }
}