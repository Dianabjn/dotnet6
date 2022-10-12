using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet.DTO.Skill;
using dotnet.DTO.Weapon;

namespace dotnet.DTO.Character
{
    public class GetCharacterDTO
    {
        public int ID {get; set;}
        public string Name {get; set;} = "Frodo";
        public int HitPoints {get; set;} = 100;
        public int Strength {get; set;} = 10;
        public int Defense {get; set;} = 10;
        public int Intelligence {get; set;} = 10;
        public RpgClass Class {get; set;} = RpgClass.Knight;
        public GetWeaponDTO Weapon { get; set; }
        public List<GetSkillDTO> Skills { get; set; }
        public int Fights { get; set; }
        public int Victories { get; set; }
        public int Defeats { get; set; }
    }
}