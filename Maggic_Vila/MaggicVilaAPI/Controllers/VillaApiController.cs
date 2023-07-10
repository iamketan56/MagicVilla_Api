using AutoMapper;
using MaggicVilaAPI.Data;
using MaggicVilaAPI.Models;
using MaggicVilaAPI.Models.Dto;
using MaggicVilaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MaggicVilaAPI.Controllers
{
    [Route("api/VillaApi")]
    [ApiController]
    public class VillaApiController : ControllerBase
    {
       // private readonly ILogger<VillaApiController> _logger;

        private readonly IVillaRepository _DbVilla;
        private readonly IMapper _mapper;
        public VillaApiController(IVillaRepository dbVilla,IMapper mapper)
        {
            _DbVilla = dbVilla;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VillaDto>>> GetVillas()
        {
            IEnumerable<Villa> villaList = await _DbVilla.GetAllAsync();
            //_logger.LogInformation("Getting All Villas");
            return Ok(_mapper.Map<List<VillaDto>>(villaList));
        }

        [HttpGet("id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VillaDto>> GetVillas(int Id)
        {
            if (Id == 0)
            {
              //  _logger.LogError("Get Villa Error with Id" + Id);
                return BadRequest();
            }

            var villa = await _DbVilla.GetAsync(u => u.Id == Id);

            if (villa == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<VillaDto>(villa));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VillaDto>> CreateVilla([FromBody] VillaCreatedDto CreateDto)
        {
           
            if (await _DbVilla.GetAsync(u => u.Name.ToLower() == CreateDto.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Villa already Exist");
                return BadRequest(ModelState);
            }
            if (CreateDto == null)
            {
                return BadRequest(CreateDto);
            }
       
            Villa model = _mapper.Map<Villa>(CreateDto);

            await _DbVilla.CreateAsync(model);
           await _DbVilla.SaveAsync();

            return Ok(model);
        }

        [HttpDelete("id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villaToDelete =await _DbVilla.GetAsync(u => u.Id == id);
            if (villaToDelete == null)
            {
                return NotFound();
            }
             _DbVilla.RemoveAsync(villaToDelete);
            await _DbVilla.SaveAsync();
            return NoContent();
        }

        [HttpPut("id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDto VillaToUpdate)
        {
            if (VillaToUpdate == null || id != VillaToUpdate.Id)
            {
                return BadRequest();
            }
            Villa model = _mapper.Map<Villa>(VillaToUpdate);
           
           await _DbVilla.UpdateAsync(model);
            await _DbVilla.SaveAsync();
            return NoContent();
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateVillaField(int id, JsonPatchDocument<VillaUpdateDto> PatchDto)
        {
            if (PatchDto == null || id == 0)
            {
                return BadRequest();
            }
            var villa = await _DbVilla.GetAsync(u => u.Id == id,false);
            VillaUpdateDto villadto = _mapper.Map<VillaUpdateDto>(villa);
            
            if (villa == null)
            {
                return BadRequest();
            }
            PatchDto.ApplyTo(villadto, ModelState) ;
            Villa model = _mapper.Map<Villa>(villadto);

            await _DbVilla.UpdateAsync(model);
            await _DbVilla.SaveAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}
