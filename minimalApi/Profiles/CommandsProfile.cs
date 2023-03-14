using AutoMapper;
using minimalApi.Dtos;
using minimalApi.Models;

namespace minimalApi.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            //Source to target
            CreateMap<Command, CommandReadDto>();
            CreateMap<CommandCreateDto, Command>(); 
            CreateMap<CommandUpdateDto, Command>(); 
        }
    }
}
