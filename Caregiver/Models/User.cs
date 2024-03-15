using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Reflection;

namespace Caregiver.Models
{
    public class User
    {
        [Key]
        public int SSN { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string address { get; set; }

        public string Location { get; set; }

        public string Bio { get; set; }
        public int priceH { get; set; }
        public int priceD { get; set; }
    }
}
