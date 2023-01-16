using System.Text.Json;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newton = Newtonsoft.Json.JsonSerializer;

namespace Cerials;
class Program
{
    static void Main(string[] args)
    {
        Person p1 = new Person {FirstName="jan", LastName="Pieters", Age=42};

        //SchrijfNaarFile(p1);
        //ReadFromFile();
        //p1.Introduce();

        //SchrijfNaarJson(p1);
        //SchrijfNaarJsonVanNewton(p1);
        LeesVanJsonNewtonian();
    }

    private static void LeesVanJsonNewtonian()
    {
        var stream = File.OpenRead("newton.json");
        var txtReader = new StreamReader(stream);
        var pss =   JsonConvert.DeserializeObject<Person>(txtReader.ReadToEnd());
        pss?.Introduce();

        stream = File.OpenRead("newton.json");
        txtReader = new StreamReader(stream);
        var reader = new JsonTextReader(txtReader);
        Newton serializer = new Newton();
        serializer.ContractResolver = new CamelCasePropertyNamesContractResolver();

        var ps = serializer.Deserialize<Person>(reader);
        ps?.Introduce();
    }

    private static void SchrijfNaarJsonVanNewton(Person p1)
    {
        var stream = File.Create("newton.json"); 
        var writer = new StreamWriter(stream);
        Newton serializer = new Newton();
        serializer.ContractResolver = new CamelCasePropertyNamesContractResolver();

        serializer.Serialize(writer, p1);
        writer.Flush();
    }

    private static void SchrijfNaarJson(Person px)
    {
        var stream = File.Create("people.json"); 
        var options   = new JsonSerializerOptions();
        options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        System.Text.Json.JsonSerializer.Serialize(stream, px, options);
        stream.Flush();
    }

    private static void ReadFromFile()
    {
        var steam = File.OpenRead("people.xml");

        XmlSerializer serializer=new XmlSerializer(typeof(Person));
        var px = serializer.Deserialize(steam) as Person;
        px.Introduce();
    }

    private static void SchrijfNaarFile(Person p1)
    {
        var steam = File.Create("people.xml");

        XmlSerializer serializer=new XmlSerializer(typeof(Person));
        serializer.Serialize(steam, p1);
    }
}
