using Mapster;
using Microsoft.Extensions.Options;
using UniversityOrderAPI.BLL.Command;
using UniversityOrderAPI.DAL;

namespace UniversityOrderAPI.BLL.Client;

public record CreateClientCommand(
    int StudentStoreId,
    ClientDTO Client
) : ICommand;

public record CreateClientCommandResult(
    ClientDTO Client
) : ICommandResult;

public class CreateClientCommandHandler : Command<UniversityOrderAPIDbContext>,
    ICommandHandler<CreateClientCommand, CreateClientCommandResult>, IConfig
{
    public CreateClientCommandHandler(UniversityOrderAPIDbContext dbContext) : base(dbContext) { }

    public CreateClientCommandHandler(UniversityOrderAPIDbContext dbContext, IOptions<Config> config) : this(dbContext)
    {
        Config = config;
    }

    public Task<CreateClientCommandResult> Handle(CreateClientCommand request, CancellationToken? cancellationToken)
    {
        var maxSlotsPerStudent = Config.Value.MaxSlotsPerStudent;

        var countOfClientsPerStudentStore = DbContext.Clients
            .Count(el => el.StudentStoreId == request.StudentStoreId);

        if (countOfClientsPerStudentStore >= maxSlotsPerStudent)
            throw new Exception($"Max amount of clients per student store was exceeded, allowed: {maxSlotsPerStudent}");
        
        if (string.IsNullOrEmpty(request.Client.FirstName))
            throw new Exception("Client name null or empty");

        var phoneNumber = request.Client.PhoneNumber;

        if (phoneNumber != null)
        {
            if (phoneNumber.Length is > 20 or < 4)
                throw new Exception($"Phone number length must contain 4-20 characters, but received {phoneNumber.Length}");
            
            if (!IsDigitsOnly(phoneNumber))
                throw new Exception("Phone number must contain only digits");
        }

        var newClient = new DAL.Models.Client
        {
            StudentStoreId = request.StudentStoreId,
            Sex = request.Client.Sex,
            FirstName = request.Client.FirstName,
            LastName = request.Client.LastName,
            Email = request.Client.Email,
            PhoneNumber = request.Client.PhoneNumber,
            IsDeleted = false
        };

        DbContext.Clients.Add(newClient);

        DbContext.SaveChanges();

        return Task.FromResult(new CreateClientCommandResult(
            newClient.Adapt<ClientDTO>()));
    }

    private static bool IsDigitsOnly(string str)
    {
        return str.All(c => c is >= '0' and <= '9');
    }

    public IOptions<Config> Config { get; set; }
}