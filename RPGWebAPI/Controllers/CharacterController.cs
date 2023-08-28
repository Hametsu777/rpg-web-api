using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RPGWebAPI.Models;

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
        public static List<Character> characters = new List<Character>
        {
            new Character(),
            new Character{Id = 1, Name = "Sam"}
        };

        [HttpGet("/GetAll")]
        public ActionResult<List<Character>> GetAllCharacters()
        {
            return Ok(characters);
        }

        [HttpGet("/GetSingle")]
        public ActionResult<Character> GetSingleCharacter(int id)
        {
            return Ok(characters.FirstOrDefault(c => c.Id == id));
        }
    }
}
