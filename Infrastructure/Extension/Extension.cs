using Infrastructure.Interface;
using Infrastructure.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extension;

public static class Extension
{
    public static void AddService(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ICustomerService, CustomerService>();
        serviceCollection.AddScoped<IProductService, ProductService>();
        serviceCollection.AddScoped<IOrderService, OrderService>();
        serviceCollection.AddScoped<IOrderItemService, OrderItemService>();
    }
}