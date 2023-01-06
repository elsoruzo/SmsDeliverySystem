using Microsoft.EntityFrameworkCore;
using SmssApi.Interfaces;
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

        public async Task<DeleteSmsResponse> DeleteSms(int SmsId, int userId)
        {
            var Sms = await SmssDbContext.Smss.FindAsync(SmsId);

            if (Sms == null)
            {
                return new DeleteSmsResponse
                {
                    Success = false,
                    Error = "Sms not found",
                    ErrorCode = "T01"
                };
            }

            if (Sms.UserId != userId)
            {
                return new DeleteSmsResponse
                {
                    Success = false,
                    Error = "You don't have access to delete this Sms",
                    ErrorCode = "T02"
                };
            }

            SmssDbContext.Smss.Remove(Sms);

            var saveResponse = await SmssDbContext.SaveChangesAsync();

            if (saveResponse >= 0)
            {
                return new DeleteSmsResponse
                {
                    Success = true,
                    SmsId = Sms.Id
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
            var Smss = await SmssDbContext.Smss.Where(o => o.UserId == userId).ToListAsync();

            return new GetSmsResponse { Success = true, Smss = Smss };

        }

        public async Task<SaveSmsResponse> SaveSms(Sms Sms)
        {
            if (Sms.Id == 0)
            {
                await SmssDbContext.Smss.AddAsync(Sms);
            }
            else
            {
                var SmsRecord = await SmssDbContext.Smss.FindAsync(Sms.Id);

                SmsRecord.IsCompleted = Sms.IsCompleted;
                SmsRecord.Ts = Sms.Ts;
            }
            
            var saveResponse = await SmssDbContext.SaveChangesAsync();
            
            if (saveResponse >= 0)
            {
                return new SaveSmsResponse
                {
                    Success = true,
                    Sms = Sms
                };
            }
            return new SaveSmsResponse
            {
                Success = false,
                Error = "Unable to save Sms",
                ErrorCode = "T05"
            };
        }
    }
}
