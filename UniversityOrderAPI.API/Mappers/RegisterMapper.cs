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

public static class RegisterMapper
{
    public static void RegisterMapsterConfiguration(this IServiceCollection services)
    {
        // Category
        TypeAdapterConfig<Category, CategoryDTO>.NewConfig();
        TypeAdapterConfig<CategoryDTO, CategoryAPIDTO>.NewConfig();

        // Manufacturer
        TypeAdapterConfig<Manufacturer, ManufacturerDTO>.NewConfig();
        TypeAdapterConfig<ManufacturerDTO, ManufacturerAPIDTO>.NewConfig();
        
        // Product
        TypeAdapterConfig<Product, ProductDTO>
            .NewConfig()
            .Map(productDTO => productDTO.CategoryName, product => product.Category.Name)
            .Map(productDTO => productDTO.ManufacturerName, product => product.Manufacturer.Name);
        TypeAdapterConfig<ProductDTO, ProductAPIDTO>.NewConfig();

        // Client
        TypeAdapterConfig<Client, ClientDTO>.NewConfig();
        TypeAdapterConfig<ClientDTO, ClientAPIDTO>.NewConfig();

        // Order
        TypeAdapterConfig<Order, OrderDTO>
            .NewConfig()
            .Map(orderDTO => orderDTO.ClientFIO, order => $"{order.Client.LastName} {order.Client.FirstName}");
        TypeAdapterConfig<OrderDTO, OrderAPIDTO>.NewConfig();

        // OrderItem
        TypeAdapterConfig<OrderItem, OrderItemDTO>.NewConfig();
        TypeAdapterConfig<OrderItemDTO, OrderItemAPIDTO>.NewConfig();

        // Purchase
        TypeAdapterConfig<Purchase, PurchaseDTO>.NewConfig();
        TypeAdapterConfig<PurchaseDTO, PurchaseAPIDTO>.NewConfig();

    }
}