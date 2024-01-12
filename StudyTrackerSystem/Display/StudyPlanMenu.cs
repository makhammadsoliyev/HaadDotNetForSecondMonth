using Spectre.Console;
using StudyTrackerSystem.Enums;
using StudyTrackerSystem.Models;
using StudyTrackerSystem.Services;

namespace StudyTrackerSystem.Display;

public class StudyPlanMenu
{
    private readonly StudyPlanService studyPlanService;

    public StudyPlanMenu(StudyPlanService studyPlanService)
    {
        this.studyPlanService = studyPlanService;
    }

    private void Add()
    {
        int studentId = AnsiConsole.Ask<int>("[aqua]StudentId: [/]");
        while (studentId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            studentId = AnsiConsole.Ask<int>("[aqua]StudentId: [/]");
        }
        int courseId = AnsiConsole.Ask<int>("[blue]CourseId: [/]");
        while (courseId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            courseId = AnsiConsole.Ask<int>("[blue]CourseId: [/]");
        }
        string materials = AnsiConsole.Ask<string>("[yellow]Materials: [/]");
        string topics = AnsiConsole.Ask<string>("[cyan2]Topics: [/]");
        string goals = AnsiConsole.Ask<string>("[cyan3]Goals: [/]");
        int studyHoursPerWeek = AnsiConsole.Ask<int>("[aqua]StudyHoursPerWeek: [/]");
        while (studyHoursPerWeek <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            studyHoursPerWeek = AnsiConsole.Ask<int>("[aqua]Id: [/]");
        }

        DayOfWeek dayOff = AnsiConsole.Ask<DayOfWeek>("[cyan3]DayOff(ex: Monday): [/]");
        PartOfDays preferredStudyTime = AnsiConsole.Ask<PartOfDays>("[blue]PreferredStudyTime(ex: Night or Morning): [/]");

        var studyPlan = new StudyPlan()
        {
            Goals = goals,
            DayOff = dayOff,
            Topics = topics,
            CourseId = courseId,
            Materials = materials,
            StudentId = studentId,
            StudyHoursPerWeek = studyHoursPerWeek,
            PreferredStudyTime = preferredStudyTime
        };

        try
        {
            var addedStudyPlan = studyPlanService.Add(studyPlan);
            AnsiConsole.MarkupLine("[green]Successfully added...[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
        }
        Thread.Sleep(1500);
    }

    private void GetById()
    {
        int id = AnsiConsole.Ask<int>("[aqua]Id: [/]");
        while (id <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            id = AnsiConsole.Ask<int>("[aqua]Id: [/]");
        }

        try
        {
            var studyPlan = studyPlanService.GetById(id);
            var table = new SelectionMenu().DataTable("StudyPlan", studyPlan);
            AnsiConsole.Write(table);
            AnsiConsole.MarkupLine("[blue]Enter to continue...[/]");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
            Thread.Sleep(1500);
        }
    }

    private void Update()
    {
        int id = AnsiConsole.Ask<int>("[aqua]Id: [/]");
        while (id <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            id = AnsiConsole.Ask<int>("[aqua]Id: [/]");
        }
        int studentId = AnsiConsole.Ask<int>("[aqua]StudentId: [/]");
        while (studentId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            studentId = AnsiConsole.Ask<int>("[aqua]StudentId: [/]");
        }
        int courseId = AnsiConsole.Ask<int>("[blue]CourseId: [/]");
        while (courseId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            courseId = AnsiConsole.Ask<int>("[blue]CourseId: [/]");
        }
        string materials = AnsiConsole.Ask<string>("[yellow]Materials: [/]");
        string topics = AnsiConsole.Ask<string>("[cyan2]Topics: [/]");
        string goals = AnsiConsole.Ask<string>("[cyan3]Goals: [/]");
        int studyHoursPerWeek = AnsiConsole.Ask<int>("[aqua]StudyHoursPerWeek: [/]");
        while (studyHoursPerWeek <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            studyHoursPerWeek = AnsiConsole.Ask<int>("[aqua]Id: [/]");
        }

        DayOfWeek dayOff = AnsiConsole.Ask<DayOfWeek>("[cyan3]DayOff(ex: Monday): [/]");
        PartOfDays preferredStudyTime = AnsiConsole.Ask<PartOfDays>("[blue]PreferredStudyTime(ex: Night or Morning): [/]");

        var studyPlan = new StudyPlan()
        {
            Id = id,
            Goals = goals,
            DayOff = dayOff,
            Topics = topics,
            CourseId = courseId,
            Materials = materials,
            StudentId = studentId,
            StudyHoursPerWeek = studyHoursPerWeek,
            PreferredStudyTime = preferredStudyTime
        };

        try
        {
            var updatedStudyPlan = studyPlanService.Update(id, studyPlan);
            AnsiConsole.MarkupLine("[green]Successfully updated...[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
        }
        Thread.Sleep(1500);
    }

    private void Delete()
    {
        int id = AnsiConsole.Ask<int>("[aqua]Id: [/]");
        while (id <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            id = AnsiConsole.Ask<int>("[aqua]Id: [/]");
        }
        try
        {
            bool isDeleted = studyPlanService.Delete(id);
            AnsiConsole.MarkupLine("[green]Successfully deleted...[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
        }
    }

    private void GetAll()
    {
        var studyPlans = studyPlanService.GetAll().ToArray();
        var table = new SelectionMenu().DataTable("StudyPlans", studyPlans);
        AnsiConsole.Write(table);
        AnsiConsole.MarkupLine("[blue]Enter to continue...[/]");
        Console.ReadKey();
    }

    private void GetStudyPlansByCourse()
    {
        int courseId = AnsiConsole.Ask<int>("[blue]CourseId: [/]");
        while (courseId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            courseId = AnsiConsole.Ask<int>("[blue]CourseId: [/]");
        }

        try
        {
            var plans = studyPlanService.GetStudyPlansByCourse(courseId).ToArray();
            var table = new SelectionMenu().DataTable($"StudyPlans of CourseId: {courseId}", plans);
            AnsiConsole.Write(table);
            AnsiConsole.MarkupLine("[blue]Enter to continue...[/]");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
            Thread.Sleep(1500);
        }
    }

    private void GetStudyPlansByStudent()
    {
        int studentId = AnsiConsole.Ask<int>("[aqua]StudentId: [/]");
        while (studentId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            studentId = AnsiConsole.Ask<int>("[aqua]StudentId: [/]");
        }

        try
        {
            var plans = studyPlanService.GetStudyPlansByStudent(studentId).ToArray();
            var table = new SelectionMenu().DataTable($"StudyPlans of StudentId: {studentId}", plans);
            AnsiConsole.Write(table);
            AnsiConsole.MarkupLine("[blue]Enter to continue...[/]");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
            Thread.Sleep(1500);
        }
    }

    public void Display()
    {
        var circle = true;
        var selectionDisplay = new SelectionMenu();

        while (circle)
        {
            AnsiConsole.Clear();
            var selection = selectionDisplay.ShowSelectionMenu("Choose one of options",
                new string[] { "Add", "GetById", "Update", "Delete", "GetAll", "GetStudyPlansByCourse", "GetStudyPlansByStudent", "Back" });

            switch (selection)
            {
                case "Add":
                    Add();
                    break;
                case "GetById":
                    GetById();
                    break;
                case "Update":
                    Update();
                    break;
                case "Delete":
                    Delete();
                    break;
                case "GetAll":
                    GetAll();
                    break;
                case "GetStudyPlansByCourse":
                    GetStudyPlansByCourse();
                    break;
                case "GetStudyPlansByStudent":
                    GetStudyPlansByStudent();
                    break;
                case "Back":
                    circle = false;
                    break;
            }
        }
    }
}
