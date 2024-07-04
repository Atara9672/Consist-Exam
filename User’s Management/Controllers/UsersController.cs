using Microsoft.AspNetCore.Mvc;

namespace User_s_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        public ActionResult<User> CreateUser(User user)
        {
            var createdUser = _userRepository.CreateUser(user);
            if (createdUser == null)
                return NotFound("Error in create user");
            return CreatedAtAction(nameof(CreateUser), new { userId = createdUser.UserId }, createdUser);
        }

        [HttpDelete("{userId}")]
        public IActionResult DeleteUser(Guid userId)
        {
            var success = _userRepository.DeleteUser(userId);
            if (success)
            {
                return Ok();
            }
            return NotFound();
        }

        [HttpGet("validate")]
        public ActionResult<User> ValidateUser(string username, string password)
        {
            var user = _userRepository.ValidateUser(username, password);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }
    }

}
