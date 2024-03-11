using System;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JsonSerializasion
{
    public class Program
    {
        public class Programer
        {
            public string FirstName;
            public string LastName;
            public int Age;
            public bool Active;
            public List<string> Languages;
        }
        
        static void Main()
        {
            string configFile = File.ReadAllText("appsettings.json");
            
            dynamic config = JsonConvert.DeserializeObject(configFile);
            
            string fileName = config.FileName;
            
            string json = File.ReadAllText(fileName);

            Programer deserialized = JsonConvert.DeserializeObject<Programer>(json);
            
            Console.WriteLine("First name: " + deserialized.FirstName);
            Console.WriteLine("Last name: " + deserialized.LastName);
            Console.WriteLine("Age: " + deserialized.Age);
            Console.WriteLine("Active: " + deserialized.Active);
            Console.WriteLine("Languages: ");
            foreach (var languages in deserialized.Languages)
            {
                Console.WriteLine("  * " + languages);
            }
            Console.WriteLine("**************************************");

            string serialized = JsonConvert.SerializeObject(deserialized);
            Console.WriteLine(serialized);
        }
        
    }
}