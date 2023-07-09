using MaggicVilaAPI.Models.Dto;

namespace MaggicVilaAPI.Data
{
    public static class VillaStore
    {
        public static List<VillaDto> villaList = new List<VillaDto>
            {
                new VillaDto{Id = 1,Name="Pool View", Occupancy = 4, SqFt=100},
                new VillaDto{Id = 2,Name="Beach View", Occupancy = 3, SqFt=200},
            };
    }
}
