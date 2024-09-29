using Dapper;
using Infrastructure.Interface;
using Infrastructure.Model;
using Npgsql;
namespace Infrastructure.Service;

public class OrderService : IOrderService
{
    public async Task<IEnumerable<Order>> GetAll()
    {
        try
        {
            using (NpgsqlConnection con = new(SqlCommands.ConnectionString))
            {
                con.Open();
                return await con.QueryAsync<Order>(SqlCommands.GetAll);
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<Order?> GetById(Guid id)
    {
        try
        {
            using (NpgsqlConnection con = new(SqlCommands.ConnectionString))
            {
                con.Open();
                return await con.QueryFirstOrDefaultAsync<Order>(SqlCommands.GetById,new{Id=id});
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<bool> Create(Order order)
    {
        try
        {
            using (NpgsqlConnection con = new(SqlCommands.ConnectionString))
            {
                con.Open();
                return await con.ExecuteAsync(SqlCommands.Create,order)>0;
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<bool> Update(Order order)
    {
        try
        {
            using (NpgsqlConnection con = new(SqlCommands.ConnectionString))
            {
                con.Open();
                return await con.ExecuteAsync(SqlCommands.Update,order)>0;
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<bool> Delete(Guid id)
    {
        try
        {
            using (NpgsqlConnection con = new(SqlCommands.ConnectionString))
            {
                con.Open();
                return await con.ExecuteAsync(SqlCommands.Delete,new{Id=id})>0;
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
    public async Task<IEnumerable<OrdersWithCustomersAndProducts>> OrdersWithCustomersAndProducts()
    {
        try
        {
            using (NpgsqlConnection con = new(SqlCommands.ConnectionString))
            {
                con.Open();
                return await con.QueryAsync<OrdersWithCustomersAndProducts>(SqlCommands.OrdersWithCustomersAndProducts);
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
    public async Task<IEnumerable<OrdersFilteredByStatusAndOrderDate>> OrdersFilteredByStatusAndOrderDate(string status, DateTime startDate, DateTime endDate)
    {
        try
        {
            using (NpgsqlConnection con = new(SqlCommands.ConnectionString))
            {
                con.Open();
                return await con.QueryAsync<OrdersFilteredByStatusAndOrderDate>(SqlCommands.OrdersFilteredByStatusAndOrderDate,new{Status=status,StartDate=startDate,EndDate=endDate});
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
    public async Task<OrderSpecificProduct?> OrderSpecificProduct(Guid id)
    {
        try
        {
            using (NpgsqlConnection con = new(SqlCommands.ConnectionString))
            {
                con.Open();
                return await con.QueryFirstOrDefaultAsync<OrderSpecificProduct>(SqlCommands.OrderSpecificProduct,new{Id=id});
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}


file class SqlCommands
{
    public const string ConnectionString = @"Host=localhost;Database=exam8;User Id=postgres;Port=4321;Password=salom;";
    public const string GetAll = @"select * from orders";
    public const string GetById = @"select * from orders where id=@id";
    public const string Create = @"insert into orders(Id,CustomerId,TotalAmount,OrderDate,Status,CreatedAt) values(@Id,@CustomerId,@TotalAmount,@OrderDate,@Status,@CreatedAt)";
    public const string Update = @"update orders set CustomerId = @CustomerId, TotalAmount = @TotalAmount, OrderDate = @OrderDate, Status = @Status CreatedAt = @CreatedAt where id=@id";
    public const string Delete = @"delete from orders where id=@id";
    public const string OrdersWithCustomersAndProducts = @"select o.Id, o.TotalAmount, c.FullName, p.Name from Orders o
join Customers c on c.id=o.CustomerId
join OrderItems oi on oi.OrderId = o.Id
join Products p on p.id = oi.ProductId
";

    public const string OrdersFilteredByStatusAndOrderDate = @"select Id, TotalAmount, OrderDate, Status from Orders 
where status = @status and OrderDate between @startDate and @endDate
";
    public const string OrderSpecificProduct = @"select o.Id, o.TotalAmount, o.Status, p.Name from Orders o
join OrderItems oi on oi.OrderId = o.Id
join Products p on p.id = oi.ProductId
where p.id = @id
";
}