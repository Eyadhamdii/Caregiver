using Caregiver.Dtos;
using Caregiver.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace Caregiver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {

        private IConfiguration _configuration;
        private UserManager<User> _userManager;

        public CheckoutController(IConfiguration configuration, UserManager<User> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        [HttpGet("Products")]
        public IActionResult GetProducts()
        {
            //StripeConfiguration.ApiKey = "sk_test_51Oevw8Akocj1NhM27tLAmOl0f1SybvzfgLlctwhd9QL60lJ8XkvD4sUfmHiE6PF2WqMBWw9y7N1gp0N9uKnf0gEZ00mJoWy3K7";
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
            var options = new ProductListOptions { Limit = 3 };
            var service = new ProductService();
            StripeList<Product> products = service.List(options);

            return Ok(products);
        }

        [HttpPost("Create Checkout")]
        public async Task<IActionResult> CreateCheckoutSession([FromBody] CheckoutSessionDTO model)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

            var user = await _userManager.FindByIdAsync(model.CustomerId);

            var email = user.Email;
            var amount = model.ReservationFee * 100;

            var options = new Stripe.Checkout.SessionCreateOptions
            {
                SuccessUrl = "http://localhost:3000/booking-success",
                CancelUrl = "http://localhost:3000/booking-cancel",
                Mode = "payment",
                LineItems = new List<Stripe.Checkout.SessionLineItemOptions>
                {
                    new Stripe.Checkout.SessionLineItemOptions
                    {
                        //Price = priceId,
                        PriceData = new Stripe.Checkout.SessionLineItemPriceDataOptions
                        {
                            Currency = "usd",
                            ProductData = new Stripe.Checkout.SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Caregiver Reservation"
                            },
                            UnitAmount = amount
                        },
                        Quantity = 1
                    }
                },
                CustomerEmail = email
            };

            var service = new Stripe.Checkout.SessionService();

            try
            {
                var session = service.Create(options);
                return Ok(new { sessionId = session.Id });

            }
            catch (StripeException e)
            {
                return BadRequest(new { ErrorMessage = e.StripeError.Message });
            }
        }
    }
}