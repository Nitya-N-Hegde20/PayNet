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
            parameters.Add("@PasswordHash", customer.Password);
            parameters.Add("@IsActive", customer.IsActive);

            var result = await _db.QuerySingleAsync<int>("RegisterCustomer",
                parameters, commandType: CommandType.StoredProcedure);

            return result; // -1 means duplicate email
        }

        public async Task<Customer?> GetCustomerByEmailAsync(string email)
        {
            var result = await _db.QueryFirstOrDefaultAsync<Customer>(
                "GetCustomerByEmail",
                new { Email = email },
                commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<Customer> UpdateCustomerAsync(Customer customer)
        {
            var parameters = new
            {
                customer.Id,
                customer.FullName,
                customer.Address,
                customer.Phone,
                customer.Email   
            };

            var result = await _db.QueryFirstOrDefaultAsync<Customer>(
                "UpdateCustomer",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result!;
        }

        public async Task<IEnumerable<Customer>> GetPayNetContactsAsync(int excludeCustomerId)
        {
            var sql = @"
        SELECT Id, FullName, Phone, Email
        FROM Customer
        WHERE IsActive = 1
          AND Id <> @CustomerId
          AND Phone IS NOT NULL
    ";

            return await _db.QueryAsync<Customer>(sql, new { CustomerId = excludeCustomerId });
        }

       


    }
}
