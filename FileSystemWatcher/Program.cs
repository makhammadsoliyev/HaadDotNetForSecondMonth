using FileSystemWatcherHW;

Console.Write("DirectoryPath: ");
var directoryPath = Console.ReadLine();

while (!Directory.Exists(directoryPath))
{
    Console.WriteLine("Invalid directory");
    Console.Write("DirectoryPath: ");
    directoryPath = Console.ReadLine();
}

DirectoryWatcher.StartWatching(directoryPath);