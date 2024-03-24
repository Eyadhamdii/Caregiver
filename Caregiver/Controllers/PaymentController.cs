using Caregiver.Models.Payment;
using Caregiver.Repositories.IRepository;
using Caregiver.Repositories.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Caregiver.Controllers
{
    [Route("stripe")]
    public class PaymentController : ControllerBase
    {
        private readonly IStripeRepo _stripeRepo;

        public PaymentController(IStripeRepo stripeRepo)
        {
            _stripeRepo = stripeRepo;

        }

            [HttpPost("customer")]
            public async Task<ActionResult<CustomerResource>> CreateCustomer([FromBody] CreateCustomerResource resource,
                CancellationToken cancellationToken)
            {
                var response = await _stripeRepo.CreateCustomer(resource, cancellationToken);
                return Ok(response);
            }

            [HttpPost("charge")]
            public async Task<ActionResult<ChargeResource>> CreateCharge([FromBody] CreateChargeResource resource, CancellationToken cancellationToken)
            {
                var response = await _stripeRepo.CreateCharge(resource, cancellationToken);
                return Ok(response);
            }
        }
    }
