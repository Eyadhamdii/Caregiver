namespace Caregiver.Models.Payment
{
    public record CreateCustomerResource(
    string Email,
    string Name,
    CreateCardResource Card);
}
