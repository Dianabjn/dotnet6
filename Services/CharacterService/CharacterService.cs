using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using dotnet.Data;
using dotnet.DTO.Character;
using Microsoft.EntityFrameworkCore;

namespace dotnet.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public readonly IHttpContextAccessor _httpContextAccessor;
        public CharacterService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        public async Task<ServiceResponse<List<GetCharacterDTO>>> AddCharacter(AddCharacterDTO newCharacter)
        {
            var serviceReponse = new ServiceResponse<List<GetCharacterDTO>>();
            Character character = _mapper.Map<Character>(newCharacter);
            character.User = await _context.Users.FirstOrDefaultAsync(u => u.ID == GetUserId());

            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
            serviceReponse.Data = await _context.Characters.Where(c => c.User.ID == GetUserId()).Select(c => _mapper.Map<GetCharacterDTO>(c)).ToListAsync();
            return serviceReponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDTO>>> DeleteCharacter(int id)
        {
            ServiceResponse<List<GetCharacterDTO>> response = new ServiceResponse<List<GetCharacterDTO>>();

            try
            {
                Character character = await _context.Characters.FirstOrDefaultAsync(c => c.ID == id && c.User.ID == GetUserId());
                if(character != null)
                {
                    _context.Characters.Remove(character);
                    await _context.SaveChangesAsync();
                    response.Data = _context.Characters.Where(c => c.User.ID == GetUserId()).Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
                } 
                else
                {
                    response.Success = false;
                    response.Message = "Character not found.";
                }
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<List<GetCharacterDTO>>> GetAllCharacters()
        {
            var response = new ServiceResponse<List<GetCharacterDTO>>();
            var dbCharacters = await _context.Characters.Where(c => c.User.ID == GetUserId()).ToListAsync();
            response.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
            return response;
        }

        public async Task<ServiceResponse<GetCharacterDTO>> GetCharacterByID(int id)
        {
            var serviceReponse = new ServiceResponse<GetCharacterDTO>();
            var dbCharacter = await _context.Characters
                .Include(c => c.Weapon)
                .Include(c => c.Skills)
                .FirstOrDefaultAsync(c => c.ID == id && c.User.ID == GetUserId());
            serviceReponse.Data = _mapper.Map<GetCharacterDTO>(dbCharacter);
            return serviceReponse;
        }

        public async Task<ServiceResponse<GetCharacterDTO>> UpdateCharacter(UpdateCharacterDTO updatedCharacter)
        {
            ServiceResponse<GetCharacterDTO> response = new ServiceResponse<GetCharacterDTO>();

            try
            {
                Character character = await _context.Characters.Include(c => c.User).FirstOrDefaultAsync(c => c.ID == updatedCharacter.ID);

                if(character.User.ID == GetUserId())
                {
                    //_mapper.Map(updatedCharacter, character);
                    character.Name = updatedCharacter.Name;
                    character.HitPoints = updatedCharacter.HitPoints;
                    character.Strength = updatedCharacter.Strength;
                    character.Defense = updatedCharacter.Defense;
                    character.Intelligence = updatedCharacter.Intelligence;
                    character.Class = updatedCharacter.Class;
                    
                    await _context.SaveChangesAsync();

                    response.Data = _mapper.Map<GetCharacterDTO>(character);
                }
                else 
                {
                    response.Success = false;
                    response.Message = "Character not found.";
                }

                
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        
        public async Task<ServiceResponse<GetCharacterDTO>> AddCharacterSkill(AddCharacterSkillDTO newCharacterSkill)
        {
            var response = new ServiceResponse<GetCharacterDTO>();
            try
            {
                var character = await _context.Characters
                .Include(c => c.Weapon)
                .Include(c => c.Skills)
                .FirstOrDefaultAsync(c => c.ID == newCharacterSkill.CharacterId &&
                 c.User.ID == GetUserId());
                if(character == null)
                {
                    response.Success = false;
                    response.Message = "Character not found.";
                    return response;
                }

                var skill = await _context.Skills.FirstOrDefaultAsync(s => s.ID == newCharacterSkill.SkillId);
                if(skill == null)
                {
                    response.Success = false;
                    response.Message = "Skill not found.";
                    return response;
                }

                character.Skills.Add(skill);
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