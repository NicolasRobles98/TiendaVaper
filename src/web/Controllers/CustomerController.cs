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
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        public CustomerController(IOwnerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public ActionResult<List<CustomerDTO>> GetAll()
        {
            return _customerService.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<CustomerDTO> GetById(int id)
        {
            try
            {
                return _customerService.GetById(id);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<CustomerDTO> Create([FromBody] CustomerCreateRequest customerCreateRequest)
        {
            try
            {
                var newCustomer = _customerService.Create(customerCreateRequest);
                return CreatedAtAction(nameof(GetById), new { id = newCustomer.Id }, newCustomer);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] CustomerUpdateRequest customerUpdateRequest)
        {
            try
            {
                _customerService.Update(id, customerUpdateRequest);
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
                _customerService.Delete(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
