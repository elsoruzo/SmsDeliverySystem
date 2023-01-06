using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmssApi.Interfaces;
using SmssApi.Requests;
using SmssApi.Responses;

namespace SmssApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SmssController : BaseApiController
    {
        private readonly ISmsService SmsService;

        public SmssController(ISmsService SmsService)
        {
            this.SmsService = SmsService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var getSmssResponse = await SmsService.GetSmss(UserID);

            if (!getSmssResponse.Success)
            {
                return UnprocessableEntity(getSmssResponse);
            }
            
            var SmssResponse = getSmssResponse.Smss.ConvertAll(o => new SmsResponse { Id = o.Id, IsCompleted = o.IsCompleted, Name = o.Name, Ts = o.Ts });

            return Ok(SmssResponse);
        }

        [HttpPost]
        public async Task<IActionResult> Post(SmsRequest SmsRequest)
        {
            var Sms = new Sms { IsCompleted = SmsRequest.IsCompleted, Ts = SmsRequest.Ts, Name = SmsRequest.Name, UserId = UserID };

            var saveSmsResponse = await SmsService.SaveSms(Sms);

            if (!saveSmsResponse.Success)
            {
                return UnprocessableEntity(saveSmsResponse);
            }

            var SmsResponse = new SmsResponse { Id = saveSmsResponse.Sms.Id, IsCompleted = saveSmsResponse.Sms.IsCompleted, Name = saveSmsResponse.Sms.Name, Ts = saveSmsResponse.Sms.Ts };
            
            return Ok(SmsResponse);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest(new DeleteSmsResponse { Success = false, ErrorCode = "D01", Error = "Invalid Sms id" });
            }
            var deleteSmsResponse = await SmsService.DeleteSms(id, UserID);
            if (!deleteSmsResponse.Success)
            {
                return UnprocessableEntity(deleteSmsResponse);
            }

            return Ok(deleteSmsResponse.SmsId);
        }

        [HttpPut]
        public async Task<IActionResult> Put(SmsRequest SmsRequest)
        {
            var Sms = new Sms { Id = SmsRequest.Id, IsCompleted = SmsRequest.IsCompleted, Ts = SmsRequest.Ts, Name = SmsRequest.Name, UserId = UserID };

            var saveSmsResponse = await SmsService.SaveSms(Sms);

            if (!saveSmsResponse.Success)
            {
                return UnprocessableEntity(saveSmsResponse);
            }

            var SmsResponse = new SmsResponse { Id = saveSmsResponse.Sms.Id, IsCompleted = saveSmsResponse.Sms.IsCompleted, Name = saveSmsResponse.Sms.Name, Ts = saveSmsResponse.Sms.Ts };

            return Ok(SmsResponse);
        }
    }
}
