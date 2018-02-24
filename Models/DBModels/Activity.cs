using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DojoActivity.Models;

namespace DojoActivity.Models
{
    public class Activity : BaseEntity
    {
        [Key]
        public int ActivityId { get; set;}

        public string Title {get; set;}

        public DateTime DateofEvent { get; set; }

        public string TimeofEvent {get; set;}

        public string Duration {get; set;}

        public string Description {get; set;}

        public int UserId {get; set;}


        public List<Participant> Participants {get; set;}

        public Activity()
        {
            Participants = new List<Participant>();
        }

    }
}