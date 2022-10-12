using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet.DTO.Character;
using dotnet.DTO.Fight;
using dotnet.DTO.Skill;
using dotnet.DTO.Weapon;

namespace dotnet
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDTO>();
            CreateMap<AddCharacterDTO, Character>();
            //CreateMap<UpdateCharacterDTO, Character>();
            CreateMap<Weapon, GetWeaponDTO>();
            CreateMap<Skill, GetSkillDTO>();
            CreateMap<Character, HighscoreDTO>();
        }
    }
}