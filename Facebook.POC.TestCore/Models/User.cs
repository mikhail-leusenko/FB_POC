﻿namespace Facebook.POC.TestCore.Models
{
    public class User
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string UserId { get; set; }

        public User(string firstName, string lastName, string email, string password, string userId)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.Password = password;
            this.UserId = userId;
        }
    }
}