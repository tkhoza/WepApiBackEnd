using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiProject.DAL.Models
{
    public class PersonalDetail : AuditableEntity, IAuditableEntity
    {

        [Required]
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public DateTime? BirthDate { get; set; }

        public string PhoneNumber { get; set; }
    }
}
