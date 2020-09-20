using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RulesEngine.Common.Model
{
    public class OrderInfo
    {
        [Required(ErrorMessage ="Customer first name not provided in input.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Customer last name not provided in input.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Customer address not provided in input.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Customer email not provided in input.")]
        public string Email { get; set; }
        public List<Product> ProductInfo { get; set; }
    }
}