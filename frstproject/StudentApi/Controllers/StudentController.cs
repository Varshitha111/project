using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;


[ApiController]
[Route("api/[controller]")]
public class StudentsController:ControllerBase
{
    private static List<Student> students= new List<Student>
    {
        new Student {Id=1,Name="John"},
        new Student {Id=2,Name="Johnny"}
    };
    private readonly string connectionString;
    
    public StudentsController(IConfiguration configuration)
    {
        connectionString=configuration.GetConnectionString("StudentDb");
    }
    [HttpGet]
    public IActionResult GetAll()
{
    var students=new List<Student>();
    using var connection=new SqlConnection(connectionString);
    connection.Open();
    using var command=new SqlCommand("select Id,Name from Students",connection);
    using var reader=command.ExecuteReader();
    while (reader.Read())
    {
        int id=Convert.ToInt32(reader["Id"]);
        string name=reader["Name"].ToString();
        Student student=new Student{Id=id,Name=name};
        students.Add(student);
    }
        
    return Ok(students);
}
[HttpGet("{id}")]
public IActionResult GetById(int id)
    {
        var students=new List<Student>();
        using var connection=new SqlConnection(connectionString);
        connection.Open();
        using var command=new SqlCommand("select * from Students where Id=@id",connection);
        command.Parameters.AddWithValue("@id",id);
        using var reader=command.ExecuteReader();
        if (!reader.Read())
        {
            return NotFound();
        }
        else {
            int ids=Convert.ToInt32(reader["Id"]);
            string name=reader["Name"].ToString();
            Student student=new Student{Id=id,Name=name};
        return Ok(student);
        }
    }
[HttpPost]
public IActionResult Create(Student student)
    {
        var students=new List<Student>();
        using var connection=new SqlConnection(connectionString);
        connection.Open();
        using var checkCommand = new SqlCommand("SELECT COUNT(*) FROM Students WHERE Id = @id",connection);
        checkCommand.Parameters.AddWithValue("@id", student.Id);
        int count = Convert.ToInt32(checkCommand.ExecuteScalar());
        if (count > 0)
        {
        return Conflict("A student with this ID already exists.");
        }

        using var command=new SqlCommand("insert into Students(Id,Name) values(@id,@name)",connection);
        command.Parameters.AddWithValue("@id",student.Id);
        command.Parameters.AddWithValue("@name",student.Name);
        command.ExecuteNonQuery();
        return Ok(student);
    }

[HttpPut("{id}")]
public IActionResult Update(int id,string name)
    {
        var existingstudent=students.FirstOrDefault(s=>s.Id==id);
        if(existingstudent!=null)
        {
            existingstudent.Name=name;
            return Ok(existingstudent);
        }
        else
        {

            return NotFound("user not found");
        }
    }
[HttpDelete("{id}")]
public IActionResult Delete(int id)
    {
        var student=students.FirstOrDefault(s=>s.Id==id);
        if (student!=null)
        {
            students.Remove(student);
            return Ok("Student deleted");
        }
        else
        {
            return NotFound("student not found or already deleted");
        }
    }
}
