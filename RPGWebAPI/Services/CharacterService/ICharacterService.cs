using RPGWebAPI.Dtos.Character;
using RPGWebAPI.Models;

namespace RPGWebAPI.Services.CharacterService
{
    public interface ICharacterService
    {
        // Code before using DTO.
        //Task<ServiceResponse<List<Character>>> GetAllCharacters();
        Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters();
        Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id);
        Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter);
        Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacter);
        Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id);
    }
}
