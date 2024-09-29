using Infrastructure.Model;

namespace Infrastructure.Interface;

public interface IOrderService
{
    Task<IEnumerable<Order>> GetAll();
    Task<Order?> GetById(Guid id);
    Task<bool> Create(Order order);
    Task<bool> Update(Order order);
    Task<bool> Delete(Guid id);
    Task<IEnumerable<OrdersWithCustomersAndProducts>> OrdersWithCustomersAndProducts();

    Task<IEnumerable<OrdersFilteredByStatusAndOrderDate>> OrdersFilteredByStatusAndOrderDate(string status,
        DateTime startDate, DateTime endDate);

    Task<OrderSpecificProduct?> OrderSpecificProduct(Guid id);
}