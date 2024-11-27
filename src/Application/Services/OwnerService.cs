using Application.Models;
using Application.Models.Request;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class OwnerService
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
    }
}
