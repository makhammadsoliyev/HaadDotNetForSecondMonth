using StudyTrackerSystem.Configurations;
using StudyTrackerSystem.Interfaces;
using StudyTrackerSystem.Models;
using System.Text;

namespace StudyTrackerSystem.Services;

public class StudentService : IStudentService
{
    public Student Add(Student student)
    {
        var students = GetAll();
        var existStudent = students.FirstOrDefault(s => s.Email.Equals(student.Email));

        if (existStudent is not null)
            throw new Exception("Student with this email already exists");

        students.Add(student);
        StringBuilder sb = new StringBuilder();

        foreach (var s in students)
            sb.AppendLine($"{s.Id}|{s.FirstName}|{s.LastName}|{s.Email}|{s.Password}");   

        File.WriteAllText(Constants.STUDENTS_PATH, sb.ToString());

        return student;
    } 

    public bool Delete(int id)
    {
        var students = GetAll();
        var student = students.FirstOrDefault(s => s.Id == id)
            ?? throw new Exception("Student with this id was not found");

        StringBuilder sb = new StringBuilder();

        foreach (var s in students)
        {
            if (s.Id == id)
                continue;

            sb.AppendLine($"{s.Id}|{s.FirstName}|{s.LastName}|{s.Email}|{s.Password}");
        }

        File.WriteAllText(Constants.STUDENTS_PATH, sb.ToString());

        return true;
    }

    public List<Student> GetAll()
    {
        var data = File.ReadAllLines(Constants.STUDENTS_PATH);
        var students = new List<Student>();

        foreach (var dataLine in data)
        {
            var studentData = dataLine.Split("|");
            var student = new Student()
            {
                Id = Convert.ToInt32(studentData[0]),
                FirstName = studentData[1],
                LastName = studentData[2],
                Email = studentData[3],
                Password = studentData[4]
            };
            students.Add(student);
        }

        return students;
    }

    public Student GetById(int id)
    {
        var students = GetAll();
        var student = students.FirstOrDefault(s => s.Id == id)
            ?? throw new Exception("Student with this id was not found");

        return student;
    }

    public Student Update(int id, Student student)
    {
        var students = GetAll();
        var existStudent = students.FirstOrDefault(s => s.Id == id)
            ?? throw new Exception("Student with this id was not found");

        existStudent.Id = id;
        existStudent.Email = student.Email;
        existStudent.Password = student.Password;
        existStudent.LastName = student.LastName;
        existStudent.FirstName = student.FirstName;

        StringBuilder sb = new StringBuilder();

        foreach (var s in students)
            sb.AppendLine($"{s.Id}|{s.FirstName}|{s.LastName}|{s.Email}|{s.Password}");

        File.WriteAllText(Constants.STUDENTS_PATH, sb.ToString());

        return existStudent;
    } 
}
