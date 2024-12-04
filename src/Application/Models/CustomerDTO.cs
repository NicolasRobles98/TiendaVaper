using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;

        public static CustomerDTO Create(Customer customer)
        {
            var dto = new CustomerDTO();
            dto.Id = customer.Id;
            dto.Name = customer.Name;
            dto.Surname = customer.Surname;
            dto.Email = customer.Email;
            dto.UserName = customer.UserName;
            return dto;

        }
    }
}
