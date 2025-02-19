using StudentApp.Domain.Models.Student;

namespace StudentApp.Domain.Entities;

public class Student
{
    public Student(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
    public int Age { get; private set; }

    public void Update(UpdateStudentModel updateStudent)
    {
        Name = updateStudent.Name;
        Age = updateStudent.Age;
    }
}
