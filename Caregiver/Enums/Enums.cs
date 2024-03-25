﻿namespace Caregiver.Enums
{
	public static class Enums
	{
		public enum CareerLevel
		{
			Student = 1,
			FreshGraduate = 2,
			Experienced = 3
		}

		public enum City
		{
			Cairo = 1,
			Alexandria = 2,
			AboTesht = 3
		}

		public enum Gender
		{
			Male = 1,
			Female = 2
		}

		public enum JobTitle
		{
			Nurse = 1,
			Caregiver = 2,
			Babysitter = 3
		}

        public enum ReservationStatus
        {
            OnProgress =1 ,
            Done =2 ,
            Cancelled =3,
            CannotProceed=4
        }
        public enum Status
        {
            Available = 1,
            FullDay = 2,
            DayOff =3
        }
    }
}
