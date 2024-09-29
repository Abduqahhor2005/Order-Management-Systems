using Dapper;
using Infrastructure.Interface;
using Infrastructure.Model;
using Npgsql;

namespace Infrastructure.Service;

public class CustomerService : ICustomerService
{
    public async Task<IEnumerable<Customer>> GetAll()
    {
        try
        {
            using (NpgsqlConnection con = new(SqlCommands.ConnectionString))
            {
                con.Open();
                return await con.QueryAsync<Customer>(SqlCommands.GetAll);
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<Customer?> GetById(Guid id)
    {
        try
        {
            using (NpgsqlConnection con = new(SqlCommands.ConnectionString))
            {
                con.Open();
                return await con.QueryFirstOrDefaultAsync<Customer>(SqlCommands.GetById,new{Id=id});
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<bool> Create(Customer customer)
    {
        try
        {
            using (NpgsqlConnection con = new(SqlCommands.ConnectionString))
            {
                con.Open();
                return await con.ExecuteAsync(SqlCommands.Create,customer)>0;
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<bool> Update(Customer customer)
    {
        try
        {
            using (NpgsqlConnection con = new(SqlCommands.ConnectionString))
            {
                con.Open();
                return await con.ExecuteAsync(SqlCommands.Update,customer)>0;
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
    public async Task<IEnumerable<CustomersWithOrdersInCertainPeriod>> CustomersWithOrdersInCertainPeriod(DateTime start, DateTime end)
    {
        try
        {
            using (NpgsqlConnection con = new(SqlCommands.ConnectionString))
            {
                con.Open();
                return await con.QueryAsync<CustomersWithOrdersInCertainPeriod>(SqlCommands.CustomersWithOrdersInCertainPeriod,new{StartDate=start,EndDate=end});
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
    public async Task<IEnumerable<CountOrdersAndTotalAmountForCustomers>> CountOrdersAndTotalAmountForCustomers()
    {
        try
        {
            using (NpgsqlConnection con = new(SqlCommands.ConnectionString))
            {
                con.Open();
                return await con.QueryAsync<CountOrdersAndTotalAmountForCustomers>(SqlCommands.CountOrdersAndTotalAmountForCustomers);
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
    public async Task<IEnumerable<InactiveCustomersInLastYear>> InactiveCustomersInLastYear()
    {
        try
        {
            using (NpgsqlConnection con = new(SqlCommands.ConnectionString))
            {
                con.Open();
                return await con.QueryAsync<InactiveCustomersInLastYear>(SqlCommands.InactiveCustomersInLastYear);
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
    public const string GetAll = @"select * from customers";
    public const string GetById = @"select * from customers where id=@id";
    public const string Create = @"insert into customers(Id,FullName,Email,Phone,CreatedAt) values(@Id,@FullName,@Email,@Phone,@CreatedAt)";
    public const string Update = @"update customers set FullName = @FullName, Email = @Email, Phone = @Phone, CreatedAt = @CreatedAt where id=@id";
    public const string Delete = @"delete from customers where id=@id";
    public const string CustomersWithOrdersInCertainPeriod = @"select c.FullName, c.Email, o.OrderDate from Customers c
join Orders o on c.id=o.CustomerId
where o.OrderDate between @startDate and @endDate
";

    public const string CountOrdersAndTotalAmountForCustomers = @"select c.FullName, count(o.Id) as countorder, sum(o.TotalAmount) as sumorder from Customers c
join Orders o on c.id=o.CustomerId
group by c.FullName
";

    public const string InactiveCustomersInLastYear = @"select c.FullName, o.OrderDate from customers c
join orders o on o.customerid=c.id
where extract(year from current_date) - extract(year from o.OrderDate) < 1 
group by c.FullName, o.OrderDate
having count(o.id) is null
";
}