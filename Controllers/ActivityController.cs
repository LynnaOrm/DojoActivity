using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DojoActivity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace DojoActivity.Controllers
{
    public class ActivityController : Controller
    {
        private DojoActivityContext _context;

        public ActivityController(DojoActivityContext context)
        {
            _context = context;
        }

    
    
    
    [HttpGet]
    [Route("/logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }



    private User ActiveUser
    {
        get{ return _context.Users.Where(u => u.id == HttpContext.Session.GetInt32("id")).FirstOrDefault();}
    }



    [HttpGet]
    [Route("Dashboard")]
    public IActionResult Dashboard()
    {
        if(ActiveUser == null)
        {
            return RedirectToAction("Index", "Home");
        }
        User user = _context.Users.Where(u => u.id == HttpContext.Session.GetInt32("id")).FirstOrDefault();
        ViewBag.UserInfo = user;

         ViewBag.AllActivities = _context.Activities.ToList();

        return View("Dashboard");
    }



    [HttpGet]
    [Route("/newActivity")]
    public IActionResult newActivity()
    {
        System.Console.WriteLine("New Activity Works!!!");
        if(ActiveUser == null)
        return RedirectToAction("Index", "Home");

        ViewBag.UserInfo = ActiveUser;
        return View("NewActivity");    
        }


    
    [HttpPost]
    [Route("AddActivity")]
    public IActionResult AddActivity(AddActivity activity)
    {
        System.Console.WriteLine("Add Activity Works!!!");

        if(ActiveUser == null)
            return RedirectToAction("Index", "Home");

        if(ModelState.IsValid)
        {
            Activity Activity = new Activity
            {
                UserId = ActiveUser.id,
                Title = activity.Title,
                DateofEvent = activity.DateofEvent,
                TimeofEvent = activity.TimeofEvent,
                Duration = activity.Duration,
                Description = activity.Description,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now

            };

            _context.Activities.Add(Activity);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        ViewBag.UserInfo = ActiveUser;
        return View("NewActivity");
    }

    [HttpGet]
    [Route("activity/{ActivityId}")]
        public IActionResult ShowActivity(int ActivityId)
        {
            if(ActiveUser == null)
                return RedirectToAction("Index", "Home");

                ViewBag.ShowOne = _context.Activities.Where(o => o.ActivityId == ActivityId).Include(g => g.Participants).ThenInclude(h => h.User).SingleOrDefault();

    
            return View ("ShowActivity");
        }

        
        [HttpPost]
        [Route("delete")]
        public IActionResult Delete(int ActivityId)
        {
            if(ActiveUser == null)
                return RedirectToAction("Index", "Home");


                Activity ToDelete = _context.Activities.SingleOrDefault(ShowOne => ShowOne.ActivityId == ActivityId);

                _context.Activities.Remove(ToDelete);
                _context.SaveChanges();


                ViewBag.UserInfo = ActiveUser;
                System.Console.WriteLine("Deleting");
                List<Activity> Activities = _context.Activities.ToList();



                return RedirectToAction ("Dashboard");
        }


        [HttpPost]
        [Route("Join")]
        public IActionResult Join(int ActivityId)
        {
            System.Console.WriteLine(":D");

                Participant addJoin = new Participant
                {
                    UserId = (int)HttpContext.Session.GetInt32("id"),
                    GuestActivityId = ActivityId
                };

                _context.Participants.Add(addJoin);
                _context.SaveChanges();




            return RedirectToAction("Dashboard");
        }

        
        
        
        [HttpPost]
        [Route("Leave")]
        public IActionResult Leave(int ActivityId)
        {
            System.Console.WriteLine(":(");

            Participant Removejoin = _context.Participants.SingleOrDefault(x => x.GuestActivityId == ActivityId && x.UserId == (int)HttpContext.Session.GetInt32("id"));
            _context.Participants.Remove(Removejoin);
            _context.SaveChanges();

            return RedirectToAction("Dashboard");
        }



















    }
}