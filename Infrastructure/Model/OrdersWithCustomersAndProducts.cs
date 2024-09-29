namespace Infrastructure.Model;

public class OrdersWithCustomersAndProducts
{
    public Guid Id { get; set; }
    public double TotalAmount { get; set; }
    public string FullName { get; set; } = null!;
    public string Name { get; set; } = null!;
}