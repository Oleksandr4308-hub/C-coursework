using BLL.Dependency;
using PL;

internal class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        string storagePath = Path.Combine(Directory.GetCurrentDirectory(), "Storage");

        if (!Directory.Exists(storagePath))
            Directory.CreateDirectory(storagePath);

        var (workerService, deptService, posService, projService) =
            ServiceFactory.CreateServices(storagePath);


        var menu = new ConsoleMenu(workerService, deptService, posService, projService);
        menu.Start();
    }
}
