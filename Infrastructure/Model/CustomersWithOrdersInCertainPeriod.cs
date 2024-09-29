namespace Infrastructure.Model;

public class CustomersWithOrdersInCertainPeriod
{
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateTime OrderDate { get; set; }
}