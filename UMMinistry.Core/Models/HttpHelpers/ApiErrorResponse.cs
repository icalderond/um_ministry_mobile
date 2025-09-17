namespace UMMinistry.Core.Models.HttpHelpers;

public class ApiErrorResponse
{
    public string ErrorMessage { get; set; }
    public bool Success { get; set; }
    public string ErrorCode { get; set; }
    public int HttpCode { get; set; }
    public string Code { get; set; }
    public string Field { get; set; }
}