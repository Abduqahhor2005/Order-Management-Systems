namespace Infrastructure.Model;

public class CountOrdersAndTotalAmountForCustomers
{
    public string FullName { get; set; } = null!;
    public int Countorder { get; set; }
    public double Sumorder { get; set; }
}