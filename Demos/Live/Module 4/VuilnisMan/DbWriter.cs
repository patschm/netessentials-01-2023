namespace VuilnisMan;

internal class DbWriter : IDisposable
{
    private static bool _isOpen = false;
    private static FileStream _fs;

    public void Open()
    {
        if (!_isOpen)
        {
            Console.WriteLine("Opening database");
            _isOpen= true;
            _fs = File.Open("bla.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
        }
        else
        {
            Console.WriteLine("Helaas. Database is in gebruik");
        }
    }
    public void Close() 
    {
        Console.WriteLine("Closing Database");
        _isOpen = false;
    }

    public void RuimOp(bool fromFinalizer)
    {
        if (!fromFinalizer)
        {
            _fs.Dispose();
        }
        Close();
    }
    public void Dispose()
    {    
        RuimOp(false);
        GC.SuppressFinalize(this);
    }

    ~DbWriter()
    {
       RuimOp(true);
    }
}
