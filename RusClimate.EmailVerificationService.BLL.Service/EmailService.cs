using Microsoft.AspNetCore.Http;
using RusClimate.EmailVerificationService.BLL.Interface;
using RusClimate.EmailVerificationService.BLL.Service.Adapters;
using RusClimate.EmailVerificationService.BLL.Service.Helpers;
using RusClimate.EmailVerificationService.BLL.Service.Models;
using RusClimate.EmailVerificationService.Common.Data.Responses;
using RusClimate.EmailVerificationService.Common.Data.Settings;
using RusClimate.EmailVerificationService.DAL.Postgres.Interface;

namespace RusClimate.EmailVerificationService.BLL.Service
{
    public class EmailService : IEmailService
    {
        private readonly IEmailRepository _emailRepository;
        private readonly EmailVerificationSettings _emailVerificationSettings;

        public EmailService(IEmailRepository emailRepository,
            EmailVerificationSettings emailVerificationSettings)
        {
            _emailRepository = emailRepository;
            _emailVerificationSettings = emailVerificationSettings;
        }

        public async Task<ServiceResponse> SendVerificationEmailAsync(string emailAddress, string text)
        {
            var emailData = (await _emailRepository.GetByEmail(emailAddress)).ToEmailBll();

            if (emailData is null)
            {
                var hash = GenerateHashFromEmail(emailAddress, _emailVerificationSettings.SecretKey);

                SendEmail(emailAddress);

                await _emailRepository.AddNewAsync(
                new EmailData
                {
                    Address = emailAddress,
                    Text = text,
                    Token = hash,
                    Sent_Date = DateTime.UtcNow
                }.ToEmailDal()
                );
            }
            else
            {
                var calculatedInterval = DateTime.UtcNow - emailData.Sent_Date;

                if (calculatedInterval >= _emailVerificationSettings.EmailTtlSeconds)
                {
                    return ServiceResponse.Error(StatusCodes.Status422UnprocessableEntity);
                }

                //if current token has expired, we need to generate new token and send it
                if (calculatedInterval >= _emailVerificationSettings.TokenTtlSeconds)
                {
                    var hash = GenerateHashFromEmail(emailAddress, _emailVerificationSettings.SecretKey);

                    SendEmail(emailAddress);

                    emailData.Token = hash;
                    await _emailRepository.Update(emailData.ToEmailDal());
                }
                else
                {
                    SendEmail(emailAddress);
                }
            }

            return ServiceResponse.Ok();
        }

        public async Task<ServiceResponse> VerifyAsync(string token)
        {
            var email = (await _emailRepository.GetByToken(token)).ToEmailBll();
            if (email is null)
                return ServiceResponse.Error(StatusCodes.Status404NotFound);

            var calculatedInterval = DateTime.UtcNow - email.Sent_Date;

            string decryptToken;

            if (calculatedInterval >= _emailVerificationSettings.TokenTtlSeconds)
            {
                return ServiceResponse.Error(StatusCodes.Status404NotFound);
            }
            else
            {
                decryptToken = HashGeneratorHelper.HashFunc(token, _emailVerificationSettings.SecretKey);

                var decryptParts = decryptToken.Split(" ", 2);

                if (decryptParts[0] != email.Address)
                    return ServiceResponse.Error(StatusCodes.Status403Forbidden);

                email.IsVerified = true;
                await _emailRepository.Update(email.ToEmailDal());

                // send to broker ex: await _brokerService.Send();
            }

            return ServiceResponse.Ok();
        }

        private static string GenerateHashFromEmail(string email, string secretKey)
        {
            string stringToHash = email + " " + DateTime.UtcNow.ToString();

            return HashGeneratorHelper.HashFunc(stringToHash, secretKey);
        }

        private static ServiceResponse SendEmail(string emailAddress)
        {
            try
            {
                //build email schema
                //send email
            }
            catch (Exception ex)
            {
                return ServiceResponse.Error(StatusCodes.Status502BadGateway);
            }

            return ServiceResponse.Ok();
        }
    }
}
