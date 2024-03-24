using Caregiver.Models.Payment;

namespace Caregiver.Repositories.IRepository
{
    public interface IStripeRepo
    {
       Task<CustomerResource> CreateCustomer(CreateCustomerResource resource, CancellationToken cancellationToken);
       Task<ChargeResource> CreateCharge(CreateChargeResource resource, CancellationToken cancellationToken);   
    }
}
