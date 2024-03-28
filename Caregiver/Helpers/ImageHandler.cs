using Caregiver.Models;
using Microsoft.AspNetCore.Mvc;

namespace Caregiver.Helpers
{
	public  static class ImageHandler
	{
       
        public static JsonResult ShowImage(byte[] image)
		{

			//CaregiverUser user = _db.Caregivers.FirstOrDefault(i => i.Id == loggedInUserId);
			//var image = user.Photo;

			var base64Image = Convert.ToBase64String(image);

			return new JsonResult(new { ImageData = base64Image });
		}

	

	}
}
