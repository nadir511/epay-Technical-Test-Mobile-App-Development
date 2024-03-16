using CustomerRESTapi.DTO;

namespace CustomerRESTapi.HelperMethods
{
    public static class CustomerGeneration
    {
        
        public static List<CustomerDTO> GenerateRandomCustomers(int count)
        {
            List<CustomerDTO> newCustomers = new List<CustomerDTO>();
            Random random = new Random();

            // Generate random customers
            for (int i = 0; i < count; i++)
            {
                CustomerDTO customer = new CustomerDTO
                {
                    firstName = GetRandomFirstName(),
                    lastName = GetRandomLastName(),
                    age = random.Next(18, 91), // Random age between 18 and 90
                    customerId = i + 1 // Sequential ID starting from 1
                };
                newCustomers.Add(customer);
            }

            return newCustomers;
        }

        public static string GetRandomFirstName()
        {
            // List of first names
            string[] firstNames = { "Aaaa", "Bbbb", "Cccc", "Dddd", "Eeee" };
            Random random = new Random();
            return firstNames[random.Next(firstNames.Length)];
        }

        public static string GetRandomLastName()
        {
            // List of last names
            string[] lastNames = { "Xxxx", "Yyyy", "Zzzz" };
            Random random = new Random();
            return lastNames[random.Next(lastNames.Length)];
        }
    }
}
