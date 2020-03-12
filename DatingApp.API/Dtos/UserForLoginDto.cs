using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Dtos
{
    public class UserForLoginDto
    {
        [Required]
        [MinLength (3)]
        public string Username { get; set; }

        [Required]
        [MinLength (3)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}