namespace Infrastructure.Model;

public class ProductsInSpecificMonthAndYear
{
    public Guid Id { get; set; }
    public double TotalAmount { get; set; }
    public DateTime OrderDate { get; set; }
    public string Status { get; set; } = null!;
}