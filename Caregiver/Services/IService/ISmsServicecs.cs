using Twilio.Rest.Api.V2010.Account;
using Twilio.TwiML.Messaging;

namespace Caregiver.Services.IService
{
    public interface ISmsServicecs
    {
        //Data type => message resource
        //2 param => Mobile number, message body
        MessageResource sendMessage(string mobileNumber, string body);

    }
}
