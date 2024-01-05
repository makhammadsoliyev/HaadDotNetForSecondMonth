using HospitalInformationSystem.Services;
using Spectre.Console;

namespace HospitalInformationSystem.Menus;

public class MainMenu
{
    private readonly DoctorService doctorService;
    private readonly PatientService patientService;
    private readonly AppointmentService appointmentService;
    private readonly MedicalRecordService medicalRecordService;

    private readonly DoctorMenu doctorMenu;
    private readonly PatientMenu patientMenu;
    private readonly AppointmentMenu appointmentMenu;
    private readonly MedicalRecordMenu medicalRecordMenu;

    public MainMenu()
    {
        this.doctorService = new DoctorService();
        this.patientService = new PatientService();
        this.appointmentService = new AppointmentService(patientService, doctorService);
        this.medicalRecordService = new MedicalRecordService(doctorService, patientService);

        this.doctorMenu = new DoctorMenu(doctorService);
        this.patientMenu = new PatientMenu(patientService);
        this.appointmentMenu = new AppointmentMenu(appointmentService);
        this.medicalRecordMenu = new MedicalRecordMenu(medicalRecordService);
    }

    public void Main()
    {
        var circle = true;
        var selectionDisplay = new SelectionMenu();

        while (circle)
        {
            AnsiConsole.Clear();
            var selection = selectionDisplay.ShowSelectionMenu("Choose one of options", new string[] { "Patient", "Doctor", "MedicalRecord", "Appointment", "Exit" });

            switch (selection)
            {
                case "Patient":
                    patientMenu.Display();
                    break;
                case "Doctor":
                    doctorMenu.Display();
                    break;
                case "MedicalRecord":
                    medicalRecordMenu.Display();
                    break;
                case "Appointment":
                    appointmentMenu.Display();
                    break;
                case "Exit":
                    circle = false;
                    break;
            }
        }
    }
}
