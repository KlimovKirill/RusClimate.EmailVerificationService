using RusClimate.EmailVerificationService.Common.Data.Responses;

namespace RusClimate.EmailVerificationService.BLL.Interface
{
    public interface IEmailService
    {
        Task<ServiceResponse> SendVerificationEmailAsync(string email, string text);

        Task<ServiceResponse> VerifyAsync(string url);
    }
}
