
using Caregiver.Configurations;
using Caregiver.Dtos;
using Caregiver.Models;
using Caregiver.Repositories.IRepository;
using Caregiver.Repositories.Repository;
using Caregiver.Services.IService;
using Caregiver.Services.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Stripe;
using System.Security.Claims;
using System.Text;

namespace Caregiver
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
			builder.Services.AddDbContext<ApplicationDBContext>(options =>

				options.UseSqlServer(connectionString)
			);

			builder.Services.AddControllers().AddJsonOptions(options => { options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles; });

            builder.Services.AddCors(options =>
			{
				options.AddPolicy(name: "angularlocalhost",
								  policy =>
								  {
									  policy.AllowAnyHeader();
									  //policy.AllowAnyOrigin();
									  policy.WithOrigins("http://localhost:4200"); //angular origin 
									  policy.AllowAnyMethod();
								  });
			});

			builder.Services.AddIdentity<User, IdentityRole>(options =>
			{
				options.Password.RequireUppercase = false;
				options.Password.RequireLowercase = false;
				options.Password.RequireNonAlphanumeric = false;
				options.User.RequireUniqueEmail = true;
				options.Lockout.MaxFailedAccessAttempts = 3;
				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);


			}).AddEntityFrameworkStores<ApplicationDBContext>()
			.AddDefaultTokenProviders();

			builder.Services.AddAuthentication(
	 options =>
	 {
		 //install bearer package
		 options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
		 options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	 }
			).AddJwtBearer(options =>
			{

				var secretKey = builder.Configuration.GetValue<string>("ApiSettings:secret");
				var secretKeyInBytes = Encoding.ASCII.GetBytes(secretKey);
				var key = new SymmetricSecurityKey(secretKeyInBytes);


				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true, //??
					IssuerSigningKey = key,
					ValidateIssuer = false,
					ValidateAudience = false

				};
			});


			builder.Services.AddAuthorization(options =>
			{
				options.AddPolicy("Caregiver", policy =>
					policy
					.RequireClaim(ClaimTypes.Role, "CaregiverUser"));

				options.AddPolicy("RegularUser", policy =>
					policy
					.RequireClaim(ClaimTypes.Role, "PatientUser"));
			});
			//automapper 
			builder.Services.AddAutoMapper(typeof(MappingConfiguration));
			//generic repo

			builder.Services.AddScoped<IGenericRepo<CaregiverUser>, GenericRepo<CaregiverUser>>();

			builder.Services.AddScoped<ICaregiverService, CaregiverService>();

		//	builder.Services.AddScoped<ICaregiverRepo, CaregiverRepo>();
			builder.Services.AddScoped<IUserRepo, UserRepo>();

			builder.Services.AddScoped<APIResponse, APIResponse>();


			

			builder.Services.AddControllers();
			builder.Services.AddTransient<IEmailRepo, EmailRepo>();
            builder.Services.AddTransient<IReservationsRepo, ReservationsRepo>();

            builder.Services.AddScoped<TokenService>();
            builder.Services.AddScoped<CustomerService>();
            builder.Services.AddScoped<ChargeService>();

            StripeConfiguration.ApiKey = builder.Configuration.GetValue<string>("Stripe:SecretKey");


            builder.Services.AddControllers().AddJsonOptions(options =>
			{
				options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
			});
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseCors("angularlocalhost");

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
