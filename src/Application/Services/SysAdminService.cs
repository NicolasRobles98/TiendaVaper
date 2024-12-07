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
    public class SysAdminService : ISysAdminService 
    {
        private readonly ISysAdminRepository _sysadminRepository;
        private readonly IMapper _mapper;
        public SysAdminService(ISysAdminRepository sysAdminRepository, IMapper mapper)
        {
            _sysadminRepository = sysAdminRepository;
            _mapper = mapper;
        }

        public SysAdminDTO Create(SysAdminCreateRequest sysAdminCreateRequest)
        {
            var sysAdmin = _mapper.Map<SysAdmin>(sysAdminCreateRequest);
            _sysadminRepository.Add(sysAdmin);
            return _mapper.Map<SysAdminDTO>(sysAdmin);

        }

        public List<SysAdminDTO> GetAll()
        {
            var admins = _sysadminRepository.GetAll();
            return _mapper.Map<List<SysAdminDTO>>(admins);
        }

        public SysAdminDTO GetById(int id)
        {
            var admin = _sysadminRepository.GetById(id) ?? throw new NotFoundException($"No se encontró el ID ingresado: {id}");
            return _mapper.Map<SysAdminDTO>(admin);
        }

        public void Delete(int id)
        {
            var adminId = _sysadminRepository.GetById(id) ?? throw new NotFoundException($"No se encontró el ID ingresado: {id}");
            _sysadminRepository.Delete(adminId);
        }

        public void Update(int id, SysAdminUpdateRequest sysAdminUpdateRequest)
        {
            var admin = _sysadminRepository.GetById(id) ?? throw new NotFoundException($"No se encontró el ID ingresado: {id}");
            _mapper.Map(sysAdminUpdateRequest, admin);
            _sysadminRepository.Update(admin);

        }
    }
}
