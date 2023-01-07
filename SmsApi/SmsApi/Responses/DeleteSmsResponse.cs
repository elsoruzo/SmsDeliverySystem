using System.Text.Json.Serialization;

namespace SmssApi.Responses
{
    public class DeleteSmsResponse : BaseResponse
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Guid SmsId { get; set; }
    }
}
