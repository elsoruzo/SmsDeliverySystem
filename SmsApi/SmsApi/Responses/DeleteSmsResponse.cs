using System.Text.Json.Serialization;

namespace SmssApi.Responses
{
    public class DeleteSmsResponse : BaseResponse
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int SmsId { get; set; }
    }
}
