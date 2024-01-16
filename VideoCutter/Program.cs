using System.Diagnostics;

class Program
{
    static void Main()
    {
        string inputFilePath = "C:\\Users\\User\\Desktop\\dotnet\\HaadDotNetForSecondMonth\\VideoCutter\\Videos\\20 Sec Timer.mp4";
        string outputFilePath = "output.mp4";
        string startTime = "00:00:10";  // Start time in HH:mm:ss format
        string duration = "00:00:05";   // Duration in HH:mm:ss format

        CutVideo(inputFilePath, outputFilePath, startTime, duration);
    }

    static void CutVideo(string inputFilePath, string outputFilePath, string startTime, string duration)
    {
        Process process = new Process();
        process.StartInfo.FileName = "ffmpeg.exe";  // Assuming ffmpeg is in the same directory as your application
        process.StartInfo.Arguments = $"-i \"{inputFilePath}\" -ss {startTime} -t {duration} -c copy \"{outputFilePath}\"";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.CreateNoWindow = true;

        process.Start();

        process.WaitForExit();

        if (process.ExitCode == 0)
        {
            Console.WriteLine("Video cut successfully.");
        }
        else
        {
            Console.WriteLine("Error cutting video. Check the console output for more information.");
        }
    }
}

