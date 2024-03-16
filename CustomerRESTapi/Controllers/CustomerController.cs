using CustomerRESTapi.DTO;
using CustomerRESTapi.HelperMethods;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace CustomerRESTapi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private static List<CustomerDTO> customersData = new List<CustomerDTO>();
        private readonly HttpClient client;

        public CustomerController(ILogger<CustomerController> logger)
        {
            _logger = logger;
            client = new HttpClient();
        }
        [HttpPost(Name = "CreateCustomer")]
        public ActionResult<ResponseModel<List<CustomerDTO>>> CreateCustomer(List<CustomerDTO> customerList)
        {
            try
            {
                foreach (var customer in customerList)
                {
                    // Validate fields
                    if (string.IsNullOrEmpty(customer.firstName) ||
                        string.IsNullOrEmpty(customer.lastName) ||
                        customer.age <= 18 ||
                        customersData.Any(c => c.customerId == customer.customerId))
                    {
                        return BadRequest("Invalid customer data.");
                    }
                    // Validate fields
                    if (customersData.Any(c => c.customerId == customer.customerId))
                    {
                        return BadRequest("Customer Id Already Exist");
                    }

                    // Find position to insert based on last name and first name
                    int index = 0;
                    while (index < customersData.Count &&
                           string.Compare(customersData[index].lastName, customer.lastName) < 0)
                    {
                        index++;
                    }
                    while (index < customersData.Count &&
                           string.Compare(customersData[index].firstName, customer.firstName) < 0 &&
                           string.Compare(customersData[index].lastName, customer.lastName) == 0)
                    {
                        index++;
                    }

                    // Insert customer at calculated position
                    customersData.Insert(index, customer);
                }

                // Save customers to file (persistence)

                return CustomerResponse(true,"Customers has been created", customersData);
            }
            catch (Exception)
            {

                throw;
            }  
        }
        [HttpGet(Name = "GetCustomerList")]
        public ActionResult<ResponseModel<List<CustomerDTO>>> CustomerList()
        {
            try
            {
                return CustomerResponse(true, "Customers has been created", customersData.ToList()); ;
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost(Name = "CreateRandomCustomer/{numberOfCustomers}")]
        public async Task<string> RandomCustomerCreation(int numberOfCustomers)
        {
            try
            {
                var request = HttpContext.Request;
                //var baseUrl = $"{request.Scheme}://{request.Host}:{request.PathBase.ToUriComponent()}"+ "/api/Customer/CreateCustomer";

                client.BaseAddress = new Uri("https://localhost:7005/api/Customer/CreateCustomer");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // Simulate POST requests
                List<CustomerDTO> newCustomers = CustomerGeneration.GenerateRandomCustomers(numberOfCustomers); // Number of customers to generate
                await SendPostRequest(client, newCustomers);
                return "Records are inserting..";
            }
            catch (Exception)
            {
                throw;
            }
        }
        #region|Helper Methods|
        [NonAction]
        public async Task SendPostRequest(HttpClient client, List<CustomerDTO> newCustomers)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:7005/api/Customer/CreateCustomer", newCustomers);
            response.EnsureSuccessStatusCode(); // Throw if not a success code
        }
        [NonAction]
        public async Task SendGetRequest(HttpClient client)
        {
            HttpResponseMessage response = await client.GetAsync("api/customers");
            response.EnsureSuccessStatusCode(); // Throw if not a success code
            List<CustomerDTO> customers = customersData;
            foreach (var customer in customers)
            {
                Console.WriteLine($"{customer.firstName} {customer.lastName}, Age: {customer.age}, ID: {customer.customerId}");
            }
        }
        [NonAction]
        public ResponseModel<List<CustomerDTO>> CustomerResponse(bool IsSuccess, string ReturnDescription, List<CustomerDTO>? customers)
        {
            return new ResponseModel<List<CustomerDTO>>()
            {
                IsSuccess = IsSuccess,
                Description = ReturnDescription,
                Result = customers
            };

        }
        #endregion
    }
}
