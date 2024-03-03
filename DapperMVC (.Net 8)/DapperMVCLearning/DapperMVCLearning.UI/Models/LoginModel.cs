using System.ComponentModel.DataAnnotations;

namespace DapperMVCLearning.UI.Models
{
    public class LoginModel
    {
        public int Id { get; set; }

        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Email { get; set; }
        [Required]
        public string? passwd { get; set; }
    }
}
