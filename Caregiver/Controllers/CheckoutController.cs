﻿using Caregiver.Dtos;
using Caregiver.Models;
using Caregiver.Repositories.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;

namespace Caregiver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {

        private IConfiguration _configuration;
        private UserManager<User> _userManager;
        private ApplicationDBContext _dbContext;
        private readonly IReservationsRepo reservationsRepo;

        public CheckoutController(IConfiguration configuration, UserManager<User> userManager, ApplicationDBContext dbContext, IReservationsRepo _reservationsRepo)
        {
            _configuration = configuration;
            _userManager = userManager;
            _dbContext = dbContext;
            reservationsRepo = _reservationsRepo;
        }

        [HttpPost("CreateCheckout")]
        public async Task<IActionResult> CreateCheckoutSession(int id)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

            var loggedInUserId = _userManager.GetUserId(HttpContext.User);
            var user = await _userManager.FindByIdAsync(loggedInUserId);
            var email = user.Email;
            var reservationOrderId = await reservationsRepo.GetReservationById(id);
            var reservation = await _dbContext.Reservations.FirstOrDefaultAsync(a=>a.PatientId==loggedInUserId);
          
            var amount = (int)Math.Round(reservation.TotalPriceWithfees * 100);
            
           
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
                CustomerEmail = email,
                Metadata = new Dictionary<string, string>
        {
            { "order_id", id.ToString() }
        }


            };
           
            var service = new Stripe.Checkout.SessionService();

            try
            {
                var session = service.Create(options);
                return Ok(new
                {
                    sessionId = session.Id,
                    sessionlink = session.Url,
                   
                }) ;

            }
            catch (StripeException e)
            {
                return BadRequest(new { ErrorMessage = e.StripeError.Message });
            }
        }
    }
}