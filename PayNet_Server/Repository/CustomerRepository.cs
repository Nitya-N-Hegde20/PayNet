using Dapper;
using PayNetServer.Models;
using System.Data;

namespace PayNet_Server.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDbConnection _db;

        public CustomerRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<int> RegisterCustomerAsync(Customer customer)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FullName", customer.FullName);
            parameters.Add("@Address", customer.Address);
            parameters.Add("@Email", customer.Email);
            parameters.Add("@Phone", customer.Phone);
            parameters.Add("@PasswordHash", customer.PasswordHash);
            parameters.Add("@IsActive", customer.IsActive);

            var result = await _db.QuerySingleAsync<int>("RegisterCustomer",
                parameters, commandType: CommandType.StoredProcedure);

            return result; // -1 means duplicate email
        }

        public async Task<Customer?> GetCustomerByEmailAsync(string email)
        {
            var query = "SELECT * FROM Customer WHERE Email = @Email";
            return await _db.QueryFirstOrDefaultAsync<Customer>(query, new { Email = email });
        }
    }
}
