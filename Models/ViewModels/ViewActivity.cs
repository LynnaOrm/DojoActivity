using System;
using System.ComponentModel.DataAnnotations;

namespace DojoActivity.Models
{
    public class AddActivity : BaseEntity
    {
        [Display(Name = "Title")]
        [Required]
        [MinLength(2)]
        public string Title {get; set;}

        [Required]
        [Display(Name = "Date of Event")]
        [MyDate(ErrorMessage ="Date must be in future")]
        public DateTime DateofEvent {get; set;}

        [Required]
        [Display(Name = "Time of Event")]
        public string TimeofEvent {get; set;}


        [Display(Name = "Duration")]
        [Required]
        public string Duration {get; set;}

        [Display(Name = "Description")]
        [Required]
        [MinLength(2)]
        public string Description {get; set;}



    }   
    public class MyDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime d = Convert.ToDateTime(value);
            return d >= DateTime.Now;
        }

    }
}