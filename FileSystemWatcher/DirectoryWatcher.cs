namespace FileSystemWatcherHW;

public class DirectoryWatcher
{
    public static void StartWatching(string path)
    {
        FileSystemWatcher watcher = new FileSystemWatcher();
        watcher.Path = path;

        watcher.Created += OnCreated;
        watcher.Deleted += OnDeleted;
        watcher.Renamed += OnRenamed;
        watcher.Changed += OnChanged;
        watcher.Error += OnError;

        watcher.EnableRaisingEvents = true;

        Console.WriteLine($"Monitoring directory: {path}");
        Console.WriteLine("Press 'q' to quit the program.");

        while (Console.Read() != 'q') ;
    }

    static void OnCreated(object sender, FileSystemEventArgs e)
        => Console.WriteLine($"{e.Name} is created in {e.FullPath}\n");

    static void OnDeleted(object sender, FileSystemEventArgs e)
        => Console.WriteLine($"{e.Name} is deleted from {e.FullPath}\n");

    static void OnChanged(object sender, FileSystemEventArgs e)
        => Console.WriteLine($"{e.Name} is changed in {e.FullPath} \n");

    static void OnRenamed(object sender, RenamedEventArgs e)
        => Console.WriteLine($"{e.OldName} is renamed to {e.Name} in {e.FullPath}\n");

    static void OnError(object sender, ErrorEventArgs e)
        => Console.WriteLine(e.GetException());
}
