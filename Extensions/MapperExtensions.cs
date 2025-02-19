using StudentApp.Domain.Entities;
using StudentApp.Domain.Models.Student;

namespace StudentApp.Extensions;

public static class MapperExtensions
{
    public static Student ToEntity(this AddStudentModel model)
        => new(model.Name, model.Age);
    public static Student ToEntity(this UpdateStudentModel model)
        => new(model.Name, model.Age);
}