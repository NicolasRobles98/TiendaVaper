using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Request;
using Application.Models;
using AutoMapper;
using Domain.Entities;

namespace Application.Profiles
{
    public class AutoMapperProfile : Profile

    {
        public AutoMapperProfile()
        {
            CreateMap<OwnerCreateRequest, Owner>();
            CreateMap<OwnerUpdateRequest, Owner>();
            CreateMap<Owner, OwnerDTO>();

            CreateMap<SysAdminUpdateRequest, SysAdmin>();
            CreateMap<SysAdminCreateRequest, SysAdmin>();
            CreateMap<SysAdmin, SysAdminDTO>();

            CreateMap<CustomerUpdateRequest, Customer>();
            CreateMap<CustomerCreateRequest, Customer>();
            CreateMap<Customer, CustomerDTO>();
        }
    }
}