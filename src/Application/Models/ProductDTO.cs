using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Amount { get; set; }
        public float Price { get; set; }

        public static ProductDTO Create(Product product)
        {
            var dto = new ProductDTO();
            dto.Id = product.Id;
            dto.Name = product.Name;
            dto.Description = product.Description;
            dto.Amount = product.Amount;
            dto.Price = product.Price;
            return dto;

        }
    }
}
