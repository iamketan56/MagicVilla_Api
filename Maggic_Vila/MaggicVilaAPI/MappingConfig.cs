using AutoMapper;
using MaggicVilaAPI.Models;
using MaggicVilaAPI.Models.Dto;

namespace MaggicVilaAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        { 
                CreateMap<Villa,VillaDto>();
                CreateMap<VillaDto, Villa>();

                CreateMap<Villa, VillaCreatedDto>().ReverseMap();
                CreateMap<Villa, VillaUpdateDto>().ReverseMap();
               
                CreateMap<VillaDto,VillaCreatedDto>().ReverseMap();
                CreateMap<VillaDto, VillaUpdateDto>().ReverseMap();
        }
    }
}
