using HospitalInformationSystem.Entities;
using HospitalInformationSystem.Services;
using Spectre.Console;

namespace HospitalInformationSystem.Menus;

public class AppointmentMenu
{
    private readonly AppointmentService appointmentService;

    public AppointmentMenu(AppointmentService appointmentService)
    {
        this.appointmentService = appointmentService;
    }

    private void Add()
    {
        int patientId = AnsiConsole.Ask<int>("[aqua]PatientId: [/]");
        while (patientId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            patientId = AnsiConsole.Ask<int>("[aqua]PatientId: [/]");
        }
        int doctorId = AnsiConsole.Ask<int>("[aqua]DoctorId: [/]");
        while (doctorId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            doctorId = AnsiConsole.Ask<int>("[blue]DoctorId: [/]");
        }
        DateTime time = AnsiConsole.Ask<DateTime>("[cyan2]Time: [/]");

        var appointment = new Appointment()
        {
            Time = time,
            DoctorId = doctorId,
            PatientId = patientId
        };
        try
        {
            var addedAppointment = appointmentService.Add(appointment);
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
            var appointment = appointmentService.GetById(id);
            var table = new SelectionMenu().DataTable("Appointment", appointment);
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
        int patientId = AnsiConsole.Ask<int>("[aqua]PatientId: [/]");
        while (patientId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            patientId = AnsiConsole.Ask<int>("[aqua]PatientId: [/]");
        }
        int doctorId = AnsiConsole.Ask<int>("[aqua]DoctorId: [/]");
        while (doctorId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            doctorId = AnsiConsole.Ask<int>("[blue]DoctorId: [/]");
        }
        DateTime time = AnsiConsole.Ask<DateTime>("[cyan2]Time: [/]");
        var appointment = new Appointment()
        {
            Id = id,
            Time = time,
            DoctorId = doctorId,
            PatientId = patientId
        };

        try
        {
            var updatedAppointment = appointmentService.Update(id, appointment);
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
            bool isDeleted = appointmentService.Delete(id);
            AnsiConsole.MarkupLine("[green]Successfully deleted...[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
        }
        Thread.Sleep(1500);
    }

    private void GetAll()
    {
        var appointments = appointmentService.GetAll().ToArray();
        var table = new SelectionMenu().DataTable("Appointments", appointments);
        AnsiConsole.Write(table);
        AnsiConsole.MarkupLine("[blue]Enter to continue...[/]");
        Console.ReadKey();
    }

    public void Display()
    {
        var circle = true;
        var selectionDisplay = new SelectionMenu();

        while (circle)
        {
            AnsiConsole.Clear();
            var selection = selectionDisplay.ShowSelectionMenu("Choose one of options",
                new string[] { "Add", "GetById", "Update", "Delete", "GetAll", "Back" });

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
                case "Back":
                    circle = false;
                    break;
            }
        }
    }
}
