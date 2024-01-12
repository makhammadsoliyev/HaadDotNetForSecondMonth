using CompressAndExtract.Interfaces;
using System.IO.Compression;

namespace CompressAndExtract.Services;

public class CompressionService : ICompressionService
{
    public void CompressFile(string sourceFilePath, string zipFilePath)
    {
        if (!File.Exists(sourceFilePath))
            throw new Exception("File does not exist");

        if (Path.GetExtension(zipFilePath) != ".zip")
            throw new Exception("Zip File Path is not valid");

        var directory = Path.Combine(Path.GetDirectoryName(sourceFilePath), Path.GetFileNameWithoutExtension(sourceFilePath));
        Directory.CreateDirectory(directory);
        string newSourceFilePath = Path.Combine(directory, Path.GetFileName(sourceFilePath));
        File.Delete(newSourceFilePath);
        File.Copy(sourceFilePath, newSourceFilePath);
        ZipFile.CreateFromDirectory(directory, zipFilePath);
        Directory.Delete(directory, true);
    }

    public void CompressFolder(string sourceFolderPath, string zipFilePath)
    {
        if (!Directory.Exists(sourceFolderPath))
            throw new Exception("Folder does not exist");

        if (Path.GetExtension(zipFilePath) != ".zip")
            throw new Exception("Zip File Path is not valid");

        ZipFile.CreateFromDirectory(sourceFolderPath, zipFilePath);
    }

    public void DecompressFile(string zipFilePath, string extractToFolderPath)
    {
        if (!File.Exists(zipFilePath))
            throw new Exception("File does not exist");

        if (Path.GetExtension(zipFilePath) != ".zip")
            throw new Exception("Zip File Path is not valid");

        /*if (!Directory.Exists(extractToFolderPath))
            throw new Exception("Folder does not exist");*/

        ZipFile.ExtractToDirectory(zipFilePath, extractToFolderPath);
    }

    public void DecompressFolder(string zipFilePath, string extractToFolderPath)
    {
        if (!File.Exists(zipFilePath))
            throw new Exception("File does not exist");

        if (Path.GetExtension(zipFilePath) != ".zip")
            throw new Exception("Zip File Path is not valid");

        /*        if (!Directory.Exists(extractToFolderPath))
                    throw new Exception("Folder does not exist");*/

        ZipFile.ExtractToDirectory(zipFilePath, extractToFolderPath);
    }
}
