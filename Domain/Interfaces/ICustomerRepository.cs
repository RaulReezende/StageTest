using StageTest.Domain.Entities;

namespace StageTest.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetByIdAsync(Guid id);
        Task AddAsync(Customer customer);
    }
}
