using Infrastructure.Extension;
using Infrastructure.Interface;
using Infrastructure.Model;
using Infrastructure.Service;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddService();
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("api/Customer", async (ICustomerService customerService) =>
{
    IEnumerable<Customer> customers = await customerService.GetAll();
    return Results.Json(customers);
});
app.MapGet(@"/api/customers/orders/startDate={startDate}&endDate={endDate}", async (ICustomerService customerService,DateTime startDate, DateTime endDate) =>
{
    IEnumerable<CustomersWithOrdersInCertainPeriod> customers = await customerService.CustomersWithOrdersInCertainPeriod(startDate,endDate);
    return Results.Json(customers);
});
app.MapGet(@"/api/customers/statistics", async (ICustomerService customerService) =>
{
    IEnumerable<CountOrdersAndTotalAmountForCustomers> customers = await customerService.CountOrdersAndTotalAmountForCustomers();
    return Results.Json(customers);
});
app.MapGet(@"/api/customers/inactive", async (ICustomerService customerService) =>
{
    IEnumerable<InactiveCustomersInLastYear> customers = await customerService.InactiveCustomersInLastYear();
    return Results.Json(customers);
});
app.MapGet("api/Customer/{id}", async (ICustomerService customerService,Guid id) =>
{
    Customer? customer = await customerService.GetById(id);
    if (customer == null) return Results.NotFound(new { message = "no Customers" });
    return Results.Json(customer);
});
app.MapPost("api/Customer", async (ICustomerService customerService,Customer customer) =>
{
    bool? res = await customerService.Create(customer);
    if (res == false) return Results.NotFound(new { message = "not created" });
    return Results.Ok(new { message = "created" });
});
app.MapPut("api/Customer", async (ICustomerService customerService,Customer customer) =>
{
    bool? res = await customerService.Update(customer);
    if (res == false) return Results.NotFound(new { message = "not updated" });
    return Results.Ok(new { message = "updated" });
});
app.MapDelete("api/Customer/{id}", async (ICustomerService customerService,Guid id) =>
{
    bool? res = await customerService.Delete(id);
    if (res == false) return Results.NotFound(new { message = "not deleted" });
    return Results.Ok(new { message = "deleted" });
});
app.MapGet("api/Product", async (IProductService productService) =>
{
    IEnumerable<Product> products = await productService.GetAll();
    return Results.Json(products);
});
app.MapGet("api/products/out-of-stock", async (IProductService productService) =>
{
    IEnumerable<ProductsWithoutStockQuantity> products = await productService.ProductsWithoutStockQuantity();
    return Results.Json(products);
});
app.MapGet("/api/orders/sales-statistics/month={month}&year={year}", async (IProductService productService,int month,int year) =>
{
    IEnumerable<ProductsInSpecificMonthAndYear> products = await productService.ProductsInSpecificMonthAndYear(month,year);
    return Results.Json(products);  
});
app.MapGet("/api/products/popular", async (IProductService productService) =>
{
    MostOrderedProduct? product = await productService.MostOrderedProduct();
    return Results.Json(product);  
});
app.MapGet("/api/orders/products-summary", async (IProductService productService) =>
{
    IEnumerable<ProductsWithSumorders> products = await productService.ProductsWithSumorders();
    return Results.Json(products);  
});
app.MapGet("api/Product/{id}", async (IProductService productService,Guid id) =>
{
    Product? product = await productService.GetById(id);
    if (product == null) return Results.NotFound(new { message = "no Products" });
    return Results.Json(product);
});
app.MapPost("api/Product", async (IProductService productService,Product product) =>
{
    bool? res = await productService.Create(product);
    if (res == false) return Results.NotFound(new { message = "not created" });
    return Results.Ok(new { message = "created" });
});
app.MapPut("api/Product", async (IProductService productService,Product product) =>
{
    bool? res = await productService.Update(product);
    if (res == false) return Results.NotFound(new { message = "not updated" });
    return Results.Ok(new { message = "updated" });
});
app.MapDelete("api/Product/{id}", async (IProductService productService,Guid id) =>
{
    bool? res = await productService.Delete(id);
    if (res == false) return Results.NotFound(new { message = "not deleted" });
    return Results.Ok(new { message = "deleted" });
});
app.MapGet("api/Order", async (IOrderService orderService) =>
{
    IEnumerable<Order> orders = await orderService.GetAll();
    return Results.Json(orders);
});
app.MapGet("/api/orders/details", async (IOrderService orderService) =>
{
    IEnumerable<OrdersWithCustomersAndProducts> orders = await orderService.OrdersWithCustomersAndProducts();
    return Results.Json(orders);
});
app.MapGet("/api/orders/status={status}&startDate={startDate}&endDate={endDate}", async (IOrderService orderService,string status, DateTime startDate, DateTime endDate) =>
{
    IEnumerable<OrdersFilteredByStatusAndOrderDate> orders = await orderService.OrdersFilteredByStatusAndOrderDate(status,startDate,endDate);
    return Results.Json(orders);
});
app.MapGet("/api/products/{productId}/orders", async (IOrderService orderService,Guid productId) =>
{
    OrderSpecificProduct? order = await orderService.OrderSpecificProduct(productId);
    if (order == null) return Results.NotFound(new { message = "no Orders" });
    return Results.Json(order);
});
app.MapGet("api/Order/{id}", async (IOrderService orderService,Guid id) =>
{
    Order? order = await orderService.GetById(id);
    if (order == null) return Results.NotFound(new { message = "no Orders" });
    return Results.Json(order);
});
app.MapPost("api/Order", async (IOrderService orderService,Order order) =>
{
    bool? res = await orderService.Create(order);
    if (res == false) return Results.NotFound(new { message = "not created" });
    return Results.Ok(new { message = "created" });
});
app.MapPut("api/Order", async (IOrderService orderService,Order order) =>
{
    bool? res = await orderService.Update(order);
    if (res == false) return Results.NotFound(new { message = "not updated" });
    return Results.Ok(new { message = "updated" });
});
app.MapDelete("api/Order/{id}", async (IOrderService orderService,Guid id) =>
{
    bool? res = await orderService.Delete(id);
    if (res == false) return Results.NotFound(new { message = "not deleted" });
    return Results.Ok(new { message = "deleted" });
});
app.MapGet("api/OrderItem", async (IOrderItemService orderItemService) =>
{
    IEnumerable<OrderItem> products = await orderItemService.GetAll();
    return Results.Json(products);
});
app.MapGet("api/OrderItem/{id}", async (IOrderItemService orderItemService,Guid id) =>
{
    OrderItem? product = await orderItemService.GetById(id);
    if (product == null) return Results.NotFound(new { message = "no OrderItems" });
    return Results.Json(product);
});
app.MapPost("api/OrderItem", async (IOrderItemService orderItemService,OrderItem orderItem) =>
{
    bool? res = await orderItemService.Create(orderItem);
    if (res == false) return Results.NotFound(new { message = "not created" });
    return Results.Ok(new { message = "created" });
});
app.MapPut("api/OrderItem", async (IOrderItemService orderItemService,OrderItem orderItem) =>
{
    bool? res = await orderItemService.Update(orderItem);
    if (res == false) return Results.NotFound(new { message = "not updated" });
    return Results.Ok(new { message = "updated" });
});
app.MapDelete("api/OrderItem/{id}", async (IOrderItemService orderItemService,Guid id) =>
{
    bool? res = await orderItemService.Delete(id);
    if (res == false) return Results.NotFound(new { message = "not deleted" });
    return Results.Ok(new { message = "deleted" });
});

app.Run();
