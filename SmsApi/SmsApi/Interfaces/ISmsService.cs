using SmssApi.Responses;

namespace SmssApi.Interfaces
{
    public interface ISmsService
    {
		Task<GetSmsResponse> GetSmss(int userId);

		Task<SaveSmsResponse> SaveSms(Sms Sms);

		Task<DeleteSmsResponse> DeleteSms(int SmsId, int userId);
    }
}
