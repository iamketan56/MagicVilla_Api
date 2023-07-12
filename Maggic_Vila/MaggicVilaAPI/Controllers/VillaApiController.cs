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
        protected ApiResponse _response;
        private readonly IVillaRepository _DbVilla;
        private readonly IMapper _mapper;
        public VillaApiController(IVillaRepository dbVilla,IMapper mapper)
        {
            _DbVilla = dbVilla;
            _mapper = mapper;
            this._response = new();
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> GetVillas()
        {
            try
            {
                //_logger.LogInformation("Getting All Villas");
                IEnumerable<Villa> villaList = await _DbVilla.GetAllAsync();
                _response.Result = _mapper.Map<List<VillaDto>>(villaList);
                _response.StatusCode = System.Net.HttpStatusCode.OK;
                return Ok(_response);
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>(){ ex.ToString()};
            }
            return _response;
        }

        [HttpGet("id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> GetVillas(int Id)
        {
            try { 
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
            _response.Result = _mapper.Map<VillaDto>(villa);
            _response.StatusCode = System.Net.HttpStatusCode.OK;
            return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> CreateVilla([FromBody] VillaCreatedDto CreateDto)
        {
            try
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

                Villa villa = _mapper.Map<Villa>(CreateDto);

                await _DbVilla.CreateAsync(villa);
                await _DbVilla.SaveAsync();

                _response.Result = _mapper.Map<VillaDto>(villa);
                _response.StatusCode = System.Net.HttpStatusCode.Created;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return _response;

        }

        [HttpDelete("id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse>> DeleteVilla(int id)
        {
            try
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
            _response.StatusCode = System.Net.HttpStatusCode.NoContent;
            _response.IsSuccess = true;
            return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPut("id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse>> UpdateVilla(int id, [FromBody] VillaUpdateDto VillaToUpdate)
        {
            try
            {

            if (VillaToUpdate == null || id != VillaToUpdate.Id)
            {
                return BadRequest();
            }
            Villa model = _mapper.Map<Villa>(VillaToUpdate);
           
           await _DbVilla.UpdateAsync(model);
            await _DbVilla.SaveAsync();
            _response.StatusCode = System.Net.HttpStatusCode.NoContent;
            _response.IsSuccess = true;
            return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }
            return _response;
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
