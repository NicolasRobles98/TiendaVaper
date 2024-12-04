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
    public class OwnerService : IOwnerService 
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;
        public OwnerService(IOwnerRepository ownerRepository, IMapper mapper)
        {
            _ownerRepository = ownerRepository;
            _mapper = mapper;
        }

        public OwnerDTO Create(OwnerCreateRequest ownerCreateRequest)
        {
            var Owner = _mapper.Map<Owner>(ownerCreateRequest);
            _ownerRepository.Add(Owner);
            return _mapper.Map<OwnerDTO>(Owner);

        }

        public List<OwnerDTO> GetAll()
        {
            var owners = _ownerRepository.GetAll();
            return _mapper.Map<List<OwnerDTO>>(owners);
        }

        public OwnerDTO GetById(int id)
        {
            var owner = _ownerRepository.GetById(id) ?? throw new NotFoundException($"No se encontró el ID ingresado: {id}");
            return _mapper.Map<OwnerDTO>(owner);
        }

        public void Delete(int id)
        {
            var ownerId = _ownerRepository.GetById(id) ?? throw new NotFoundException($"No se encontró el ID ingresado: {id}");
            _ownerRepository.Delete(ownerId);
        }

        public void Update(int id, OwnerUpdateRequest OwnerUpdateRequest)
        {
            var owner = _ownerRepository.GetById(id) ?? throw new NotFoundException($"No se encontró el ID ingresado: {id}");
            _mapper.Map(OwnerUpdateRequest, owner);
            _ownerRepository.Update(owner);

        }
    }
}
