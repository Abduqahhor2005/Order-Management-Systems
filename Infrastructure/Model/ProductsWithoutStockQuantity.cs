namespace Infrastructure.Model;

public class ProductsWithoutStockQuantity
{
    public string Name { get; set; } = null!;
    public double Price { get; set; }
    public int StockQuantity { get; set; }
}