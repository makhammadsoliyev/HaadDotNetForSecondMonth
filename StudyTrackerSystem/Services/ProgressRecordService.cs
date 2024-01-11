using StudyTrackerSystem.Configurations;
using StudyTrackerSystem.Interfaces;
using StudyTrackerSystem.Models;
using System.Text;

namespace StudyTrackerSystem.Services;

public class ProgressRecordService : IProgressRecordService
{
    private readonly CourseService courseService;
    private readonly StudentService studentService;

    public ProgressRecordService(StudentService studentService, CourseService courseService)
    {
        this.courseService = courseService;
        this.studentService = studentService;
    }

    public ProgressRecord Add(ProgressRecord record)
    {
        var progressRecords = GetAll();
        var student = studentService.GetById(record.StudentId);
        var course = courseService.GetById(record.CourseId);

        progressRecords.Add(record);

        StringBuilder sb = new StringBuilder();
        foreach (var p in progressRecords)
            sb.AppendLine($"{p.Id}|{p.StudentId}|{p.CourseId}|{p.ProgressDetails}");

        File.WriteAllText(Constants.PROGRESS_RECORDS_PATH, sb.ToString());

        return record;
    }

    public bool Delete(int id)
    {
        var progressRecords = GetAll();
        var progressRecord = progressRecords.FirstOrDefault(p => p.Id == id)
            ?? throw new Exception("Progress Record with this id was not found");

        StringBuilder sb = new StringBuilder();
        foreach (var p in progressRecords)
        {
            if (p.Id == id)
                continue;

            sb.AppendLine($"{p.Id}|{p.StudentId}|{p.CourseId}|{p.ProgressDetails}");
        }

        File.WriteAllText(Constants.PROGRESS_RECORDS_PATH, sb.ToString());
        
        return true;
    }

    public List<ProgressRecord> GetAll()
    {
        var data = File.ReadAllLines(Constants.PROGRESS_RECORDS_PATH);

        var progressRecords = new List<ProgressRecord>();

        foreach (var dataLine in data)
        {
            var recordData = dataLine.Split("|");
            var progressRecord = new ProgressRecord()
            {
                Id = Convert.ToInt32(recordData[0]),
                StudentId = Convert.ToInt32(recordData[1]),
                CourseId = Convert.ToInt32(recordData[2]),
                ProgressDetails = recordData[3]
            };

            progressRecords.Add(progressRecord);
        }

        return progressRecords;
    }

    public ProgressRecord GetById(int id)
    {
        var progressRecords = GetAll();
        var progressRecord = progressRecords.FirstOrDefault(p => p.Id == id)
            ?? throw new Exception("Progress Record with this id was not found");

        return progressRecord;
    }

    public List<ProgressRecord> GetProgressRecordsByCourse(int courseId)
    {
        var course = courseService.GetById(courseId);
        var progressRecords = GetAll();
        var result = progressRecords.FindAll(p => p.CourseId == courseId);

        return result;
    }

    public List<ProgressRecord> GetProgressRecordsByStudent(int studentId)
    {
        var student = studentService.GetById(studentId);
        var progressRecords = GetAll();
        var result = progressRecords.FindAll(p => p.StudentId == studentId);

        return result;
    }

    public ProgressRecord Update(int id, ProgressRecord record)
    {
        var progressRecords = GetAll();
        var progressRecord = progressRecords.FirstOrDefault(p => p.Id == id)
            ?? throw new Exception("Progress Record with this id was not found");
        var student = studentService.GetById(record.StudentId);
        var course = courseService.GetById(record.CourseId);

        progressRecord.Id = id;
        progressRecord.CourseId = record.CourseId;
        progressRecord.StudentId = record.StudentId;
        progressRecord.ProgressDetails = record.ProgressDetails;

        StringBuilder sb = new StringBuilder();
        foreach (var p in progressRecords)
            sb.AppendLine($"{p.Id}|{p.StudentId}|{p.CourseId}|{p.ProgressDetails}");

        File.WriteAllText(Constants.PROGRESS_RECORDS_PATH, sb.ToString());

        return progressRecord;
    }
}
