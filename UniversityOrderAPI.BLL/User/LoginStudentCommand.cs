using Microsoft.EntityFrameworkCore;
using UniversityOrderAPI.BLL.Command;
using UniversityOrderAPI.DAL;

namespace UniversityOrderAPI.BLL.User;

public record LoginStudentCommand(
    string Identifier) : ICommand
{
    
}



public record LoginStudentCommandResult(
    StudentDTO student) : ICommandResult
{
    
}

public class LoginStudentCommandHandler : Command<UniversityOrderAPIDbContext>, ICommandHandler<LoginStudentCommand, LoginStudentCommandResult>
{
    public LoginStudentCommandHandler(UniversityOrderAPIDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<LoginStudentCommandResult> Handle(LoginStudentCommand request, CancellationToken? cancellationToken)
    {
        var student = await DbContext.Students
            .Include(el=>el.StudentStore)
            .SingleOrDefaultAsync(el => el.Identifier == request.Identifier);

        if (student == null)
            throw new Exception("Идентификатор студента указан неверно");
        
        if (student.StudentStore == null)
            throw new Exception("Студент не привязан ни к одному студенту");

        return new LoginStudentCommandResult(new StudentDTO
        {
            StudentStoreId = student.StudentStore.Id,
            StudentId = student.Id
        });
    }
}