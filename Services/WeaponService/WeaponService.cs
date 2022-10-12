using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using dotnet.Data;
using dotnet.DTO.Character;
using dotnet.DTO.Weapon;
using Microsoft.EntityFrameworkCore;

namespace dotnet.Services.WeaponService
{
    public class WeaponService : IWeaponService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccesor;
        private readonly IMapper _mapper;

        public WeaponService(DataContext context, IHttpContextAccessor httpContextAccesor, IMapper mapper)
        {
            _context = context;
            _httpContextAccesor = httpContextAccesor;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<GetCharacterDTO>> AddWeapon(AddWeaponDTO newWeapon)
        {
            ServiceResponse<GetCharacterDTO> response = new ServiceResponse<GetCharacterDTO>();

            try
            {
                Character character = await _context.Characters.FirstOrDefaultAsync(c => c.ID == newWeapon.CharacterId && c.User.ID == int.Parse(_httpContextAccesor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));
                if(character == null)
                {
                    response.Success = false;
                    response.Message = "Character not found.";
                    return response;
                }
                Weapon weapon = new Weapon{
                    Name = newWeapon.Name,
                    Damage = newWeapon.Damage,
                    Character = character
                };

                _context.Weapons.Add(weapon);
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetCharacterDTO>(character);
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}