using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();


var app = builder.Build();
var connectionString=builder.Configuration.GetConnectionString("StudentDb");
using var connection=new SqlConnection(connectionString);
connection.Open();
using var command=new SqlCommand("select Id,Name from Students",connection);
using var reader=command.ExecuteReader();
while (reader.Read())
{
    Console.WriteLine(reader["Id"]+" "+reader["Name"]);
}
// connection.Open();
app.UseSwagger();
app.UseSwaggerUI();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
