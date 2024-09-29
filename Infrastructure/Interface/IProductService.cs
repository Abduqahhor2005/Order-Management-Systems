using Infrastructure.Model;

namespace Infrastructure.Interface;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAll();
    Task<Product?> GetById(Guid id);
    Task<bool> Create(Product product);
    Task<bool> Update(Product product);
    Task<bool> Delete(Guid id);
    Task<IEnumerable<ProductsWithoutStockQuantity>> ProductsWithoutStockQuantity();
    Task<MostOrderedProduct?> MostOrderedProduct();
    Task<IEnumerable<ProductsInSpecificMonthAndYear>> ProductsInSpecificMonthAndYear(int month, int year);
    Task<IEnumerable<ProductsWithSumorders>> ProductsWithSumorders();
}