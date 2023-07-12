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
    [Route("api/VillaNumberApi")]
    [ApiController]
    public class VillaNumberApiController : ControllerBase
    {
        // private readonly ILogger<VillaApiController> _logger;
        protected ApiResponse _response;
        private readonly IVillaNumebrRepository _DbVillaNumber;

        private readonly IVillaRepository _DbVilla;
        private readonly IMapper _mapper;
        public VillaNumberApiController(IVillaNumebrRepository dbVillaNumber,IMapper mapper, IVillaRepository dbVilla)
        {
            _DbVillaNumber = dbVillaNumber;
            _mapper = mapper;
            this._response = new();
            _DbVilla = dbVilla;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> GetVillaNumbers()
        {
            try
            {
                //_logger.LogInformation("Getting All Villas");
                IEnumerable<VillaNumber> villaNumberList = await _DbVillaNumber.GetAllAsync();
                _response.Result = _mapper.Map<List<VillaNumberDto>>(villaNumberList);
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
        public async Task<ActionResult<ApiResponse>> GetVillaNumber(int Id)
        {
            try { 
            if (Id == 0)
            {
              //  _logger.LogError("Get Villa Error with Id" + Id);
                return BadRequest();
            }

            var villaNumber = await _DbVillaNumber.GetAsync(u => u.VillaNo == Id);

            if (villaNumber == null)
            {
                return NotFound();
            }
            _response.Result = _mapper.Map<VillaNumberDto>(villaNumber);
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
        public async Task<ActionResult<ApiResponse>> CreateVillaNumber([FromBody] VillaNumberCreatedDto CreateVillaDto)
        {
            try
            {
                if (await _DbVillaNumber.GetAsync(u => u.VillaNo == CreateVillaDto.VillaNo) != null)
                {
                    ModelState.AddModelError("CustomError", "Villa Number already Exist");
                    return BadRequest(ModelState);
                }
                if (CreateVillaDto == null)
                {
                    return BadRequest(CreateVillaDto);
                }
                if(await _DbVilla.GetAsync(u=>u.Id==CreateVillaDto.VillaId)==null)
                {

                    ModelState.AddModelError("CustomError", "Villa Id is Invalid");
                    return BadRequest(ModelState);
                }

                VillaNumber villa = _mapper.Map<VillaNumber>(CreateVillaDto);

                await _DbVillaNumber.CreateAsync(villa);
                await _DbVillaNumber.SaveAsync();

                _response.Result = _mapper.Map<VillaNumberDto>(villa);
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
        public async Task<ActionResult<ApiResponse>> DeleteVillaNumber(int id)
        {
            try
            { 
            if (id == 0)
            {
                return BadRequest();
            }
            var villaToDelete =await _DbVillaNumber.GetAsync(u => u.VillaNo == id);
            if (villaToDelete == null)
            {
                return NotFound();
            }
             _DbVillaNumber.RemoveAsync(villaToDelete);
             await _DbVillaNumber.SaveAsync();
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
        public async Task<ActionResult<ApiResponse>> UpdateVillaNumber(int id, [FromBody] VillaNumberUpdateDto VillaToUpdate)
        {
            try
            {

            if (VillaToUpdate == null || id != VillaToUpdate.VillaNo)
            {
                return BadRequest();
            }
                if (await _DbVilla.GetAsync(u => u.Id == VillaToUpdate.VillaId) == null)
                {

                    ModelState.AddModelError("CustomError", "Villa Id is Invalid");
                    return BadRequest(ModelState);
                }
                VillaNumber model = _mapper.Map<VillaNumber>(VillaToUpdate);
           
           await _DbVillaNumber.UpdateAsync(model);
            await _DbVillaNumber.SaveAsync();
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

    }
}
