using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CustomerRESTapi.DTO
{
    public class CustomerDTO
    {
        public int customerId { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public int ? age { get; set; }
    }
}
