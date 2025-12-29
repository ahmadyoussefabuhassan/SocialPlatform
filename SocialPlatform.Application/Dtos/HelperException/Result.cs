namespace SocialPlatform.Application.Dtos.HelperException
{
    public record Result<T>(
        bool Success,
        T? Data = default,
        string? Error = null,
        string? Message = null
    );
    // Result <Ie<t>>
}
