namespace FitnessTrackingSystem.Models;

public class Workout
{
    private static int id = 0;

    public Workout()
    {
        this.Id = ++id;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public List<string> ExerciseSets { get; set; }
    public int RestTime { get; set; } // minutes
    public List<string> TargetedMuscleGroups { get; set; }
}
