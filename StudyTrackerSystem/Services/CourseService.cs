using StudyTrackerSystem.Configurations;
using StudyTrackerSystem.Interfaces;
using StudyTrackerSystem.Models;
using System.Text;

namespace StudyTrackerSystem.Services;

public class CourseService : ICourseService
{ 
    public Course Add(Course course)
    {
        var courses = GetAll();
        courses.Add(course);

        StringBuilder sb = new StringBuilder();
        foreach (var c in courses)
            sb.AppendLine($"{c.Id}|{c.Name}|{c.Description}|{c.InstructorName}|{c.Schedule}|{c.Credits}");

        File.WriteAllText(Constants.COURSES_PATH, sb.ToString());

        return course;
    }

    public bool Delete(int id)
    {
        var courses = GetAll();
        var course = courses.FirstOrDefault(c => c.Id == id)
            ?? throw new Exception("Course with this id was not found");

        StringBuilder sb = new StringBuilder();
        foreach (var c in courses)
        {
            if (c.Id == id)
                continue;

            sb.AppendLine($"{c.Id}|{c.Description}|{c.InstructorName}|{c.Schedule}|{c.Credits}");
        }

        File.WriteAllText(Constants.COURSES_PATH, sb.ToString());

        return true;
    }

    public List<Course> GetAll()
    {
        var data = File.ReadAllLines(Constants.COURSES_PATH);
        var courses = new List<Course>();

        foreach (var dataLine in data)
        {
            var courseData = dataLine.Split("|");
            var course = new Course()
            {
                Id = Convert.ToInt32(courseData[0]),
                Name = courseData[1],
                Description = courseData[2],
                InstructorName = courseData[3],
                Schedule = courseData[4],
                Credits = Convert.ToInt32(courseData[5])
            };
            courses.Add(course);
        }

        return courses;
    }

    public Course GetById(int id)
    {
        var courses = GetAll();
        var course = courses.FirstOrDefault(c => c.Id == id)
            ?? throw new Exception("Course with this id was not found");

        return course;
    }

    public Course Update(int id, Course course)
    {
        var courses = GetAll();
        var existCourse = courses.FirstOrDefault(c => c.Id == id)
            ?? throw new Exception("Course with this id was not found");

        existCourse.Id = id;
        existCourse.Name = course.Name;
        existCourse.Credits = course.Credits;
        existCourse.Schedule = course.Schedule;
        existCourse.Description = course.Description;
        existCourse.InstructorName = course.InstructorName;

        StringBuilder sb = new StringBuilder();
        foreach (var c in courses)
            sb.AppendLine($"{c.Id}|{c.Name}|{c.Description}|{c.InstructorName}|{c.Schedule}|{c.Credits}");

        File.WriteAllText(Constants.COURSES_PATH, sb.ToString());

        return existCourse;
    }
}
