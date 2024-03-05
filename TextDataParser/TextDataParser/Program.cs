using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static TextDataParser.TextFileParser;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using System.IO;

namespace TextDataParser
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            string pathToAppSettings = "appsettings.json";
            string jsonString = File.ReadAllText(pathToAppSettings);
            JObject jsonObject = JObject.Parse(jsonString);
            string filePath = (string)jsonObject["FilePath"];
            
            var services = new ServiceCollection();
            services.AddSingleton<IParsedData, ParsedData>();
            var serviceProvider = services.BuildServiceProvider();
            var parsedDataService = serviceProvider.GetRequiredService<IParsedData>();
            var parser = new TextFileParser(filePath, parsedDataService);

            parser.ParsingCompleted += (sender, eventArgs) =>
            {
                var parsedData = eventArgs.ParsedData;
                parsedData.ReadWhenReady();
            };

            await parser.Run();
        }
    }
}

