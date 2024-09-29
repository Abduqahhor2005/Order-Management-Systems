using Dapper;
using Infrastructure.Interface;
using Infrastructure.Model;
using Npgsql;
namespace Infrastructure.Service;

public class ProductService : IProductService
{
    public async Task<IEnumerable<Product>> GetAll()
    {
        try
        {
            using (NpgsqlConnection con = new(SqlCommands.ConnectionString))
            {
                con.Open();
                return await con.QueryAsync<Product>(SqlCommands.GetAll);
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<Product?> GetById(Guid id)
    {
        try
        {
            using (NpgsqlConnection con = new(SqlCommands.ConnectionString))
            {
                con.Open();
                return await con.QueryFirstOrDefaultAsync<Product>(SqlCommands.GetById,new{Id=id});
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<bool> Create(Product product)
    {
        try
        {
            using (NpgsqlConnection con = new(SqlCommands.ConnectionString))
            {
                con.Open();
                return await con.ExecuteAsync(SqlCommands.Create,product)>0;
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<bool> Update(Product product)
    {
        try
        {
            using (NpgsqlConnection con = new(SqlCommands.ConnectionString))
            {
                con.Open();
                return await con.ExecuteAsync(SqlCommands.Update,product)>0;
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
    public async Task<IEnumerable<ProductsWithoutStockQuantity>> ProductsWithoutStockQuantity()
    {
        try
        {
            using (NpgsqlConnection con = new(SqlCommands.ConnectionString))
            {
                con.Open();
                return await con.QueryAsync<ProductsWithoutStockQuantity>(SqlCommands.ProductsWithoutStockQuantity);
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
    public async Task<MostOrderedProduct?> MostOrderedProduct()
    {
        try
        {
            using (NpgsqlConnection con = new(SqlCommands.ConnectionString))
            {
                con.Open();
                return await con.QueryFirstOrDefaultAsync<MostOrderedProduct>(SqlCommands.MostOrderedProduct);
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
    public async Task<IEnumerable<ProductsInSpecificMonthAndYear>> ProductsInSpecificMonthAndYear(int month,int year)
    {
        try
        {
            using (NpgsqlConnection con = new(SqlCommands.ConnectionString))
            {
                con.Open();
                return await con.QueryAsync<ProductsInSpecificMonthAndYear>(SqlCommands.ProductsInSpecificMonthAndYear,new{Month=month,Year=year});
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
    public async Task<IEnumerable<ProductsWithSumorders>> ProductsWithSumorders()
    {
        try
        {
            using (NpgsqlConnection con = new(SqlCommands.ConnectionString))
            {
                con.Open();
                return await con.QueryAsync<ProductsWithSumorders>(SqlCommands.ProductsWithSumorders);
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
    public const string GetAll = @"select * from products";
    public const string GetById = @"select * from products where id=@id";
    public const string Create = @"insert into products(Id,Name,Price,StockQuantity,CreatedAt) values(@Id,@Name,@Price,@StockQuantity,@CreatedAt)";
    public const string Update = @"update products set Name = @Name, Price = @Price, StockQuantity = @StockQuantity, CreatedAt = @CreatedAt where id=@id";
    public const string Delete = @"delete from products where id=@id";
    public const string ProductsWithoutStockQuantity = @"select Name,Price,StockQuantity from Products
where StockQuantity is null
";

    public const string MostOrderedProduct = @"select p.Name, p.Price, count(o.id) as countorder from Products p
join orderitems oi on oi.productid=p.id
join orders o on o.id = oi.orderid
group by p.Name, p.Price
order by count(o.id) desc
limit 1
";
    public const string ProductsInSpecificMonthAndYear = @"select Id, TotalAmount, OrderDate, Status from Orders o
where extract(month from OrderDate) = @month and extract(year from OrderDate) = @year
";

    public const string ProductsWithSumorders = @"select p.Name, p.Price, sum(o.TotalAmount) as sumorders from Products p
join OrderItems oi on oi.ProductId = p.Id
join Orders o on o.id = oi.OrderId
group by p.Name, p.Price
";
}