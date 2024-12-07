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
    public class SysAdminController : Controller
    {
        private readonly ISysAdminService _sysAdminService;
        public SysAdminController(ISysAdminService sysAdminService)
        {
            _sysAdminService = sysAdminService;
        }

        [HttpGet]
        public ActionResult<List<SysAdminDTO>> GetAll()
        {
            return _sysAdminService.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<SysAdminDTO> GetById(int id)
        {
            try
            {
                return _sysAdminService.GetById(id);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<SysAdminDTO> Create([FromBody] SysAdminCreateRequest sysAdminCreateRequest)
        {
            try
            {
                var newAdmin = _sysAdminService.Create(sysAdminCreateRequest);
                return CreatedAtAction(nameof(GetById), new { id = newAdmin.Id }, newAdmin);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] SysAdminUpdateRequest sysAdminUpdateRequest)
        {
            try
            {
                _sysAdminService.Update(id, sysAdminUpdateRequest);
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
                _sysAdminService.Delete(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
