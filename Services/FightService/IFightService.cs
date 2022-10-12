using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet.DTO.Fight;

namespace dotnet.Services.FightService
{
    public interface IFightService
    {
        Task<ServiceResponse<AttackResultDTO>> WeaponAttack(WeaponAttackDTO request);
        Task<ServiceResponse<AttackResultDTO>> SkillAttack(SkillAttackDTO request);
        Task<ServiceResponse<FightResultDTO>> Fight(FightRequestDTO request);
        Task<ServiceResponse<List<HighscoreDTO>>> GetHighscore();
    }
}