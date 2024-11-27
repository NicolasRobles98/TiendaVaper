using Application.Models;
using Application.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IOwnerService
    {
        void Delete(int id);
        List<OwnerDTO> GetAll();
        OwnerDTO GetById(int id);

        void Update(int id, OwnerUpdateRequest OwnerUpdateRequest);

        OwnerDTO Create(OwnerCreateRequest OwnerCreateRequest);
    }
}
