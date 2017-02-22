using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMvc.Dal
{
    public class User
    {
        public User()
        {
            Educations = new List<Education>();
        }

        [Key]
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Age { get; set; }
        public string Gender { get; set; }
        public DateTime? EmploymentDate { get; set; }

        public ICollection<Education> Educations { get; set; }
    }
}
