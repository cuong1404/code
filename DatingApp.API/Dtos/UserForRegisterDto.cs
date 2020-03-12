using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Dtos {
    public class UserForRegisterDto {
        [Required]
        [MinLength (3)]
        public string Username { get; set; }

        [Required]
        [MinLength (3)]
        public string Password { get; set; }
    }
}