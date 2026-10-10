using Microsoft.AspNetCore.Mvc;
namespace myproject.Controllers;
// using myproject.Models;

[ApiController]
[Route("[controller]")]
public class SubtractControllers : ControllerBase
{
    [HttpPost]
    public int Post(SubtractRequest request)
    {
        return request.FirstNumber-request.SecondNumber;
    }

}
public sealed record SubtractRequest(int FirstNumber,int SecondNumber);