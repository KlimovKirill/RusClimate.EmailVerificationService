using RusClimate.EmailVerificationService.Common.Data.Enums;

namespace RusClimate.EmailVerificationService.Common.Data.Responses
{
    public class ServiceResponse
    {
        public bool IsOk { get; }

        public int ErrorCode { get; }
        
        public string ErrorMessage { get; }

        public ServiceResponse(bool isOk, int errorCode, string errorMessage)
        {
            IsOk = isOk;
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        public static ServiceResponse Ok() => new ServiceResponse(true, (int)AppStatusCodes.Success200Ok, null);

        public static ServiceResponse Error(int errorCode, string errorMessage = null) =>
            new ServiceResponse(false, errorCode, errorMessage);
    }
}
