namespace TransferFilesInChunks;

public static class TransferInChunks
{
    public static void TransferFileInChunks(string sourceFilePath, string destinationDirectory)
    {
        int bufferSize = 1024 * 1024;
        byte[] buffer = new byte[bufferSize];
        long totalBytes = new FileInfo(sourceFilePath).Length;
        long bytesTransferred = 0;

        FileStream sourceStream = File.OpenRead(sourceFilePath);

        string fileName = Path.GetFileName(sourceFilePath);
        string destinationFilePath = Path.Combine(destinationDirectory, fileName);

        FileStream destinationStream = File.Create(destinationFilePath);

        int bytesRead;

        while ((bytesRead = sourceStream.Read(buffer, 0, buffer.Length)) > 0)
        {
            destinationStream.Write(buffer, 0, bytesRead);
            bytesTransferred += bytesRead;

            decimal percentage = (decimal)bytesTransferred / totalBytes;
            Console.Clear();
            Console.WriteLine($"{percentage:P2}% complete");
            //Thread.Sleep();
        }

        sourceStream.Close();
        destinationStream.Close();
    }
}
