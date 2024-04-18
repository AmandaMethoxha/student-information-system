using System;
namespace Lidhja
{
	public class Student
	{
	
        
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string StudentId { get; set; }
            public DateTime DateOfBirth { get; set; }
            public string Gender { get; set; }
            public string Email { get; set; }
            public string MobilePhone { get; set; }
            public string Address { get; set; }
            public string Nationality { get; set; }
            public string Country { get; set; }
            public string LevelOfStudy { get; set; }
            public string ProgramOfStudy { get; set; }
            public string FullName => $"{FirstName} {LastName}";
        

    }
}
