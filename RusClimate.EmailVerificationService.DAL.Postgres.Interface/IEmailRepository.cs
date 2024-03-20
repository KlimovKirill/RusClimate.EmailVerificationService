using RusClimate.EmailVerificationService.DAL.Postgres.EF.Models;

namespace RusClimate.EmailVerificationService.DAL.Postgres.Interface
{
    public interface IEmailRepository
    {
        Task AddNewAsync(Email email);

        Task Update(Email email);

        Task<Email> GetByToken(string token);

        Task<Email> GetByEmail(string emailAddress);
    }
}
