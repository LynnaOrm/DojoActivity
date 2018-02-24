using System;
using System.ComponentModel.DataAnnotations;
using DojoActivity.Models;

namespace DojoActivity.Models
{
    public class Participant : BaseEntity
    {
        [Key]
        public int ParticipantsId { get; set; } 
        public int UserId {get; set;} //Foreign Key

        public int GuestActivityId {get; set;} //Foreign Key

        public User User {get; set;} //User Objects created along with the foreign key
        public Activity GuestActivity {get; set;} //Activity Objects created along with the foreign key

        
    }
}