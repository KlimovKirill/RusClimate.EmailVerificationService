using RusClimate.EmailVerificationService.BLL.Service.Models;
using RusClimate.EmailVerificationService.DAL.Postgres.EF.Models;

namespace RusClimate.EmailVerificationService.BLL.Service.Adapters
{
    public static class EmailAdapter
    {
        public static EmailData? ToEmailBll(this Email? emailDal)
        {
            if (emailDal == null)
                return null;

            return new EmailData
            {
                Id = emailDal.Id,
                Address = emailDal.Address,
                Text = emailDal.Text,
                Token = emailDal.Token,
                Sent_Date = emailDal.Sent_Date,
                IsVerified = emailDal.IsVerified
            };
        }

        public static Email ToEmailDal(this EmailData emailBll)
        {
            return new Email
            {
                Id = emailBll.Id,
                Address = emailBll.Address,
                Text = emailBll.Text,
                Token = emailBll.Token,
                Sent_Date = emailBll.Sent_Date,
                IsVerified = emailBll.IsVerified
            };
        }
    }
}
