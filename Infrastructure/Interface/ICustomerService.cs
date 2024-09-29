using Infrastructure.Model;

namespace Infrastructure.Interface;

public interface ICustomerService
{
    Task<IEnumerable<Customer>> GetAll();
    Task<Customer?> GetById(Guid id);
    Task<bool> Create(Customer customer);
    Task<bool> Update(Customer customer);
    Task<bool> Delete(Guid id);

    Task<IEnumerable<CustomersWithOrdersInCertainPeriod>> CustomersWithOrdersInCertainPeriod(DateTime start,
        DateTime end);

    Task<IEnumerable<CountOrdersAndTotalAmountForCustomers>> CountOrdersAndTotalAmountForCustomers();
    Task<IEnumerable<InactiveCustomersInLastYear>> InactiveCustomersInLastYear();
}