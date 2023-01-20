using Mapster;
using UniversityOrderAPI.BLL.Category;
using UniversityOrderAPI.BLL.Client;
using UniversityOrderAPI.BLL.Manufacturer;
using UniversityOrderAPI.BLL.Order;
using UniversityOrderAPI.BLL.Product;
using UniversityOrderAPI.DAL.Models;
using UniversityOrderAPI.Models.Category;
using UniversityOrderAPI.Models.Client;
using UniversityOrderAPI.Models.Manufacturer;
using UniversityOrderAPI.Models.Order;
using UniversityOrderAPI.Models.Product;

namespace UniversityOrderAPI.Mappers;

public class RegisterMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Category, CategoryDTO>().RequireDestinationMemberSource(true);
        config.NewConfig<CategoryDTO, CategoryAPIDTO>().RequireDestinationMemberSource(true);
        
        config.NewConfig<Manufacturer, ManufacturerDTO>().RequireDestinationMemberSource(true);
        config.NewConfig<ManufacturerDTO, ManufacturerAPIDTO>().RequireDestinationMemberSource(true);
        
        config.NewConfig<Product, ProductDTO>().RequireDestinationMemberSource(true);
        config.NewConfig<ProductDTO, ProductAPIDTO>().RequireDestinationMemberSource(true);
        
        config.NewConfig<Client, ClientDTO>().RequireDestinationMemberSource(true);
        config.NewConfig<ClientDTO, ClientAPIDTO>().RequireDestinationMemberSource(true);

        config.NewConfig<Order, OrderDTO>().RequireDestinationMemberSource(true);
        config.NewConfig<OrderDTO, OrderAPIDTO>().RequireDestinationMemberSource(true);
    }
}