using PayNet_Server.DTOs;
using System.Text.Json;

namespace PayNet_Server.Repository
{
    public class BankRepository : IBankRepository
    {
        private readonly HttpClient _httpClient;

        public BankRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<BankDto>> GetBanksAsync()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "bank.json");

            if (!File.Exists(filePath))
                return new List<BankDto>();

            var json = await File.ReadAllTextAsync(filePath);

            return JsonSerializer.Deserialize<List<BankDto>>(json);
        }
    }
}
