using Microsoft.Extensions.Options;

namespace UniversityOrderAPI.BLL;

public interface IConfig
{
    public IOptions<Config> Config { get; set; }
}