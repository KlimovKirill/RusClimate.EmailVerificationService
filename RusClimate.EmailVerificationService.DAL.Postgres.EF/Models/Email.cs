namespace RusClimate.EmailVerificationService.DAL.Postgres.EF.Models
{
    public class Email
    {
        public int Id { get; set; }

        public string Address { get; set; } = default!;

        public string Text { get; set; } = default!;

        public string Token { get; set; } = default!;

        public DateTime Sent_Date { get; set; }

        public bool IsVerified { get; set; }
    }
}
