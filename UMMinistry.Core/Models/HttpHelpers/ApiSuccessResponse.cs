namespace UMMinistry.Core.Models.HttpHelpers;

public class ApiSuccessResponse<TData>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public TData Data { get; set; }
    public int HttpCode { get; set; }
    public string Code { get; set; }
}