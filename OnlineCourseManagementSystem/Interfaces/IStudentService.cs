﻿using OnlineCourseManagementSystem.Models;

namespace OnlineCourseManagementSystem.Interfaces;

public interface IStudentService
{
    Student Add(Student student);
    Student GetById(int id);
    Student Update(int id, Student student);
    bool Delete(int id);
    List<Student> GetAll();
}
