namespace SmssApi.Responses
{
    public class SmsMessageResponse
	{
		public Guid Id { get; set; }
		public int UserId { get; set; }
		public string From { get; set; }
		public string Content { get; set; }
		public virtual ICollection<SmsStatus> SmsStatuses { get; set; }
	}
}
