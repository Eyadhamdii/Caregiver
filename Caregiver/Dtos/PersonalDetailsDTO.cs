using static Caregiver.Enums.Enums;

namespace Caregiver.Dtos
{
    public class PersonalDetailsDTO
    {
        //changge here
        public int ReservationNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int Age { get; set; }
        public string Location { get; set; }
        public string EmailAddress { get; set; }
        public Gender Gender { get; set; }

        public string ReservationNotes { get; set; }

        public ReservationType ReservationType { get; set; }
    }
}
