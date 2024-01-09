using TransferFilesInChunks;

Console.Write("SourceFilePath: ");
var sourceFilePath = Console.ReadLine();

while (!File.Exists(sourceFilePath))
{
    Console.WriteLine("Invalid file");
    Console.Write("SourceFilePath: ");
    sourceFilePath = Console.ReadLine();
}


Console.Write("DestinationDirectory: ");
var destinationDirectory = Console.ReadLine();

while (!Directory.Exists(destinationDirectory))
{
    Console.WriteLine("Invalid directory");
    Console.Write("DestinationDirectory: ");
    destinationDirectory = Console.ReadLine();
}

TransferInChunks.TransferFileInChunks(sourceFilePath, destinationDirectory);
Console.WriteLine("File transfer completed!");