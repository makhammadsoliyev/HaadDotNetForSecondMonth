using Spectre.Console;
using System.Diagnostics;

startLoop:
try
{
    var file1 = AnsiConsole.Ask<string>("File1: ");
    var thread1 = new Thread(
    () => { OpenExplorerWithFile(file1); }
    );

    var file2 = AnsiConsole.Ask<string>("File2: ");
    var thread2 = new Thread(
        () => { OpenExplorerWithFile(file2); }
    );

    var file3 = AnsiConsole.Ask<string>("File3: ");
    var thread3 = new Thread(
        () => { OpenExplorerWithFile(file3); }
    );

    var file4 = AnsiConsole.Ask<string>("File4: ");
    var thread4 = new Thread(
        () => { OpenExplorerWithFile(file4); }
    );

    var file5 = AnsiConsole.Ask<string>("File5: ");
    var thread5 = new Thread(
        () => { OpenExplorerWithFile(file5); }
    );

    thread1.Start();
    thread2.Start();
    thread3.Start();
    thread4.Start();
    thread5.Start();
    thread1.Join();
    thread2.Join();
    thread3.Join();
    thread4.Join();
    thread5.Join();
}
catch (Exception ex)
{
    // PDF
    goto startLoop;
    AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
    Thread.Sleep(1500);
}



static void OpenExplorerWithFile(string filePath)
{
    Process.Start("explorer.exe", filePath);
}


