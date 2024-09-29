using Infrastructure.Model;

namespace Infrastructure.Interface;

public interface IOrderItemService
{
    Task<IEnumerable<OrderItem>> GetAll();
    Task<OrderItem?> GetById(Guid id);
    Task<bool> Create(OrderItem orderItem);
    Task<bool> Update(OrderItem orderItem);
    Task<bool> Delete(Guid id);
}