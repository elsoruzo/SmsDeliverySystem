using SmssApi.Requests;
using SmssApi.Responses;

namespace SmssApi.Interfaces
{
    public interface ISmsService
    {
		Task<GetSmsResponse> GetSmss(int userId);

		Task<SaveSmsResponse> SaveSms(SmsMessage sms);

		Task<DeleteSmsResponse> DeleteSms(int smsId, int userId);
    }
}
