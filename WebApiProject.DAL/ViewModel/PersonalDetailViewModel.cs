using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiProject.DAL.ViewModel
{
    public class PersonalDetailViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public DateTime? BirthDate { get; set; }

        public string PhoneNumber { get; set; }
    }
}
