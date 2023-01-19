using Mapster;
using UniversityOrderAPI.BLL.Category;
using UniversityOrderAPI.BLL.Client;
using UniversityOrderAPI.DAL.Models;
using UniversityOrderAPI.Models.Category;
using UniversityOrderAPI.Models.Client;

namespace UniversityOrderAPI.Mappers;

public class RegisterMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Client, ClientDTO>().RequireDestinationMemberSource(true);
        config.NewConfig<ClientDTO, ClientAPIDTO>().RequireDestinationMemberSource(true);
    }
}