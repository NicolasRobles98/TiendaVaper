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
    public class OwnerController : Controller
    {
        private readonly IOwnerService _ownerService;
        public OwnerController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }

        [HttpGet]
        public ActionResult<List<OwnerDTO>> GetAll()
        {
            return _ownerService.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<OwnerDTO> GetById(int id)
        {
            try
            {
                return _ownerService.GetById(id);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<OwnerDTO> Create([FromBody] OwnerCreateRequest ownerCreateRequest)
        {
            try
            {
                var newOWner = _ownerService.Create(ownerCreateRequest);
                return CreatedAtAction(nameof(GetById), new { id = newOWner.Id }, newOWner);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] OwnerUpdateRequest ownerUpdateRequest)
        {
            try
            {
                _ownerService.Update(id, ownerUpdateRequest);
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
                _ownerService.Delete(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
