namespace TaskDemos;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine($"Main thread ID {Thread.CurrentThread.ManagedThreadId}");
        //Synchronous();
        //Asynchronous();
        //TaskChaining();
        //NogFraaier();
        Fouten();
        Console.WriteLine("End Program");
        Console.ReadLine();
    }

    private static async Task Fouten()
    {
        //ErrorFunction().ContinueWith(pt => {          
        //    if (pt.Exception!= null)
        //    {
        //        Console.WriteLine(pt.Exception.Message);
        //    }
        //});
        try
        {
            await ErrorFunction();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
}

    private static async void NogFraaier()
    {
        int res = await LongAddAsync(5, 6);
        Console.WriteLine(res);

        Task<int> t1 = new Task<int>(() => {
            var bla = LongAdd(2, 3);
            return bla;
        });
        t1.Start();
        int result =  await t1;   // Soort van return
        Console.WriteLine(result);

        var t2 = Task.Run(() => LongAdd(4, 5));
        result= await t2;
        Console.WriteLine(result);
    }

    private static void TaskChaining()
    {
        Task<int> t1 = new Task<int>(() => {
            var bla = LongAdd(2, 3);
            return bla;
        });
        t1.ContinueWith(pt => {
            Console.WriteLine(pt.Result);
        }).ContinueWith(vt => Console.WriteLine("Klaar"));

        t1.ContinueWith(vorig => Console.WriteLine("Dit doen we ook"));

        t1.Start();
        //Console.WriteLine(t1.Result);
    }

    private static void Asynchronous()
    {
        Task t2 = Task.Run(() =>
        {
            Console.WriteLine($"Thread ID {Thread.CurrentThread.ManagedThreadId}");
            var result = LongAdd(3, 4);
            Console.WriteLine(result);
        });

       
        Task t1 = new Task(() => {
            Console.WriteLine($"Thread ID {Thread.CurrentThread.ManagedThreadId}");
            var result = LongAdd(2, 3);
            Console.WriteLine(result);
        });

        t1.Start(); 
        Console.WriteLine(t2.Status);
    }

    private static void Synchronous()
    {
        var result = LongAdd(1, 2);
        Console.WriteLine(result);
    }

    static int LongAdd(int a, int b)
    {
        Task.Delay(10000).Wait();
        return a + b;
    }
    static Task<int> LongAddAsync(int a, int b)
    {
        var t1 = Task.Run(() => LongAdd(a, b));
        return t1;
    }
    static async Task ErrorFunction()
    {
        await Task.Run(() =>
        {
            Console.WriteLine("Starten maar");
            throw new Exception("Ooops");
        });
    }
}