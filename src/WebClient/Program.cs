using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace WebClient
{
    static class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            ClientSetup();
            long customerId = GetCustomerId();
            await GetExistingCustomerAsync(customerId);

            CustomerCreateRequest newCustomer = GenerateRandomCustomer();
            long newCustomerId = await CreateNewCustomerAsync(newCustomer);
            await GetExistingCustomerAsync(newCustomerId);

            Console.ReadLine();
        }

        private static void ClientSetup()
        {
            client.BaseAddress = new Uri("http://localhost");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private static long GetCustomerId()
        {
            Console.WriteLine("Введите ID клиента:");
            if (!long.TryParse(Console.ReadLine(), out long customerId))
            {
                throw new ArgumentException("Некорректный ID клиента");
            }
            return customerId;
        }

        private static async Task GetExistingCustomerAsync(long customerId)
        {
            string endpoint = $"{customerId}";
            HttpResponseMessage response = await client.GetAsync(endpoint);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                Customer customer = JsonConvert.DeserializeObject<Customer>(responseBody);
                Console.WriteLine($"Найден клиент ID: {customer.Id}, Имя {customer.Firstname}, Фамилия: {customer.Lastname}");
            }
            else
            {
                Console.WriteLine($"Ошибка: {response.StatusCode}");
            }
        }

        private static CustomerCreateRequest GenerateRandomCustomer()
        {
            string[] firstNames = { "Иван", "Петр", "Алексей", "Андрей", "Михаил", "Сергей" };
            string[] lastNames = { "Иванов", "Петров", "Сидоров", "Моргунов", "Вицин", "Никулин" };

            Random random = new Random();

            string randomFirstName = firstNames[random.Next(firstNames.Length)];
            string randomLastName = lastNames[random.Next(lastNames.Length)];

            return new CustomerCreateRequest(randomFirstName, randomLastName);
        }


        private static async Task<long> CreateNewCustomerAsync(CustomerCreateRequest customer)
        {
            string endpoint = "create";
            HttpResponseMessage response = await client.PostAsJsonAsync(endpoint, customer);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                var createdCustomerId = JsonConvert.DeserializeObject<long>(responseBody);
                Console.WriteLine("Клиент успешно создан на сервере.");
                return createdCustomerId;
            }
            else
            {
                Console.WriteLine($"Ошибка: {response.StatusCode}");
                return 0;
            }
        }

    }
}