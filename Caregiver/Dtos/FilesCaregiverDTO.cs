namespace Caregiver.Dtos
{
	public class FilesCaregiverDTO
	{
		[Required]

		public IFormFile Resume { get; set; }
		[Required]
		public IFormFile CriminalRecords { get; set; }

		[Required]
		public IFormFile UploadPhoto { get; set; }
	}
}
