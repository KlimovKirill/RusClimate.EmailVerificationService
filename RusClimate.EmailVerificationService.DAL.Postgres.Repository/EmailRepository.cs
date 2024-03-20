using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using RusClimate.EmailVerificationService.DAL.Postgres.EF;
using RusClimate.EmailVerificationService.DAL.Postgres.EF.Models;
using RusClimate.EmailVerificationService.DAL.Postgres.Interface;

namespace RusClimate.EmailVerificationService.DAL.Postgres.Repository
{
    public class EmailRepository : IEmailRepository
    {
        private IMemoryCache cache;

        private readonly ApplicationDbContext _context;

        public EmailRepository(ApplicationDbContext context, IMemoryCache memoryCache)
        {
            _context = context;
            cache = memoryCache;
        }

        public async Task AddNewAsync(Email email)
        {
            _context.Emails.Add(email);
            var count = await _context.SaveChangesAsync();

            if (count > 0)
            {
                cache.Set(email.Token, email, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                });
            }
        }

        public async Task Update(Email email)
        {
            _context.Emails.Update(email);
            await _context.SaveChangesAsync();
        }

        public async Task<Email> GetByToken(string token)
        {
            Email email = null;

            if (!cache.TryGetValue(token, out email))
            {
                email = await _context.Emails.FirstOrDefaultAsync(x => x.Token == token);

                if (email != null)
                    cache.Set(email.Token, email, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
            }

            return email;
        }

        public async Task<Email> GetByEmail(string emailAddress)
        {
            Email emailData = null;

            if (!cache.TryGetValue(emailAddress, out emailData))
            {
                emailData = await _context.Emails.FirstOrDefaultAsync(x => x.Address == emailAddress);

                if (emailData != null)
                    cache.Set(emailData.Address, emailData, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
            }

            /*if (email == null)
                throw new ArgumentNullException(nameof(email));*/

            return emailData;
        }
    }
}
