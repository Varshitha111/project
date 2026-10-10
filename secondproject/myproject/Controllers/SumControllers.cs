using Microsoft.AspNetCore.Mvc;
namespace myproject.Controllers;
// using myproject.Models;

[ApiController]
[Route("[controller]")]

public class SumControllers : ControllerBase
{
    [HttpPost]
    public int Post(SumRequest request)
    {
        return request.FirstNumber+request.SecondNumber;
    }

}
public sealed record SumRequest(int FirstNumber,int SecondNumber);