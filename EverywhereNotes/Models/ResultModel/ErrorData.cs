using EverywhereNotes.Models.Enums;

namespace EverywhereNotes.Models.ResultModel
{
    public class ErrorData
    {
        public ErrorCode Code { get; set; }

        public string Message { get; set; }
    }
}
