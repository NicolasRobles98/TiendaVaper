﻿using Application.Models;
using Application.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProductService
    {
        void Delete(int id);
        List<ProductDTO> GetAll();
        ProductDTO GetById(int id);

        void Update(int id, ProductUpdateRequest productUpdateRequest);

        ProductDTO Create(ProductCreateRequest productCreateRequest);
    }
}
