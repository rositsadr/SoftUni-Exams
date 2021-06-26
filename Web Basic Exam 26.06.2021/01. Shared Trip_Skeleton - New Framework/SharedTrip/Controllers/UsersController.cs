﻿using MyWebServer.Controllers;
using MyWebServer.Http;
using SharedTrip.Data;
using SharedTrip.Data.Models;
using SharedTrip.Models;
using SharedTrip.Models.User;
using SharedTrip.Services;
using System.Linq;

namespace SharedTrip.Controllers
{
    public class UsersController : Controller
    {
        private readonly IValidator validator;
        private readonly IPasswordHasher passwordHasher;
        private readonly ApplicationDbContext data;

        public UsersController(
            IValidator validator,
            IPasswordHasher passwordHasher,
            ApplicationDbContext data)
        {
            this.validator = validator;
            this.passwordHasher = passwordHasher;
            this.data = data;
        }

        public HttpResponse Register()
        {
            return View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterUserModel model)
        {
            var errors = this.validator.ValidateUserRegistration(model);

           if (this.data.Users.Any(u => u.Username == model.Username))
           {
                errors.Add($"User with '{model.Username}' username already exists.");
           }

           if (this.data.Users.Any(u => u.Email == model.Email))
           {
               errors.Add($"User with '{model.Email}' e-mail already exists.");
           }

           if (errors.Any())
           {
                //return Error(errors);
                return Redirect("/Users/Register");
           }

           var user = new User
            {
               Username = model.Username,
               Password = this.passwordHasher.HashPassword(model.Password),
               Email = model.Email,
           };

           data.Users.Add(user);

           data.SaveChanges();

            return Redirect("/Users/Login");
        }

        public HttpResponse Login()
        {
            return View();
        }

        [HttpPost]
        public HttpResponse Login(LoginUserModel model)
        {
            var hashedPassword = this.passwordHasher.HashPassword(model.Password);

            var userId = this.data
                .Users
                .Where(u => u.Username == model.Username && u.Password == hashedPassword)
                .Select(u => u.Id)
                .FirstOrDefault();

            if (userId == null)
            {
                //return Error("Username and password combination is not valid.");
                return Redirect("/Users/Login");
            }

            this.SignIn(userId);

            return Redirect("/Trips/All");
        }

        [Authorize]
        public HttpResponse Logout()
        {
            this.SignOut();

            return Redirect("/");
        }
    }
}
