using OnlineCourseManagementSystem.Services;
using Spectre.Console;

namespace OnlineCourseManagementSystem.Display;

public class MainMenu
{
    private readonly CourseService courseService;
    private readonly StudentService studentService;
    private readonly EnrollmentService enrollmentService;
    private readonly InstructorService instructorService;

    private readonly CourseMenu courseMenu;
    private readonly StudentMenu studentMenu;
    private readonly EnrollmentMenu enrollmentMenu;
    private readonly InstructorMenu instructorMenu;

    public MainMenu()
    {
        this.courseService = new CourseService();
        this.studentService = new StudentService();
        this.enrollmentService = new EnrollmentService(courseService, studentService);
        this.instructorService = new InstructorService(studentService, courseService);

        this.studentMenu = new StudentMenu(studentService);
        this.enrollmentMenu = new EnrollmentMenu(enrollmentService);
        this.instructorMenu = new InstructorMenu(instructorService);
        this.courseMenu = new CourseMenu(courseService, instructorService);

    }

    public void Main()
    {
        var circle = true;
        var selectionDisplay = new SelectionMenu();

        while (circle)
        {
            AnsiConsole.Clear();
            var selection = selectionDisplay.ShowSelectionMenu("Choose one of options", new string[] { "Student", "Instructor", "Course", "Enrollment", "Exit" });

            switch (selection)
            {
                case "Student":
                    studentMenu.Display();
                    break;
                case "Instructor":
                    instructorMenu.Display();
                    break;
                case "Course":
                    courseMenu.Display();
                    break;
                case "Enrollment":
                    enrollmentMenu.Display();
                    break;
                case "Exit":
                    circle = false;
                    break;
            }
        }
    }
}
