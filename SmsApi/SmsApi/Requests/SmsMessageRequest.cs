namespace SmssApi.Requests
{
    public class SmsMessageRequest
	{
		public Guid Id { get; set; }
		public string From { get; set; }
		public string[] To { get; set; }
		public string Content { get; set; }
	}
}
