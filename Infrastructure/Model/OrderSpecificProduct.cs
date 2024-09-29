namespace Infrastructure.Model;

public class OrderSpecificProduct
{
    public Guid Id { get; set; }
    public double TotalAmount { get; set; }
    public string Status { get; set; } = null!;
    public string Name { get; set; } = null!;
}