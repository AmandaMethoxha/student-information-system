using System;

namespace Lidhja
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; } 
        public UserRole Role { get; set; }
    }

    public enum UserRole
    {
        Admin,
        Teacher,
        Student
    }
}
