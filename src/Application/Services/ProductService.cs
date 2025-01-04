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
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public ProductDTO Create(ProductCreateRequest productCreateRequest)
        {
            var product = _mapper.Map<Product>(productCreateRequest);
            _productRepository.Add(product);
            return _mapper.Map<ProductDTO>(product);

        }

        public List<ProductDTO> GetAll()
        {
            var products = _productRepository.GetAll();
            return _mapper.Map<List<ProductDTO>>(products);
        }

        public ProductDTO GetById(int id)
        {
            var product = _productRepository.GetById(id) ?? throw new NotFoundException($"No se encontró el ID ingresado: {id}");
            return _mapper.Map<ProductDTO>(product);
        }

        public void Delete(int id)
        {
            var productId = _productRepository.GetById(id) ?? throw new NotFoundException($"No se encontró el ID ingresado: {id}");
            _productRepository.Delete(productId);
        }

        public void Update(int id, ProductUpdateRequest productUpdateRequest)
        {
            var product = _productRepository.GetById(id) ?? throw new NotFoundException($"No se encontró el ID ingresado: {id}");
            _mapper.Map(productUpdateRequest, product);
            _productRepository.Update(product);

        }
    }
}
