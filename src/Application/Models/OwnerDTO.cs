using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Models
{
    public class OwnerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;

        public static OwnerDTO Create(Owner owner)
        {
            var dto = new OwnerDTO();
            dto.Id = owner.Id;
            dto.Name = owner.Name;
            dto.Surname = owner.Surname;
            dto.Email = owner.Email;
            dto.UserName = owner.UserName;
            return dto;

        }
    }
}
