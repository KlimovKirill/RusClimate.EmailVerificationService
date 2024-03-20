using System.ComponentModel.DataAnnotations;

namespace RusClimate.EmailVerificationService.PL.WebAPI.Models
{
    public class CheckInputModel
    {
        [Required(ErrorMessage = "Token can not be empty!")]
        [StringLength(maximumLength: 10, ErrorMessage = "Token can not have more than 1700 symbols!")]
        public string Token { get; set; }
    }
}
