namespace VuilnisMan;

internal class Program
{
    static DbWriter connection1 = new DbWriter();
    static DbWriter connection2 = new DbWriter();   

    static void Main(string[] args)
    {
        try
        {
            connection1.Open();
        }
        finally
        {
            connection1.Dispose();
        }
        connection1 = null;

        GC.Collect();
        GC.WaitForPendingFinalizers();

        using (connection2)
        {
            connection2.Open();
        }
        connection2 = null;
        Console.ReadLine();
    }
}