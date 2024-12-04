using Application.Models;
using Application.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICustomerService
    {
        void Delete(int id);
        List<CustomerDTO> GetAll();
        CustomerDTO GetById(int id);

        void Update(int id, CustomerUpdateRequest customerUpdateRequest);

        CustomerDTO Create(CustomerCreateRequest customerCreateRequest);
    }
}
