using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RPGWebAPI.Dtos.Character;
using RPGWebAPI.Models;
using RPGWebAPI.Services.CharacterService;

namespace RPGWebAPI.Controllers
{
    // ApiController enables attribute routing and automatic HTTP 400 responses.
    // Route helps find a specific controller when we want to make a service call.
    // [Controller] in route is the name of the controller(Character), minus the controller part.
    // IActionResult allows us to send back HHTP status codes with data that client requested.
    // ActionResult<Character> sends character schema as well.
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {

        // This code above the HTTP request, injects the character service into the controller.
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        // In the return statement, need to call the corresponding _characterService method.
        [HttpGet("/GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> GetAllCharacters()
        {
            return Ok(await _characterService.GetAllCharacters());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetSingleCharacter(int id)
        {
            return Ok(await _characterService.GetCharacterById(id));
        }

        [HttpPost("/Add")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> AddCharacter(AddCharacterDto newCharacter)
        {
            return Ok(await _characterService.AddCharacter(newCharacter));
        }

        [HttpPut("/Update")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            var response = await _characterService.UpdateCharacter(updatedCharacter);
            if (response == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var response = await _characterService.DeleteCharacter(id);
            if (response == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
