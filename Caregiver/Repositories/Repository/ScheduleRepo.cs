using Caregiver.Dtos;
using Caregiver.Models;
using Caregiver.Repositories.IRepository;
using Microsoft.AspNetCore.Identity;
using static Caregiver.Enums.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Caregiver.Repositories.Repository
{
	public class ScheduleRepo : GenericRepo<CaregiverSchedule>, IScheduleRepo
	{

		private readonly ApplicationDBContext _db;
		private UserManager<User> _userManager;
		private readonly IHttpContextAccessor _httpContextAccessor;
		public ScheduleRepo(ApplicationDBContext db, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor) :base(db)
		{
			_db = db;
			_userManager = userManager;
			_httpContextAccessor = httpContextAccessor;
		}
		public async Task<UserManagerResponse> AddScheduleAsync(ScheduleDTO model)
		{
			var loggedInUserId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
			var schedule = new CaregiverSchedule
			{
				CaregiverId = loggedInUserId,
				Day=model.Day,
				Status = model.Status
			};

			if (model.Status != Status.FullDay && model.Status != Status.DayOff)
			{
				schedule.FromTime = model.FromTime;
				schedule.ToTime = model.ToTime;
			}


            //var schedule = new CaregiverSchedule
            //{
            //	CaregiverId = loggedInUserId,
            //	FromTime = model.FromTime,
            //	ToTime = model.ToTime,
            //	Status = model.Status

            //};
            //try
            //{


            //	_db.CaregiverSchedule.Add(schedule);
            //	_db.SaveChanges();
            //	return new UserManagerResponse
            //	{
            //		Message = "ok",
            //		IsSuccess = true,
            //	};

            //}
            //catch
            //{
            //	return new UserManagerResponse
            //	{
            //		Message = "User did not create",
            //		IsSuccess = false,
            //	};
            //}
            var schedule = new CaregiverSchedule
            {
                CaregiverId = loggedInUserId,
                Status = model.Status
            };

            if (model.Status != Status.FullDay || model.Status != Status.DayOff)
            {
                schedule.FromTime = model.FromTime;
                schedule.ToTime = model.ToTime;
            }

			try
			{
				_db.CaregiverSchedules.Add(schedule);
				_db.SaveChanges();
				return new UserManagerResponse
				{
					Message = "ok",
					IsSuccess = true,
				};
			}
			catch
			{
				return new UserManagerResponse
				{
					Message = "User did not create",
					IsSuccess = false,
				};
			}
            try
            {
                _db.CaregiverSchedules.Add(schedule);
                _db.SaveChanges();
                return new UserManagerResponse
                {
                    Message = "ok",
                    IsSuccess = true,
                };
            }
            catch
            {
                return new UserManagerResponse
                {
                    Message = "User did not create",
                    IsSuccess = false,
                };
            }


        }
	}
}
