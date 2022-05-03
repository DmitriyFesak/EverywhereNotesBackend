namespace EverywhereNotes.Models.ResultModel
{
    public enum ErrorCode
    {
        ValidationError,
        Unauthorized,
        InternalServerError,
        NotFound,
        UnprocessableEntity,
        Conflict,
        TokenExpired,
        Forbidden,
    }
}
