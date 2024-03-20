namespace RusClimate.EmailVerificationService.Common.Data.Responses
{
    public class ServiceResponse
    {
        public bool IsOk { get; }

        public string ErrorKey { get; }
        
        public string ErrorMessage { get; }

        public ServiceResponse(bool isOk, string errorKey, string errorMessage)
        {
            IsOk = isOk;
            ErrorKey = errorKey;
            ErrorMessage = errorMessage;
        }

        public static ServiceResponse Ok() => new ServiceResponse(true, null, null);

        public static ServiceResponse Error(string errorKey = null, string errorMessage = null) =>
            new ServiceResponse(false, errorKey, errorMessage);
    }
}
