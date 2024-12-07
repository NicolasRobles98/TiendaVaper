using Application.Models;
using Application.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISysAdminService
    {
        void Delete(int id);
        List<SysAdminDTO> GetAll();
        SysAdminDTO GetById(int id);

        void Update(int id, SysAdminUpdateRequest sysAdminUpdateRequest);

        SysAdminDTO Create(SysAdminCreateRequest sysAdminCreateRequest);
    }
}
