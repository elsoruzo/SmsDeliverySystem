using Microsoft.EntityFrameworkCore;
using SmssApi.Interfaces;
using SmssApi.Requests;
using SmssApi.Responses;

namespace SmssApi.Services
{
	public class SmsService : ISmsService
	{
		private readonly SmsDbContext SmssDbContext;

		public SmsService(SmsDbContext SmssDbContext)
		{
			this.SmssDbContext = SmssDbContext;
		}

		public async Task<DeleteSmsResponse> DeleteSms(int smsId, int userId)
		{
			var sms = await SmssDbContext.SmsMessages.FindAsync(smsId);

			if (sms == null)
			{
				return new DeleteSmsResponse
				{
					Success = false,
					Error = "Sms not found",
					ErrorCode = "T01"
				};
			}

			if (sms.UserId != userId)
			{
				return new DeleteSmsResponse
				{
					Success = false,
					Error = "You don't have access to delete this Sms",
					ErrorCode = "T02"
				};
			}

			SmssDbContext.SmsMessages.Remove(sms);

			var saveResponse = await SmssDbContext.SaveChangesAsync();

			if (saveResponse >= 0)
			{
				return new DeleteSmsResponse
				{
					Success = true,
					SmsId = sms.Id
				};
			}

			return new DeleteSmsResponse
			{
				Success = false,
				Error = "Unable to delete Sms",
				ErrorCode = "T03"
			};
		}

		public async Task<GetSmsResponse> GetSmss(int userId)
		{
			var smss = await SmssDbContext.SmsMessages.Where(o => o.UserId == userId).ToListAsync();

			return new GetSmsResponse { Success = true, Smss = smss };

		}

		public async Task<SaveSmsResponse> SaveSms(SmsMessage sms)
		{

			await SmssDbContext.SmsMessages.AddAsync(sms);
			try
			{
				var saveResponse = await SmssDbContext.SaveChangesAsync();

				if (saveResponse >= 0)
				{
					return new SaveSmsResponse
					{
						Success = true,
						Sms = sms
					};
				}
				return new SaveSmsResponse
				{
					Success = false,
					Error = "Unable to save Sms",
					ErrorCode = "T05"
				};
			}
			catch (Exception ex)
			{

				return new SaveSmsResponse
				{
					Success = false,
					Error = "Unable to save Sms",
					ErrorCode = "T05"
				};

			}
		}
	}
}
