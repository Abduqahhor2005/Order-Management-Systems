using Dapper;
using Infrastructure.Interface;
using Infrastructure.Model;
using Npgsql;
namespace Infrastructure.Service;

public class OrderItemService : IOrderItemService
{
    public async Task<IEnumerable<OrderItem>> GetAll()
    {
        try
        {
            using (NpgsqlConnection con = new(SqlCommands.ConnectionString))
            {
                con.Open();
                return await con.QueryAsync<OrderItem>(SqlCommands.GetAll);
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<OrderItem?> GetById(Guid id)
    {
        try
        {
            using (NpgsqlConnection con = new(SqlCommands.ConnectionString))
            {
                con.Open();
                return await con.QueryFirstOrDefaultAsync<OrderItem>(SqlCommands.GetById,new{Id=id});
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<bool> Create(OrderItem orderItem)
    {
        try
        {
            using (NpgsqlConnection con = new(SqlCommands.ConnectionString))
            {
                con.Open();
                return await con.ExecuteAsync(SqlCommands.Create,orderItem)>0;
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<bool> Update(OrderItem orderItem)
    {
        try
        {
            using (NpgsqlConnection con = new(SqlCommands.ConnectionString))
            {
                con.Open();
                return await con.ExecuteAsync(SqlCommands.Update,orderItem)>0;
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
}


file class SqlCommands
{
    public const string ConnectionString = @"Host=localhost;Database=exam8;User Id=postgres;Port=4321;Password=salom;";
    public const string GetAll = @"select * from orderitems";
    public const string GetById = @"select * from orderitems where id=@id";
    public const string Create = @"insert into orderitems(Id,OrderId,ProductId,Quantity,Price,CreatedAt) values(@Id,@OrderId,@ProductId,@Quantity,@Price,@CreatedAt)";
    public const string Update = @"update orderitems set OrderId = @OrderId, ProductId = @ProductId, Quantity = @Quantity, Price = @Price CreatedAt = @CreatedAt where id=@id";
    public const string Delete = @"delete from orderitems where id=@id";
}