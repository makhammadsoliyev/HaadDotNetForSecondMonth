using Spectre.Console;
using StudyTrackerSystem.Interfaces;
using StudyTrackerSystem.Models;
using StudyTrackerSystem.Services;

namespace StudyTrackerSystem.Display;

public class MainMenu
{
    private readonly CourseService courseService;
    private readonly StudentService studentService;
    private readonly StudyPlanService studyPlanService;
    private readonly StudySessionService studySessionService;
    private readonly ProgressRecordService progressRecordService;

    private readonly CourseMenu courseMenu;
    private readonly StudentMenu studentMenu;
    private readonly StudyPlanMenu studyPlanMenu;
    private readonly StudySessionMenu studySessionMenu;
    private readonly ProgressRecordMenu progressRecordMenu;

    public MainMenu()
    {
        courseService = new CourseService();
        studentService = new StudentService();
        studyPlanService = new StudyPlanService(studentService, courseService);
        studySessionService = new StudySessionService(courseService, studentService);
        progressRecordService = new ProgressRecordService(studentService, courseService);

        courseMenu = new CourseMenu(courseService);
        studentMenu = new StudentMenu(studentService);
        studyPlanMenu = new StudyPlanMenu(studyPlanService);
        studySessionMenu = new StudySessionMenu(studySessionService);
        progressRecordMenu = new ProgressRecordMenu(progressRecordService);
    }

    public void Main()
    {
        var circle = true;
        var selectionDisplay = new SelectionMenu();

        while (circle)
        {
            AnsiConsole.Clear();
            var selection = selectionDisplay.ShowSelectionMenu("Choose one of options", new string[] { "Course", "Student", "StudyPlan", "StudySession", "ProgressRecord", "Exit" });

            switch (selection)
            {
                case "Course":
                    courseMenu.Display();
                    break;
                case "Student":
                    studentMenu.Display();
                    break;
                case "StudyPlan":
                    studyPlanMenu.Display();
                    break;
                case "StudySession":
                    studySessionMenu.Display();
                    break;
                case "ProgressRecord":
                    progressRecordMenu.Display();
                    break;
                case "Exit":
                    circle = false;
                    break;
            }
        }
    }
}
