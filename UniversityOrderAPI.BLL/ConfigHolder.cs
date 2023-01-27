using System.Reflection;
using Newtonsoft.Json;
using UniversityOrderAPI.BLL.Category;
using UniversityOrderAPI.BLL.Client;
using UniversityOrderAPI.BLL.Command;
using UniversityOrderAPI.BLL.Manufacturer;
using UniversityOrderAPI.BLL.Order;
using UniversityOrderAPI.BLL.Product;
using UniversityOrderAPI.BLL.Purchase;

namespace UniversityOrderAPI.BLL;

public class ConfigModel
{
    [JsonProperty("max_categories_per_user")]
    public int MaxCategoriesPerUser { get; set; }

    [JsonProperty("max_clients_per_user")]
    public int MaxClientsPerUser { get; set; }

    [JsonProperty("max_manufacturers_per_user")]
    public int MaxManufacturersPerUser { get; set; }

    [JsonProperty("max_order_items_per_user")]
    public int MaxOrderItemsPerUser { get; set; }

    [JsonProperty("max_orders_per_user")]
    public int MaxOrdersPerUser { get; set; }

    [JsonProperty("max_products_per_user")]
    public int MaxProductsPerUser { get; set; }

    [JsonProperty("max_purchases_per_user")]
    public int MaxPurhcasesPerUser { get; set; }
}

public abstract class ConfigHelper
{
    private const string ConfigFilePath = "config.json";

    public static int GetMaxNPerUser(ICommand request)
    {
        var configModel = JsonConvert.DeserializeObject<ConfigModel>(File.ReadAllText(ConfigFilePath));
        
        return request switch
        {
            CreateCategoryCommand => configModel!.MaxCategoriesPerUser,
            CreateClientCommand => configModel!.MaxClientsPerUser,
            CreateManufacturerCommand => configModel!.MaxManufacturersPerUser,
            CreateOrderCommand => configModel!.MaxOrdersPerUser,
            CreateProductCommand => configModel!.MaxProductsPerUser,
            CreatePurchaseCommand => configModel!.MaxPurhcasesPerUser,
            _ => throw new Exception("Использована команда с проверкой на кол-во допустимых значений для пользователя в БД, но в конфиге этого значения нет")
        };
    }
}