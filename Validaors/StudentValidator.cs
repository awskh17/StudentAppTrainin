using FluentValidation;
using StudentApp.Domain.Entities;

namespace StudentApp.Validaors;

public class StudentValidator:AbstractValidator<Student>
{
    public StudentValidator() {
        RuleFor(s => s.Id).NotNull();
        RuleFor(s => s.Name).Length(0, 15);
        RuleFor(s => s.Age).InclusiveBetween(15, 60);
            }

}
