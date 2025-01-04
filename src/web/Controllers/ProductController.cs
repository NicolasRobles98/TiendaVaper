using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace web.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public ActionResult<List<ProductDTO>> GetAll()
        {
            return _productService.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<ProductDTO> GetById(int id)
        {
            try
            {
                return _productService.GetById(id);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<ProductDTO> Create([FromBody] ProductCreateRequest productCreateRequest)
        {
            try
            {
                var newProduct = _productService.Create(productCreateRequest);
                return CreatedAtAction(nameof(GetById), new { id = newProduct.Id }, newProduct);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] ProductUpdateRequest productUpdateRequest)
        {
            try
            {
                _productService.Update(id, productUpdateRequest);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        //[HttpPut()]
        //public IActionResult UpdatePersonal([FromBody] OwnerUpdateRequest ownerUpdateRequest)
        //{
        //    try
        //    {
        //        var duenoIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //        if (duenoIdClaim == null)
        //        {
        //            return Unauthorized("No se pudo encontrar el Id del Dueno.");
        //        }
        //        var DuenoId = int.Parse(duenoIdClaim);

        //        _ownerService.Update(DuenoId, ownerUpdateRequest);
        //        return NoContent();
        //    }
        //    catch (NotFoundException ex)
        //    {
        //        return NotFound(ex.Message);
        //    }
        //}

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                _productService.Delete(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
