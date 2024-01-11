using StudyTrackerSystem.Configurations;
using StudyTrackerSystem.Interfaces;
using StudyTrackerSystem.Models;
using System.Text;

namespace StudyTrackerSystem.Services;

public class StudySessionService : IStudySessionService
{
    private readonly CourseService courseService;
    private readonly StudentService studentService;

    public StudySessionService(CourseService courseService, StudentService studentService)
    {
        this.courseService = courseService;
        this.studentService = studentService;
    }

    public StudySession Add(StudySession session)
    {
        var studySessions = GetAll();
        
        var student = studentService.GetById(session.StudentId);
        var course = courseService.GetById(session.CourseId);

        studySessions.Add(session);

        StringBuilder sb = new StringBuilder();

        foreach (var s in studySessions)
            sb.AppendLine($"{s.Id}|{s.StudentId}|{s.CourseId}|{s.StartDate}|{s.DurationInMinutes}|{s.StudyMaterialsCovered}|{s.Notes}");

        File.WriteAllText(Constants.STUDY_SESSIONS_PATH, sb.ToString());

        return session;
    }

    public bool Delete(int id)
    {
        var studySessions = GetAll();
        var studySession = studySessions.FirstOrDefault(s => s.Id == id)
            ?? throw new Exception("Session with this id was not found");

        StringBuilder sb = new StringBuilder();

        foreach (var s in studySessions)
        {
            if (s.Id == id)
                continue;

            sb.AppendLine($"{s.Id}|{s.StudentId}|{s.CourseId}|{s.StartDate}|{s.DurationInMinutes}|{s.StudyMaterialsCovered}|{s.Notes}");
        }

        File.WriteAllText(Constants.STUDY_SESSIONS_PATH, sb.ToString());

        return true;
    }

    public List<StudySession> GetAll()
    {
        var data = File.ReadAllLines(Constants.STUDY_SESSIONS_PATH);
        var studySessions = new List<StudySession>();

        foreach (var dataLine in data)
        {
            var sessionData = dataLine.Split("|");
            var studySession = new StudySession()
            {
                Id = Convert.ToInt32(sessionData[0]),
                StudentId = Convert.ToInt32(sessionData[1]),
                CourseId = Convert.ToInt32(sessionData[2]),
                StartDate = DateOnly.Parse(sessionData[3]),
                DurationInMinutes = Convert.ToInt32(sessionData[4]),
                StudyMaterialsCovered = sessionData[5],
                Notes = sessionData[6]
            };
            
            studySessions.Add(studySession);
        }

        return studySessions;
    }

    public List<StudySession> GetStudySessionsByCourse(int courseId)
    {
        var course = courseService.GetById(courseId);
        var studySessions = GetAll();
        var result = studySessions.FindAll(s => s.CourseId == courseId);

        return result;
    }

    public List<StudySession> GetStudySessionsByStudent(int studentId)
    {
        var student = studentService.GetById(studentId);
        var studySessions = GetAll();
        var result = studySessions.FindAll(s => s.StudentId == studentId);

        return result;
    }

    public StudySession GetById(int id)
    {
        var studySessions = GetAll();
        var studySession = studySessions.FirstOrDefault(s => s.Id == id)
            ?? throw new Exception("Session with this id was not found");

        return studySession;

    }

    public List<StudySession> GetUpcomingStudySession()
    {
        var sessions = GetAll();
        var result = sessions.OrderBy(s => s.StartDate).ToList();

        return result;
    }

    public StudySession Update(int id, StudySession session)
    {
        var studySessions = GetAll();
        var studySession = studySessions.FirstOrDefault(s => s.Id == id)
            ?? throw new Exception("Session with this id was not found");

        var student = studentService.GetById(session.StudentId);
        var course = courseService.GetById(session.CourseId);

        studySession.Id = id;
        studySession.Notes = session.Notes;
        studySession.CourseId = session.CourseId;
        studySession.StartDate = session.StartDate;
        studySession.StudentId = session.StudentId;
        studySession.DurationInMinutes = session.DurationInMinutes;
        studySession.StudyMaterialsCovered = session.StudyMaterialsCovered;

        StringBuilder sb = new StringBuilder();

        foreach (var s in studySessions)
            sb.AppendLine($"{s.Id}|{s.StudentId}|{s.CourseId}|{s.StartDate}|{s.DurationInMinutes}|{s.StudyMaterialsCovered}|{s.Notes}");   
        
        File.WriteAllText(Constants.STUDY_SESSIONS_PATH, sb.ToString());

        return studySession;
    }
}
