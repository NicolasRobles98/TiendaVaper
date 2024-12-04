using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CustomerService : ICustomerService 
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public CustomerDTO Create(CustomerCreateRequest customerCreateRequest)
        {
            var customer = _mapper.Map<Customer>(customerCreateRequest);
            _customerRepository.Add(customer);
            return _mapper.Map<CustomerDTO>(customer);

        }

        public List<CustomerDTO> GetAll()
        {
            var customers = _customerRepository.GetAll();
            return _mapper.Map<List<CustomerDTO>>(customers);
        }

        public CustomerDTO GetById(int id)
        {
            var customer = _customerRepository.GetById(id) ?? throw new NotFoundException($"No se encontró el ID ingresado: {id}");
            return _mapper.Map<CustomerDTO>(customer);
        }

        public void Delete(int id)
        {
            var customerId = _customerRepository.GetById(id) ?? throw new NotFoundException($"No se encontró el ID ingresado: {id}");
            _customerRepository.Delete(customerId);
        }

        public void Update(int id, CustomerUpdateRequest customerUpdateRequest)
        {
            var owner = _customerRepository.GetById(id) ?? throw new NotFoundException($"No se encontró el ID ingresado: {id}");
            _mapper.Map(customerUpdateRequest, owner);
            _customerRepository.Update(owner);

        }
    }
}
