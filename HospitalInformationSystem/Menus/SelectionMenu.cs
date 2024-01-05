using HospitalInformationSystem.Entities;
using Spectre.Console;

namespace HospitalInformationSystem.Menus;



public class SelectionMenu
{
    public Table DataTable(string title, params Patient[] patients)
    {
        var table = new Table();

        table.Title(title.ToUpper())
            .BorderColor(Color.Blue)
            .AsciiBorder();

        table.AddColumn("ID");
        table.AddColumn("FirstName");
        table.AddColumn("LastName");
        table.AddColumn("Gender");
        table.AddColumn("DateOfBirth");
        table.AddColumn("Phone");
        table.AddColumn("Address");
        table.AddColumn("MedicalHistory");


        foreach (var patient in patients)
            table.AddRow(patient.Id.ToString(), patient.FirstName, patient.LastName, patient.Gender.ToString(),
                patient.DateOfBirth.ToString(), patient.Phone, patient.Address, patient.MedicalHistory);

        return table;
    }

    public Table DataTable(string title, params Doctor[] doctors)
    {
        var table = new Table();

        table.Title(title.ToUpper())
            .BorderColor(Color.Blue)
            .AsciiBorder();

        table.AddColumn("ID");
        table.AddColumn("FirstName");
        table.AddColumn("LastName");
        table.AddColumn("Phone");
        table.AddColumn("Address");
        table.AddColumn("Number of Appointments");


        foreach (var doctor in doctors)
            table.AddRow(doctor.Id.ToString(), doctor.FirstName, doctor.LastName,
                doctor.Phone, doctor.Address, doctor.Appointments.Count.ToString());

        return table;
    }

    public Table DataTable(string title, params MedicalRecord[] records)
    {
        var table = new Table();

        table.Title(title.ToUpper())
            .BorderColor(Color.Blue)
            .AsciiBorder();

        table.AddColumn("ID");
        table.AddColumn("MedicalConditions");
        table.AddColumn("Medications");
        table.AddColumn("TestResults");
        table.AddColumn("TreatmentPlans");
        table.AddColumn("PatientId");
        table.AddColumn("DoctorId");


        foreach (var record in records)
            table.AddRow(record.Id.ToString(), record.MedicalConditions, record.Medications, record.TestResults,
                record.TreatmentPlans, record.PatientId.ToString(), record.DoctorId.ToString());

        return table;
    }

    public Table DataTable(string title, params Appointment[] appointments)
    {
        var table = new Table();

        table.Title(title.ToUpper())
            .BorderColor(Color.Blue)
            .AsciiBorder();

        table.AddColumn("ID");
        table.AddColumn("PatientId");
        table.AddColumn("DoctorId");
        table.AddColumn("Time");

        foreach (var appointment in appointments)
            table.AddRow(appointment.Id.ToString(), appointment.Time.ToString(), appointment.PatientId.ToString(), appointment.DoctorId.ToString());

        return table;
    }

    public string ShowSelectionMenu(string title, string[] options)
    {
        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title(title)
                .PageSize(5) // Number of items visible at once
                .AddChoices(options)
                .HighlightStyle(new Style(foreground: Color.Cyan1, background: Color.Blue))
        );

        return selection;
    }
}

