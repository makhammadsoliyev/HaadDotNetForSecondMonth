using StudyTrackerSystem.Models;
using StudyTrackerSystem.Services;

CourseService courseService = new CourseService();
courseService.Add(new Course() { Name = "asasa", Credits = 1, Description="as", InstructorName="asaa", Schedule="sdsdsd" });
courseService.Update(1, new Course() { Name = "ASSAS", Credits = 1, Description="ASSA", InstructorName="ASAS", Schedule="sdsdsd" });
courseService.Delete(1);
Console.WriteLine();
