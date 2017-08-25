using System.ComponentModel.DataAnnotations;

namespace MDW.Models
{
    public class Login
    {
        [Required(ErrorMessage = "This is a required field")]
        public string Username { get; set; }

        [Required(ErrorMessage = "This is a required field")]
        public string Password { get; set; }

        public bool Remember { get; set; }
    }
}