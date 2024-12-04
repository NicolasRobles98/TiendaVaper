using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class SysAdminDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;

        public static SysAdminDTO Create(SysAdmin sysAdmin)
        {
            var dto = new SysAdminDTO();
            dto.Id = sysAdmin.Id;
            dto.Name = sysAdmin.Name;
            dto.Surname = sysAdmin.Surname;
            dto.Email = sysAdmin.Email;
            dto.UserName = sysAdmin.UserName;
            return dto;

        }
    }
}
