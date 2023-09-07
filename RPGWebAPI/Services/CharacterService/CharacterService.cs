using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RPGWebAPI.Data;
using RPGWebAPI.Dtos.Character;
using RPGWebAPI.Models;

namespace RPGWebAPI.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        public static List<Character> characters = new List<Character>
        {
            new Character(),
            new Character{Id = 1, Name = "Sam"}
        };

        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CharacterService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        // Need to return service response object in each method with the appropriate data.
        // Character is replaced with GetCharacterDto since we are using DTOs now.
        // When using AutoMapper, to map a whole list in one line, we use the select method of linq.
        // Select returns an I enumurable but we want an actual list.
        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var character = _mapper.Map<Character>(newCharacter);
            //character.Id = characters.Max(c => c.Id) + 1;
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
            serviceResponse.Data = await _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
            return serviceResponse;
        }

        // First or Default will return null if no matching entity was found. First throws an exception directly.
        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var serviceRespone = new ServiceResponse<List<GetCharacterDto>>();
            try
            {
                var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
                if (character == null)
                {
                    throw new Exception($"Character with Id '{id}' not found.");
                }

                _context.Characters.Remove(character);
                await _context.SaveChangesAsync();

                serviceRespone.Data = await _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceRespone.Success = false;
                serviceRespone.Message = ex.Message;
            }

            return serviceRespone;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await _context.Characters.ToListAsync();
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        // Angle brackets for mapper decide Which type the value will be mapped to.
        // Parameter is the actual object that will be mapped.
        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
            return serviceResponse;
            //if (character is not null)
            //{
            //    return character;
            //}
            //else
            //{
            //    throw new Exception("Character not found.");
            //}
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            var serviceRespone = new ServiceResponse<GetCharacterDto>();
            try
            {
                var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);
                if (dbCharacter == null)
                {
                    throw new Exception($"Character with Id '{updatedCharacter.Id}' not found.");
                }

                // Using automapper to update character. Both ways map updatedCharacter to Character.
                // Have to create another map if using automapper method.
                //_mapper.Map<Character>(updatedCharacter);
                //_mapper.Map(updatedCharacter, character);

                dbCharacter.Name = updatedCharacter.Name;
                dbCharacter.HitPoints = updatedCharacter.HitPoints;
                dbCharacter.Strength = updatedCharacter.Strength;
                dbCharacter.Defense = updatedCharacter.Defense;
                dbCharacter.Class = updatedCharacter.Class;

                await _context.SaveChangesAsync();

                serviceRespone.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
            }
            catch (Exception ex)
            {
                serviceRespone.Success = false;
                serviceRespone.Message = ex.Message;
            }

            return serviceRespone;
        }
    }
}
