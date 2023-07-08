using MaggicVilaAPI.Data;
using MaggicVilaAPI.Models;
using MaggicVilaAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace MaggicVilaAPI.Controllers
{
    [Route("api/VillaApi")]
    [ApiController]
    public class VillaApiController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<VillaDto> GetVillas()
        {
            return VillaStore.villaList;
        }

        [HttpGet("id")]
        public VillaDto GetVillas(int Id)
        {
            return VillaStore.villaList.FirstOrDefault(u=>u.Id == Id);
        }

    }
}
