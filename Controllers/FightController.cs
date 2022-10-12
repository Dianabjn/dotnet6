using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet.DTO.Fight;
using dotnet.Services.FightService;
using Microsoft.AspNetCore.Mvc;

namespace dotnet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FightController :ControllerBase
    {
        public readonly IFightService _fightServicie;
        public FightController(IFightService fightServicie)
        {
            _fightServicie = fightServicie;
            
        }

        [HttpPost("Weapon")]
        public async Task<ActionResult<ServiceResponse<AttackResultDTO>>> WeaponAttack(WeaponAttackDTO request)
        {
            return Ok(await _fightServicie.WeaponAttack(request));
        }

        [HttpPost("Skill")]
        public async Task<ActionResult<ServiceResponse<AttackResultDTO>>> SkillAttack(SkillAttackDTO request)
        {
            return Ok(await _fightServicie.SkillAttack(request));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<FightResultDTO>>> Fight(FightRequestDTO request)
        {
            return Ok(await _fightServicie.Fight(request));
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<HighscoreDTO>>>> GetHighscore()
        {
            return Ok(await _fightServicie.GetHighscore());
        }
    }
}