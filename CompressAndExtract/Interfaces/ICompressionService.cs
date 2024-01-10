namespace CompressAndExtract.Interfaces;

public interface ICompressionService
{
    void CompressFolder(string sourceFolderPath, string zipFilePath);

    void DecompressFolder(string zipFilePath, string extractToFolderPath);

    void CompressFile(string sourceFilePath, string zipFilePath);

    void DecompressFile(string zipFilePath, string extractToFolderPath);
}
