using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

[ApiController]
[Route("api/[controller]")]
public class StudentsController:ControllerBase
{

    private static List<Students> students=new List<Students>
    {
        new Students {Id=1,Name="Varshitha"},
        new Students {Id=2,Name="harshitha"}
    };
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(students);
    }
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var existingid=students.FirstOrDefault(s=>s.Id==id);
        if (existingid!=null)
        {
            return Ok(existingid);
        }
        return NotFound("student not found");
    }
    [HttpPut]
    public IActionResult Create(Students student)
    {
        var existingStudent=students.FirstOrDefault(s=>s.Id==student.Id);
        if (existingStudent==null)
        {
            students.Add(student);
            return Ok(students);
        }
        return BadRequest("Student already exists");
    }
    [HttpPut("{id}")]
    public IActionResult Put(int id,string name)
    {
        var existedstudent=students.FirstOrDefault(s=>s.Id==id);
        if (existedstudent!=null)
        {
            existedstudent.Name=name;
            return Ok(students);
        }
        else
        {
            return NotFound("student not found");
        }
    }
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var existingone=students.FirstOrDefault(s=>s.Id==id);
        if (existingone!=null)
        {
            students.Remove(existingone);
            return Ok("Deleted student");

        }
        else
        {
            return NotFound("not found");
        }
    }
}