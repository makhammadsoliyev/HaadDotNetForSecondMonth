namespace FitnessTrackingSystem.Models;

public class User
{
    private static int id = 0;

    public User()
    {
        this.Id = ++id;   
    }

    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public decimal Weight { get; set; }
    public decimal Height { get; set; }
    public List<string> FitnessGoals { get; set; }
    public int ExerciseExperienceMonths { get; set; }
}
