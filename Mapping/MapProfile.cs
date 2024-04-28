using AutoMapper;
using vize.Dtos;
using vize.Models;
using vize.Dtos;
using vize.Models;

namespace vize.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Dosya, DosyaDto>().ReverseMap();
           
            CreateMap<AppUser, UserDto>().ReverseMap();

           CreateMap<Klasor, KlasorDto>().ReverseMap();

            CreateMap<UserGroup,UserGroupDto> ().ReverseMap();
        }
    }
}
