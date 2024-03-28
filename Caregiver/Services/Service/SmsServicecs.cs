using Caregiver.Helpers;
using Caregiver.Services.IService;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Caregiver.Services.Service
{
    public class SmsServicecs : ISmsServicecs
    {
        private readonly TwilioSettings _TwilioSettings;

        public SmsServicecs(IOptions<TwilioSettings> twilioSettings)
        {
            //hiro7 y5od el values/data el gwa el appsettings w y map kol value b esm el key bt3ha b esm property el gwa el class lzm tkon el asmi wa7da
            _TwilioSettings = twilioSettings.Value;
        }

        public MessageResource sendMessage(string mobileNumber, string body)
        {

            TwilioClient.Init(_TwilioSettings.AccountSID, _TwilioSettings.AuthToken);
            var result = MessageResource.Create(
               body: body,
               from: new Twilio.Types.PhoneNumber(_TwilioSettings.TwilioPhoneNumber),
               to: mobileNumber
              );
            return result;
        }
    }
}
