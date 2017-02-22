using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyMvc.Areas.Security.Models
{
    public class UserViewModel
    {
        public Guid Guid { get; set; }

        [Required, MinLength(5, ErrorMessage = "Min of 5 characters"), MaxLength(10, ErrorMessage = "Max of 10 characters")]
        public string Firstname { get; set; }

        [Required, Display(Name = "Family Name")]
        public string LastName { get; set; }

        public string FullName { get { return Firstname + " " + LastName; } }

        public int? Age { get; set; }

        public string Gender { get; set; }

        public IList<string> Schools { get; set; }

    }
}