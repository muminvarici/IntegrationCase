using Integration.Common;
using Integration.Service;

namespace Integration;

public abstract class Program
{
    public static void Main(string[] args)
    {
        var service = new ItemIntegrationService();

        ThreadPool.QueueUserWorkItem(_ => PrintResult(service.SaveItem("a")));
        ThreadPool.QueueUserWorkItem(_ => PrintResult(service.SaveItem("b")));
        ThreadPool.QueueUserWorkItem(_ => PrintResult(service.SaveItem("c")));

        Thread.Sleep(500);

        ThreadPool.QueueUserWorkItem(_ => PrintResult(service.SaveItem("a")));
        ThreadPool.QueueUserWorkItem(_ => PrintResult(service.SaveItem("b")));
        ThreadPool.QueueUserWorkItem(_ => PrintResult(service.SaveItem("c")));

        Thread.Sleep(5000);

        Console.WriteLine("Everything recorded:");

        service.GetAllItems().ForEach(Console.WriteLine);

        Console.ReadLine();
    }

    private static void PrintResult(Result result)
    {
        Console.WriteLine("{0} - {1}", result.Success, result.Message);
    }
}