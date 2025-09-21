using UMMinistry.Core.Models.HttpHelpers;

namespace UMMinistry.Core.Models.General;

public class ApiErrorException : Exception
{
    public ApiErrorResponse ApiErrorResponse { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="apiErrorResponse"></param>
    /// <param name="message"></param>
    public ApiErrorException(ApiErrorResponse? apiErrorResponse) : base(apiErrorResponse?.ErrorMessage)
    {
        ApiErrorResponse = apiErrorResponse;
    }
}