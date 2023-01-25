using Mapster;
using UniversityOrderAPI.BLL.Category;
using UniversityOrderAPI.BLL.Client;
using UniversityOrderAPI.BLL.Manufacturer;
using UniversityOrderAPI.BLL.Order;
using UniversityOrderAPI.BLL.OrderItem;
using UniversityOrderAPI.BLL.Product;
using UniversityOrderAPI.BLL.Purchase;
using UniversityOrderAPI.DAL.Models;
using UniversityOrderAPI.Models.Category;
using UniversityOrderAPI.Models.Client;
using UniversityOrderAPI.Models.Manufacturer;
using UniversityOrderAPI.Models.Order;
using UniversityOrderAPI.Models.OrderItem;
using UniversityOrderAPI.Models.Product;
using UniversityOrderAPI.Models.Purchase;

namespace UniversityOrderAPI.Mappers;

public class RegisterMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Category, CategoryDTO>().RequireDestinationMemberSource(true);
        config.NewConfig<CategoryDTO, CategoryAPIDTO>().RequireDestinationMemberSource(true);
        
        config.NewConfig<Manufacturer, ManufacturerDTO>().RequireDestinationMemberSource(true);
        config.NewConfig<ManufacturerDTO, ManufacturerAPIDTO>().RequireDestinationMemberSource(true);
        
        config.NewConfig<Product, ProductDTO>()
            .Map(e => e.CategoryName, e => e.Category.Name)
            .Map(e => e.ManufacturerName, e => e.Manufacturer.Name)
            .RequireDestinationMemberSource(true);
        config.NewConfig<ProductDTO, ProductAPIDTO>().RequireDestinationMemberSource(true);
        
        config.NewConfig<Client, ClientDTO>().RequireDestinationMemberSource(true);
        config.NewConfig<ClientDTO, ClientAPIDTO>().RequireDestinationMemberSource(true);

        config.NewConfig<Order, OrderDTO>().RequireDestinationMemberSource(true);
        config.NewConfig<OrderDTO, OrderAPIDTO>().RequireDestinationMemberSource(true);

        config.NewConfig<OrderItem, OrderItemDTO>().RequireDestinationMemberSource(true);
        config.NewConfig<OrderItemDTO, OrderItemAPIDTO>().RequireDestinationMemberSource(true);

        config.NewConfig<Purchase, PurchaseDTO>().RequireDestinationMemberSource(true);
        config.NewConfig<PurchaseDTO, PurchaseAPIDTO>().RequireDestinationMemberSource(true);
    }
}