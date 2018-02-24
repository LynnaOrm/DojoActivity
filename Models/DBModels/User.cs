using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DojoActivity.Models;

namespace DojoActivity.Models
{
    public class User : BaseEntity
    {
            [Key]
            public int id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }

            public List<Participant> Participants {get; set;}

            public User()
            {
            Participants = new List<Participant>();
            }

    }
}