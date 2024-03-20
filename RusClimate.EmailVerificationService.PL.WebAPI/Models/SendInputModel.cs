using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace RusClimate.EmailVerificationService.PL.WebAPI.Models
{
    public class SendInputModel : IValidatableObject
    {
        public string Email { get; set; }

        [MinLength(1, ErrorMessage = "Text should not be empty!")]
        public string Text { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            try
            {
                MailAddress m = new MailAddress(Email);
            }
            catch (Exception)
            {
                errors.Add(new ValidationResult("Incorrect email format"));
            }

            return errors;
        }
    }
}
