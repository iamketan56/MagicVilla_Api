using MaggicVilaAPI.Data;
using MaggicVilaAPI.Models;
using MaggicVilaAPI.Models.Dto;
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

        private readonly ApplicationDbContext _Db;
    
        public VillaApiController(ApplicationDbContext db)
        {
            _Db = db;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDto>> GetVillas()
        {
            //_logger.LogInformation("Getting All Villas");
            return Ok(_Db.Villas.ToList());
        }

        [HttpGet("id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDto> GetVillas(int Id)
        {
            if (Id == 0)
            {
              //  _logger.LogError("Get Villa Error with Id" + Id);
                return BadRequest();
            }

            var villa = _Db.Villas.FirstOrDefault(u => u.Id == Id);

            if (villa == null)
            {
                return NotFound();
            }
            return Ok(villa);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDto> CreateVilla([FromBody] VillaCreatedDto villadto)
        {
            //if(!ModelState.IsValid)
            //{
            //    return BadRequest();
            //}
            if (_Db.Villas.FirstOrDefault(u => u.Name.ToLower() == villadto.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Villa already Exist");
                return BadRequest(ModelState);
            }
            if (villadto == null)
            {
                return BadRequest(villadto);
            }
            //if (villadto.Id > 0)
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError);
            //}
            Villa model = new()
            {
                Amenity = villadto.Amenity,
                Details = villadto.Details,
                ImgUrl = villadto.ImgUrl,
                Name = villadto.Name,
                Occupency = villadto.Occupancy,
                rate = villadto.rate,
                Sqft = villadto.SqFt,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };
            _Db.Villas.Add(model);
            _Db.SaveChanges();

            return Ok(model);
        }

        [HttpDelete("id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villaToDelete = _Db.Villas.FirstOrDefault(u => u.Id == id);
            if (villaToDelete == null)
            {
                return NotFound();
            }
            _Db.Villas.Remove(villaToDelete);
            _Db.SaveChanges();
            return NoContent();
        }

        [HttpPut("id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateVilla(int id, [FromBody] VillaUpdateDto VillaToUpdate)
        {
            if (VillaToUpdate == null || id != VillaToUpdate.Id)
            {
                return BadRequest();
            }
            Villa model = new()
            {
                Amenity = VillaToUpdate.Amenity,
                Details = VillaToUpdate.Details,
                Id = VillaToUpdate.Id,
                ImgUrl = VillaToUpdate.ImgUrl,
                Name = VillaToUpdate.Name,
                Occupency = VillaToUpdate.Occupancy,
                rate = VillaToUpdate.rate,
                Sqft = VillaToUpdate.SqFt         
            };
            _Db.Villas.Update(model);
            _Db.SaveChanges();
            return NoContent();
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateVillaField(int id, JsonPatchDocument<VillaUpdateDto> PatchDto)
        {
            if (PatchDto == null || id == 0)
            {
                return BadRequest();
            }
            var villa = _Db.Villas.AsNoTracking().FirstOrDefault(u => u.Id == id);
            VillaUpdateDto villadto = new()
            {
                Amenity = villa.Amenity,
                Details = villa.Details,
                Id = villa.Id,
                ImgUrl = villa.ImgUrl,
                Name = villa.Name,
                Occupancy = villa.Occupency,
                rate = villa.rate,
                SqFt = villa.Sqft
            };
            if (villa == null)
            {
                return BadRequest();
            }
            PatchDto.ApplyTo(villadto, ModelState) ;
            Villa model = new()
            {
                Amenity = villadto.Amenity,
                Details = villadto.Details,
                Id = villadto.Id,
                ImgUrl = villadto.ImgUrl,
                Name = villadto.Name,
                Occupency = villadto.Occupancy,
                rate = villadto.rate,
                Sqft = villadto.SqFt
            };
            _Db.Villas.Update(model);
            _Db.SaveChanges();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}
