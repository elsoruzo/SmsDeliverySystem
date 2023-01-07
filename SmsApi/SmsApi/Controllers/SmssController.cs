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
            
            var smssResponse = getSmssResponse.Smss.ConvertAll(o => new SmsMessageResponse { Id = o.Id, UserId = o.UserId, From = o.From, Content = o.Content, SmsStatuses = o.SmsStatuses });

            return Ok(smssResponse);
        }

        [HttpPost]
        public async Task<IActionResult> Post(SmsMessageRequest smsRequest)
        {
            var sms = new SmsMessage { 
                //Id = smsRequest.Id, 
                From = smsRequest.From, UserId = UserID };

            var saveSmsResponse = await SmsService.SaveSms(sms);

            if (!saveSmsResponse.Success)
            {
                return UnprocessableEntity(saveSmsResponse);
            }

            var smsResponse = new SmsMessageResponse { Id = saveSmsResponse.Sms.Id, UserId = saveSmsResponse.Sms.UserId, From = saveSmsResponse.Sms.From, Content = saveSmsResponse.Sms.Content, SmsStatuses = saveSmsResponse.Sms.SmsStatuses };
            
            return Ok(smsResponse);
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
        public async Task<IActionResult> Put(SmsMessageRequest smsRequest)
        {
            var sms = new SmsMessage { Id = smsRequest.Id, From = smsRequest.From, Content = smsRequest.Content, UserId = UserID };

            var saveSmsResponse = await SmsService.SaveSms(sms);

            if (!saveSmsResponse.Success)
            {
                return UnprocessableEntity(saveSmsResponse);
            }

            var SmsResponse = new SmsMessageResponse { Id = saveSmsResponse.Sms.Id, UserId = saveSmsResponse.Sms.UserId, From = saveSmsResponse.Sms.From, Content = saveSmsResponse.Sms.Content, SmsStatuses = saveSmsResponse.Sms.SmsStatuses };

            return Ok(SmsResponse);
        }
    }
}
