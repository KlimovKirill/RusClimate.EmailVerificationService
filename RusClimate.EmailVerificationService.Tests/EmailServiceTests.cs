using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using RusClimate.EmailVerificationService.BLL.Interface;
using RusClimate.EmailVerificationService.BLL.Service;
using RusClimate.EmailVerificationService.Common.Data.Settings;
using RusClimate.EmailVerificationService.DAL.Postgres.EF;
using RusClimate.EmailVerificationService.DAL.Postgres.Interface;
using Assert = NUnit.Framework.Assert;

namespace RusClimate.EmailVerificationService.Tests
{
    [TestFixture]
    public class EmailServiceTests
    {
        private IEmailService _emailService;
        private ApplicationDbContext _context;

        [SetUp]
        public void Startup()
        {
            var fakeIEmailRepo = new Mock<IEmailRepository>();
            var fakeEmailVerificationSettings = new Mock<EmailVerificationSettings>();

            var fakeAppDbContextOptions = new Mock<DbContextOptions<ApplicationDbContext>>();

            _emailService = new EmailService(fakeIEmailRepo.Object, fakeEmailVerificationSettings.Object);
            _context = new ApplicationDbContext(fakeAppDbContextOptions.Object);
        }

        //some problem with running
        [Test]
        public async Task CheckVerificationTest()
        {
            //Arrange
            string emailAddress = "first@gmail.com";
            string text = "some text";

            await _emailService.SendVerificationEmailAsync(emailAddress, text);

            //Act
            var addedEmail = _context.Emails.FirstOrDefault(x => x.Address == emailAddress);
            if(addedEmail == null)
                throw new ArgumentNullException(nameof(addedEmail));

            await _emailService.VerifyAsync(addedEmail.Token);

            //Assert
            var verifiedEmail = _context.Emails.FirstOrDefault(x => x.Address == emailAddress);
            if (verifiedEmail == null)
                throw new ArgumentNullException(nameof(verifiedEmail));

            Assert.Equals(verifiedEmail.IsVerified, true);
        }
    }
}
