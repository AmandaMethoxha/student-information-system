using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Lidhja
{
    public static class DataManager
    {
        // Add a new student to the database
        public static void AddStudent(Student student)
        {
            using (var context = new DatabaseContext())
            {
                context.Students.Add(student);
                context.SaveChanges();
            }
        }

        // Asynchronous method to retrieve all students
        public static async Task<List<Student>> GetAllStudentsAsync()
        {
            using (var context = new DatabaseContext())
            {
                return await context.Students.ToListAsync();
            }
        }

        // Retrieve a single student by their ID
        public static Student GetStudentById(string studentId)
        {
            using (var context = new DatabaseContext())
            {
                return context.Students.FirstOrDefault(s => s.StudentId == studentId);
            }
        }

        // Delete a student from the database by ID
        public static void DeleteStudent(string studentId)
        {
            using (var context = new DatabaseContext())
            {
                var student = context.Students.Find(studentId);
                if (student != null)
                {
                    context.Students.Remove(student);
                    context.SaveChanges();
                }
            }
        }

        // Update an existing student's details in the database
        public static bool UpdateStudent(Student updatedStudent)
        {
            try
            {
                using (var context = new DatabaseContext())
                {
                    var student = context.Students.FirstOrDefault(s => s.StudentId == updatedStudent.StudentId);
                    if (student != null)
                    {
                        // Update properties
                        student.FirstName = updatedStudent.FirstName;
                        student.LastName = updatedStudent.LastName;
                        student.DateOfBirth = updatedStudent.DateOfBirth;
                        student.Gender = updatedStudent.Gender;
                        student.Email = updatedStudent.Email;
                        student.MobilePhone = updatedStudent.MobilePhone;
                        student.Address = updatedStudent.Address;
                        student.Nationality = updatedStudent.Nationality;
                        student.Country = updatedStudent.Country;
                        student.LevelOfStudy = updatedStudent.LevelOfStudy;
                        student.ProgramOfStudy = updatedStudent.ProgramOfStudy;

                        context.SaveChanges();
                        Console.WriteLine("Student updated successfully.");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Student not found.");
                        return false; // Student not found
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return false;
            }
        }

        // Add a new student progress to the database
        public static void AddStudentProgress(StudentProgress progress)
        {
            using (var context = new DatabaseContext())
            {
                context.StudentProgresses.Add(progress);
                context.SaveChanges();
            }
        }

        // Student log in validation
        public static bool ValidateStudentLogin(string studentId, string email)
        {
            using (var context = new DatabaseContext())
            {
                var student = context.Students.FirstOrDefault(s => s.StudentId == studentId && s.Email == email);
                return student != null;
            }
        }

        // Retrieve a single student's progress by their ID
        public static IEnumerable<StudentProgress> GetStudentProgress(string studentId)
        {
            using (var context = new DatabaseContext())
            {
                return context.StudentProgresses
                              .Where(sp => sp.StudentId == studentId)
                              .ToList();
            }
        }

        // Additional methods for statistics
        public static string GetGenderStatistics()
        {
            using (var context = new DatabaseContext())
            {
                var maleCount = context.Students.Count(s => s.Gender == "Male");
                var femaleCount = context.Students.Count(s => s.Gender == "Female");
                var otherCount = context.Students.Count(s => s.Gender == "Other");
                return $"Males: {maleCount}, Females: {femaleCount}, Others: {otherCount}";
            }
        }

        public static string GetNationalityStatistics()
        {
            using (var context = new DatabaseContext())
            {
                var nationalCount = context.Students.Count(s => s.Nationality == "National");
                var internationalCount = context.Students.Count(s => s.Nationality == "International");
                return $"Nationals: {nationalCount}, Internationals: {internationalCount}";
            }
        }

        public static string GetProgramOfStudyStatistics()
        {
            using (var context = new DatabaseContext())
            {
                var computerScienceCount = context.Students.Count(s => s.ProgramOfStudy == "Computer Science");
                var cyberSecurityCount = context.Students.Count(s => s.ProgramOfStudy == "Cyber Security");
                var gamesProgrammingCount = context.Students.Count(s => s.ProgramOfStudy == "Games Programming");
                var creativeComputingCount = context.Students.Count(s => s.ProgramOfStudy == "Creative Computing");
                return $"Computer Science: {computerScienceCount}, Cyber Security: {cyberSecurityCount}, Games Programming: {gamesProgrammingCount}, Creative Computing: {creativeComputingCount}";
            }
        }

        public static string GetLevelOfStudyStatistics()
        {
            using (var context = new DatabaseContext())
            {
                var undergraduateCount = context.Students.Count(s => s.LevelOfStudy == "Undergraduate");
                var graduateCount = context.Students.Count(s => s.LevelOfStudy == "Graduate");
                var postgraduateCount = context.Students.Count(s => s.LevelOfStudy == "Postgraduate");
                return $"Undergraduate: {undergraduateCount}, Graduate: {graduateCount}, Postgraduate: {postgraduateCount}";
            }
        }






        // Method to get combined student progress
        public static IEnumerable<StudentProgressViewModel> GetCombinedStudentProgress()
        {
            using (var context = new DatabaseContext())
            {
                var query = from sp in context.StudentProgresses
                            join s in context.Students on sp.StudentId equals s.StudentId into studentDetails
                            from s in studentDetails.DefaultIfEmpty()
                            select new StudentProgressViewModel
                            {
                                Id = sp.Id,
                                StudentId = sp.StudentId,
                                FullName = sp.FullName,
                                Course = sp.Course,
                                Grade = sp.Grade,
                                Notes = sp.Notes,
                                LevelOfStudy = s != null ? s.LevelOfStudy : null,
                                ProgramOfStudy = s != null ? s.ProgramOfStudy : null,
                                Email = s != null ? s.Email : null
                            };
                return query.ToList();
            }
        }

        // Update student progress
        public static bool UpdateStudentProgress(StudentProgress updatedProgress)
        {
            try
            {
                using (var context = new DatabaseContext())
                {
                    var progress = context.StudentProgresses.FirstOrDefault(sp => sp.Id == updatedProgress.Id);
                    if (progress != null)
                    {
                        progress.FullName = updatedProgress.FullName;
                        progress.Course = updatedProgress.Course;
                        progress.Grade = updatedProgress.Grade;
                        progress.Notes = updatedProgress.Notes;

                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false; // Progress not found
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return false;
            }
        }

        public static void DeleteStudentProgress(int progressId)
        {
            using (var context = new DatabaseContext())
            {
                var progress = context.StudentProgresses.Find(progressId);
                if (progress != null)
                {
                    context.StudentProgresses.Remove(progress);
                    context.SaveChanges();
                }
                else
                {
                    throw new InvalidOperationException("Student progress not found.");
                }
            }
        }
    }


    // ViewModel class combining Student and StudentProgress information // shtim i ri
    public class StudentProgressViewModel
    {
        public int Id { get; set; } // This should map to the Id of StudentProgres
        public string StudentId { get; set; }
        public string FullName { get; set; }
        public string Course { get; set; }
        public string Grade { get; set; }
        public string Notes { get; set; }
        public string LevelOfStudy { get; set; }
        public string ProgramOfStudy { get; set; }
        public string Email { get; set; }
    }

}
