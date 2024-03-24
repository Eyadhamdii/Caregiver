namespace Caregiver.Models.Payment
{
    public record CreateCardResource(
     string Name,
     string Number,
     string ExpiryYear,
     string ExpiryMonth,
     string Cvc
    //string Token
);
}
