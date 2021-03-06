﻿using System;
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
    public class HomeController : Controller
    {
        private DojoActivityContext _context;

        public HomeController(DojoActivityContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View("Index");
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterUser newUser)
        {
            System.Console.WriteLine("Register here!");

             if(_context.Users.Where(u =>u.Email == newUser.Email).SingleOrDefault() != null)
                ModelState.AddModelError("Email", "Email is already in use!"); //checks to see if the new email is already in use. 

                if(ModelState.IsValid)
                {
                    PasswordHasher<RegisterUser> hasher = new PasswordHasher<RegisterUser>();

                    User User = new User
                    {
                        FirstName = newUser.FirstName,
                        LastName = newUser.LastName,
                        Email = newUser.Email,
                        Password = hasher.HashPassword(newUser, newUser.Password),
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now

                    };

                    User theUser = _context.Add(User).Entity;
                    _context.SaveChanges();

                    System.Console.WriteLine("YAY MADE IT!");

                    HttpContext.Session.SetInt32("id", theUser.id);

                    return RedirectToAction("Dashboard", "Activity");
                }
                return View("Index");
        }


        [HttpPost]
        [Route("/login")]
        public IActionResult Login(LoginUser logUser)
        {
            System.Console.WriteLine("Login works!");
            PasswordHasher<LoginUser> hasher = new PasswordHasher<LoginUser>();

            User userToLog = _context.Users.Where(u => u.Email == logUser.LogEmail).SingleOrDefault();

            if(userToLog == null)
                ModelState.AddModelError("LogEmail", "Cannot find Email");
            else if (hasher.VerifyHashedPassword(logUser, userToLog.Password, logUser.LogPassword) == 0)
            {
                ModelState.AddModelError("LogPassword", "Wrong Password!!!!");
            }


            if(!ModelState.IsValid)
                return View("Index");

            HttpContext.Session.SetInt32("id", userToLog.id);
            return RedirectToAction("Dashboard", "Activity");
        }




        public IActionResult Error()
        {
            return View();
        }
    }
}
