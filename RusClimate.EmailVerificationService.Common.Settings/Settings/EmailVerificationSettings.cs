namespace RusClimate.EmailVerificationService.Common.Data.Settings
{
    public class EmailVerificationSettings
    {
        public class RawSettings
        {
            public int? EmailTtlSeconds { get; set; }
            public int? TokenTtlSeconds { get; set; }
            public string? SecretKey { get; set; }
        }

        private const int DEFAULT_TTL_IN_SECONDS = 3600;

        public EmailVerificationSettings(RawSettings rawSettings)
        {
            EmailTtlSeconds = TimeSpan.FromSeconds(
                rawSettings.EmailTtlSeconds.GetValueOrDefault(DEFAULT_TTL_IN_SECONDS));

            TokenTtlSeconds = TimeSpan.FromSeconds(
                rawSettings.TokenTtlSeconds.GetValueOrDefault(DEFAULT_TTL_IN_SECONDS));

            SecretKey = !string.IsNullOrEmpty(rawSettings.SecretKey)
                ? rawSettings.SecretKey
                : throw new ArgumentException("SecretKey cannot be null or empty");
        }

        public TimeSpan EmailTtlSeconds { get; set; }
        public TimeSpan TokenTtlSeconds { get; set; }
        public string SecretKey { get; set; }
    }
}

