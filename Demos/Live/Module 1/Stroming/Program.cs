using System.IO.Compression;
using System.Text;

namespace Stroming;
class Program
{
    static void Main(string[] args)
    {
        //NerdyWriting();
        //NerdyReading();
        //HandyWriting();
        //HandyReading();
        //GzipWriting();
        GzipReading();
    }

    private static void NerdyReading()
    {
        FileStream stream = File.OpenRead("data.txt");

        byte[] buffer = new byte[4];
        while(stream.Read(buffer, 0, buffer.Length) > 0)
        {       
            string txt = Encoding.UTF8.GetString(buffer);
            Console.Write(txt);
            Array.Clear(buffer);
        }
    }

    private static void NerdyWriting()
    {
        FileStream stream = File.Create("data.txt");

        var data = "Hello World";
        for(int i = 0; i < 1000; i++)
        {
            byte[] buffer = Encoding.UTF8.GetBytes($"{data} {i}\r\n");
            stream.Write(buffer, 0, buffer.Length);
        }
        stream.Flush();
        stream.Close();
    }

    private static void HandyReading()
    {
        FileStream stream = File.OpenRead("data.txt");
        StreamReader reader = new StreamReader(stream);

        string? line;
        while((line = reader.ReadLine()) != null)
        {       
            Console.WriteLine(line);
        }
    }

    private static void HandyWriting()
    {
        FileStream stream = File.Create("data1.txt");
        StreamWriter writer = new StreamWriter(stream);
        var data = "Hello World";
        for(int i = 0; i < 1000; i++)
        {
            writer.WriteLine($"{data} {i}");
        }
        writer.Flush();
        writer.Close();
        stream.Close();
    }
    private static void GzipWriting()
    {
        FileStream stream = File.Create("data1.zip");
        GZipStream zip = new GZipStream(stream, CompressionMode.Compress);
        StreamWriter writer = new StreamWriter(zip);
        var data = "Hello World";
        for(int i = 0; i < 1000; i++)
        {
            writer.WriteLine($"{data} {i}");
        }
        writer.Flush();
        writer.Close();
        stream.Close();
    }
    private static void GzipReading()
    {
        FileStream stream = File.OpenRead("data1.zip");
        GZipStream zip = new GZipStream(stream, CompressionMode.Decompress);
        StreamReader reader = new StreamReader(zip);

        string? line;
        while((line = reader.ReadLine()) != null)
        {       
            Console.WriteLine(line);
        }
    }
}
