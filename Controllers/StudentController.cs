

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentApp.Authorization;
using StudentApp.Domain.Entities;
using StudentApp.Domain.Models.Student;
using StudentApp.Extensions;
using StudentApp.Persistence.DataBase;
using StudentApp.Security;
using StudentApp.Services;

namespace StudentApp.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentController(StudentService studentService)
    : ControllerBase
{

    [HttpGet("{id}")]
    [CheckPermission(Permission.ReadProducts)]
    public Task<Student?> GetByIdAsync(int id) 
        => studentService.GetByIdAsyncService(id);

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
    [CheckPermission(Permission.AddProduct)]
    public  IActionResult AddAsync([FromBody] AddStudentModel studentModel)
    //=> await studentService.AddStudentAsyncService(studentModel);
    {
        var s = studentService.AddStudentAsyncService(studentModel);
        if (s.Result == 0)
        {
            return BadRequest("Error") ;
        }
        return Ok(s.Result);
    }
    [HttpDelete("{id}")]
    [CheckPermission(Permission.AddProduct)]
    public async Task<int> Delete(int id)
    => await studentService.DeleteAsyncService(id);

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
    [CheckPermission(Permission.AddProduct)]
    public IActionResult UpdateAsync([FromBody] UpdateStudentModel updateStudent)
    // =>await studentService.UpdateAsyncService(updateStudent);
    {
        var s = studentService.UpdateAsyncService(updateStudent);
        if (s.Result == 0)
        {
            return BadRequest("Error");
        }
        return Ok(s.Result);
    }
}