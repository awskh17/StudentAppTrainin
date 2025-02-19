using Microsoft.EntityFrameworkCore;
using StudentApp.Domain.Entities;
using StudentApp.Domain.Models.Student;
using StudentApp.Extensions;
using StudentApp.Persistence.DataBase;
using StudentApp.Validaors;

namespace StudentApp.Services;
    public class StudentService(AppDbContext dbContext,StudentValidator studentValidator)
    {
        public Task<Student?> GetByIdAsyncService(int id)
          => dbContext.Students.AsNoTracking()
                        .FirstOrDefaultAsync(s => s.Id == id);
        public async Task<int> AddStudentAsyncService(AddStudentModel studentModel)
        {
        Student student = studentModel.ToEntity();
        var results = studentValidator.Validate(student);
        if (!results.IsValid) 
        {
            return 0;
        }
        await dbContext.Students.AddAsync(student);
        await dbContext.SaveChangesAsync();
        return student.Id;
        }

    public async Task<int> DeleteAsyncService(int id)
        {
        Student? student = await dbContext.Students.FirstOrDefaultAsync(s => s.Id == id);
        if (student != null)
            {
            dbContext.Students.Remove(student);
            await dbContext.SaveChangesAsync();
            return id;
            }
        return 0;
        }
    public async Task<int> UpdateAsyncService(UpdateStudentModel updateStudent)
    {
        Student? student = await dbContext.Students.FirstOrDefaultAsync(s => s.Id == updateStudent.Id);
        if (student != null)
        {
            var results = studentValidator.Validate(updateStudent.ToEntity());
            if (!results.IsValid)
            {
                return 0;
            }
            student.Update(updateStudent);
            await dbContext.SaveChangesAsync();
            return student.Id;
        }
        return 0;
    }

}

