using LINQ_Avance.NET.Data;
using LINQ_Avance.NET.Models;
using Microsoft.EntityFrameworkCore;

namespace LINQ_Avance.NET
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var context = new SchoolDbContext())
            {

                ////Skapa de första data
                ////ID skapas automatisk av databasen

                //var teacher1 = new Teacher { Name = "Anas Smith" };
                //var teacher2 = new Teacher { Name = "Tobias Johnson" };
                //var teacher3 = new Teacher { Name = "Reidar Rider" };


                //var subject1 = new Subject { Name = "C#", Teacher = teacher1 };
                //var subject2 = new Subject { Name = "SQL", Teacher = teacher2 };
                //var subject3 = new Subject { Name = "Math", Teacher = teacher3 };


                //var sut21 = new Course { Name = "SUT21", Teachers = new List<Teacher> { teacher1, teacher3 } };
                //var sut22 = new Course { Name = "SUT22", Teachers = new List<Teacher> { teacher2, teacher3 } };


                //var student1 = new Student { Name = "Tim Nilsson", Courses = new List<Course> { sut21 } };
                //var student2 = new Student { Name = "Sarah Johansson", Courses = new List<Course> { sut22 } };
                //var student3 = new Student { Name = "Sussie Lindfelt", Courses = new List<Course> { sut21 } };
                //var student4 = new Student { Name = "James Bond", Courses = new List<Course> { sut22 } };
                //var student5 = new Student { Name = "Will Smith", Courses = new List<Course> { sut21 } };

                ////AddRange för att lägga till i lista

                //context.Teachers.AddRange(new[] { teacher1, teacher2, teacher3 });
                //context.Subjects.AddRange(new[] { subject1, subject2, subject3 });
                //context.Courses.AddRange(new[] { sut21, sut22 });
                //context.Students.AddRange(new[] { student1, student2, student3, student4, student5 });

                //context.SaveChanges();

                ////-------------------Remove All-------------
                //context.Students.RemoveRange(context.Students);
                //context.Courses.RemoveRange(context.Courses);
                //context.Subjects.RemoveRange(context.Subjects);
                //context.Teachers.RemoveRange(context.Teachers);
                //context.SaveChanges();




                int choice = 0;
                while (choice != 6)
                {
                    Console.WriteLine("Welcome to the School System:");
                    Console.WriteLine("-------------------------------");
                    Console.WriteLine("1. Find Math Teacher");
                    Console.WriteLine("2. Find Students and Their Teachers");
                    Console.WriteLine("3. Find existing subject in Database");
                    Console.WriteLine("4. Change Subject Name");
                    Console.WriteLine("5. Rename a Student's Teacher");
                    Console.WriteLine("6. Quit");

                    Console.Write("Enter your choice: ");
                    string input = Console.ReadLine();

                    if (int.TryParse(input, out choice))
                    {
                        switch (choice)
                        {
                            case 1:
                                Console.WriteLine("You chose to find a Math teacher.");
                                //-----------Find Math teacher by LINQ-------------
                                Console.ReadKey();
                                Console.Clear();
                                Console.WriteLine("Loading data, please wait...");
                                var mathTeachers = context.Subjects
                                .Where(s => s.Name == "Math")
                                .Select(s => s.Teacher)
                                .ToList();
                                Console.Clear();
                                Console.WriteLine("Data loaded successfully!");
                                foreach (var T1 in mathTeachers)
                                {
                                    Console.WriteLine($"Teacher: {T1.Name}");
                                }
                                Console.ReadKey();
                                Console.Clear();

                                break;
                            case 2:
                                Console.WriteLine("You chose to find students and their teachers.");
                                //------Getting all students and teacher they have with LINQ-------
                                Console.ReadKey();
                                Console.Clear();
                                Console.WriteLine("Loading data, please wait...");
                                var students = context.Students.Include(s => s.Courses).ThenInclude(c => c.Teachers);
                                Console.WriteLine("Data loaded successfully!");
                                Console.Clear();

                                // Print table header
                                Console.WriteLine("--------------------------------------------------------------");
                                Console.WriteLine("| Student ID  | Student Name    | Course Name | Teacher         |");
                                Console.WriteLine("--------------------------------------------------------------");

                                foreach (var student1 in students)
                                {
                                    foreach (var course in student1.Courses)
                                    {
                                        // Print row for each teacher for the course
                                        foreach (var teacher1 in course.Teachers)
                                        {

                                            // -11 is the maximum space between colum 
                                            Console.WriteLine($"| {student1.ID,-11} | {student1.Name,-15} | {course.Name,-11} | {teacher1.Name,-15} |");
                                        }
                                    }
                                }
                                Console.WriteLine("--------------------------------------------------------------");

                                Console.ReadKey();
                                Console.Clear();

                                break;
                            case 3:
                                Console.WriteLine("You chose search subject");
                                //Checking if Subject SQL exist with LINQ
                                Console.ReadKey();
                                Console.Clear();
                                Console.WriteLine("Please type the subject you want search for");
                                string searchTerm = Console.ReadLine();
                                Console.WriteLine("Loading data, please wait...");
                                var subjects = context.Subjects.Where(s => s.Name.Contains(searchTerm)).ToList();
                                Console.WriteLine("Data loaded successfully!");
                                Console.Clear();

                                //Any return bool, if subject "SQL" exist/contain in Subject then return true 
                                if (subjects.Any())
                                {
                                    Console.WriteLine("SQL exists.");
                                }
                                else 
                                {
                                    Console.WriteLine($"{searchTerm} do not exsist");
                                 }
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            case 4:
                                Console.WriteLine("You chose to change a subject name.");
                                //-------------Change subject name--------------------------
                                Console.ReadKey();
                                Console.Clear();
                                Console.WriteLine("Type in the subject you want to rename ");
                                string oldName = Console.ReadLine();
                                string newName = null;
                                Console.WriteLine("Loading data, please wait...");
                                // Retrieve the subject object from the database
                                var subject = context.Subjects.FirstOrDefault(s => s.Name == oldName);
                                Console.WriteLine("Data loaded successfully!");
                                if (subject != null)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Subject found what do you want to rename it to ?");
                                    newName = Console.ReadLine();
                                    // Update the name property
                                    subject.Name = newName;
                                    // Save changes to the database
                                    context.SaveChanges();
                                    Console.WriteLine("DONE");
                                }
                                else
                                {
                                    Console.WriteLine($"{oldName} do not exist.");
                                }
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            case 5:
                                Console.Clear();
                                Console.WriteLine("You chose to rename a student's teacher.");
                                Console.ReadKey();
                                Console.Clear();
                                // get the student by name
                                Console.WriteLine("Type in students name to find the teachers");
                                string studentName = Console.ReadLine();
                                Console.Clear();
                                Console.WriteLine("Loading data, please wait...");
                                var student = context.Students
                                    .Include(s => s.Courses).ThenInclude(c => c.Teachers)
                                    .FirstOrDefault(s => s.Name == studentName);
                                Console.WriteLine("Data loaded successfully!");

                                if (student != null)
                                {
                                    Console.Clear();
                                    // print the student's name
                                    Console.WriteLine($"Student Name: {student.Name}");

                                    // loop through the student's courses and teachers
                                    foreach (var course in student.Courses)
                                    {
                                        Console.WriteLine($"Teacher(s) for {course.Name}:");
                                        int index = 0;
                                        foreach (var teacher in course.Teachers)
                                        {
                                            Console.WriteLine($"[{index}] - {teacher.Name}");
                                            index++;
                                        }
                                    }

                                    // prompt the user to choose a teacher by index
                                    Console.WriteLine("Enter the index number of the teacher you want to change:");
                                    int teacherIndex = int.Parse(Console.ReadLine());
                                    Console.Clear() ;
                                    // change the name of the selected teacher
                                    var teacherToChange = student.Courses.FirstOrDefault()?.Teachers.ElementAt(teacherIndex);
                                    if (teacherToChange != null)
                                    {
                                        Console.WriteLine("What is the name of the new teacher ?");
                                        string newTeacherName = Console.ReadLine();
                                        teacherToChange.Name = newTeacherName;
                                        context.SaveChanges();
                                        Console.Clear();
                                        Console.WriteLine($"Teacher name changed to {teacherToChange.Name}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Student not found");
                                }

                                Console.ReadKey();
                                Console.Clear();
                                 break;
                            case 6:
                                Console.WriteLine("Goodbye!");
                                break;
                            default:
                                Console.WriteLine("Invalid choice. Please try again.");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a number.");
                    }

                    Console.WriteLine();
                }



































              



               


               







               


               




            }
        }
    }
}