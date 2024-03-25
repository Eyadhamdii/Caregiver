using static Caregiver.Enums.Enums;

namespace Caregiver.Dtos
{
    public class PersonalDetailsDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PhoneNumber { get; set; }
        public int Age { get; set; }
        public string Location { get; set; }
        public string EmailAddress { get; set; }
        public Gender Gender { get; set; }
        public string ReservationNotes { get; set; }

        public ReservationType ReservationType { get; set; }
    }
}
