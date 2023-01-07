namespace SmssApi.Responses
{
    public class GetSmsResponse : BaseResponse
    {
        public List<SmsMessage> Smss { get; set; }
    }
}
