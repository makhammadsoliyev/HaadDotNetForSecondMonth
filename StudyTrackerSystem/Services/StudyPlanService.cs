using StudyTrackerSystem.Configurations;
using StudyTrackerSystem.Enums;
using StudyTrackerSystem.Interfaces;
using StudyTrackerSystem.Models;
using System.Text;

namespace StudyTrackerSystem.Services;

public class StudyPlanService : IStudyPlanService
{
    private readonly CourseService courseService;
    private readonly StudentService studentService;

    public StudyPlanService(StudentService studentService, CourseService courseService)
    {
        this.courseService = courseService;
        this.studentService = studentService;
    }

    public StudyPlan Add(StudyPlan studyPlan)
    {
        var studyPlans = GetAll();
        var student = studentService.GetById(studyPlan.StudentId);
        var course = courseService.GetById(studyPlan.CourseId);

        studyPlans.Add(studyPlan);

        StringBuilder sb = new StringBuilder();
        foreach (var s in studyPlans)
            sb.AppendLine($"{s.Id}|{s.StudentId}|{s.CourseId}|{s.Materials}|{s.Topics}|{s.Goals}|{s.StudyHoursPerWeek}|{s.DayOff}|{s.PreferredStudyTime}");

        File.WriteAllText(Constants.STUDY_PLANS_PATH, sb.ToString());

        return studyPlan;
    }

    public bool Delete(int id)
    {
        var studyPlans = GetAll();
        var studyPlan = studyPlans.FirstOrDefault(s => s.Id == id)
            ?? throw new Exception("Study Plan with this id was not found");

        StringBuilder sb = new StringBuilder();

        foreach (var s in studyPlans)
        {
            if (s.Id == id)
                continue;

            sb.AppendLine($"{s.Id}|{s.StudentId}|{s.CourseId}|{s.Materials}|{s.Topics}|{s.Goals}|{s.StudyHoursPerWeek}|{s.DayOff}|{s.PreferredStudyTime}");
        }

        File.WriteAllText(Constants.STUDY_PLANS_PATH, sb.ToString());

        return true;
    }

    public List<StudyPlan> GetAll()
    {
        var data = File.ReadAllLines(Constants.STUDY_PLANS_PATH);
        var studyPlans = new List<StudyPlan>();

        foreach (var dataLine in data)
        {
            var studyPlanData = dataLine.Split("|");
            var studyPlan = new StudyPlan()
            {
                Id = Convert.ToInt32(studyPlanData[0]),
                StudentId = Convert.ToInt32(studyPlanData[1]),
                CourseId = Convert.ToInt32(studyPlanData[2]),
                Materials = studyPlanData[3],
                Topics = studyPlanData[4],
                Goals = studyPlanData[5],
                StudyHoursPerWeek = Convert.ToInt32(studyPlanData[6]),
                DayOff = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), studyPlanData[7]),
                PreferredStudyTime = (PartOfDays)Enum.Parse(typeof(PartOfDays), studyPlanData[8])
            };

            studyPlans.Add(studyPlan);
        }

        return studyPlans;
    }

    public StudyPlan GetById(int id)
    {
        var studyPlans = GetAll();
        var studyPlan = studyPlans.FirstOrDefault(s => s.Id == id)
            ?? throw new Exception("StudyPlan with this id was not found");

        return studyPlan;
    }

    public List<StudyPlan> GetStudyPlansByCourse(int courseId)
    {
        var course = courseService.GetById(courseId);
        var studyPlans = GetAll();
        var result = studyPlans.FindAll(s => s.CourseId == courseId);

        return result;
    }

    public List<StudyPlan> GetStudyPlansByStudent(int studentId)
    {
        var student = studentService.GetById(studentId);
        var studyPlans = GetAll();
        var result = studyPlans.FindAll(s => s.StudentId == studentId);

        return result;
    }

    public StudyPlan Update(int id, StudyPlan studyPlan)
    {
        var studyPlans = GetAll();
        var existStudyPlan = studyPlans.FirstOrDefault(s => s.Id == id)
            ?? throw new Exception("StudyPlan with this id was not found");
        var student = studentService.GetById(studyPlan.StudentId);
        var course = courseService.GetById(studyPlan.CourseId);

        existStudyPlan.Id = id;
        existStudyPlan.Goals = studyPlan.Goals;
        existStudyPlan.DayOff = studyPlan.DayOff;
        existStudyPlan.Topics = studyPlan.Topics;
        existStudyPlan.CourseId = studyPlan.CourseId;
        existStudyPlan.StudentId = studyPlan.StudentId;
        existStudyPlan.Materials = studyPlan.Materials;
        existStudyPlan.StudyHoursPerWeek = studyPlan.StudyHoursPerWeek;
        existStudyPlan.PreferredStudyTime = studyPlan.PreferredStudyTime;

        StringBuilder sb = new StringBuilder();
        foreach (var s in studyPlans)
            sb.AppendLine($"{s.Id}|{s.StudentId}|{s.CourseId}|{s.Materials}|{s.Topics}|{s.Goals}|{s.StudyHoursPerWeek}|{s.DayOff}|{s.PreferredStudyTime}");

        File.WriteAllText(Constants.STUDY_PLANS_PATH, sb.ToString());

        return existStudyPlan;
    }
}
