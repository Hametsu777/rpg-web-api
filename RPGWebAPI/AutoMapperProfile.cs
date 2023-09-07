using AutoMapper;
using RPGWebAPI.Dtos.Character;
using RPGWebAPI.Models;

namespace RPGWebAPI
{
    // Have to create maps for mapping. AutoMapper does not know how to map character into GetCharacterDTO
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDto>();
            CreateMap<AddCharacterDto, Character>();
            //CreateMap<UpdateCharacterDto, Character>();
        }
    }
}
