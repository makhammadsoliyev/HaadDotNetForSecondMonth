using OnlineCourseManagementSystem.Interfaces;
using OnlineCourseManagementSystem.Models;

namespace OnlineCourseManagementSystem.Services;

public class StudentService : IStudentService
{
    private readonly List<Student> students;

    public StudentService()
    {
        this.students = new List<Student>();
    }

    public Student Add(Student student)
    {
        var existStudent = students.FirstOrDefault(s => s.Id == student.Id);
        if (existStudent is not null)
            throw new Exception("Student with this phone already exists");

        students.Add(student);
        return student;
    }

    public bool Delete(int id)
    {
        var student = students.FirstOrDefault(s => s.Id == id)
            ?? throw new Exception("Student with this id was not found");

        return students.Remove(student);
    }

    public List<Student> GetAll()
        => students;

    public Student GetById(int id)
    {
        var student = students.FirstOrDefault(s => s.Id == id)
            ?? throw new Exception("Student with this id was not found");

        return student;
    }

    public Student Update(int id, Student student)
    {
        var existStudent = students.FirstOrDefault(s => s.Id == id)
            ?? throw new Exception("Student with this id was not found");

        existStudent.Id = id;
        existStudent.Phone = student.Phone;
        existStudent.LastName = student.LastName;
        existStudent.FirstName = student.FirstName;

        return existStudent;
    }
}
