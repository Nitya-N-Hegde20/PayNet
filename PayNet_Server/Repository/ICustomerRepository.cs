using PayNetServer.Models;

namespace PayNet_Server.Repository
{
    public interface ICustomerRepository
    {
        Task<int> RegisterCustomerAsync(Customer customer);
        Task<Customer?> GetCustomerByEmailAsync(string email);
        Task<Customer> UpdateCustomerAsync(Customer customer);
        Task<IEnumerable<Customer>> GetPayNetContactsAsync(int excludeCustomerId);
        

    }
}
