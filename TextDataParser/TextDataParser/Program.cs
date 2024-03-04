using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static TextDataParser.TextFileParser;
using Microsoft.Extensions.DependencyInjection;

namespace TextDataParser
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            string filePath = "C:\\Users\\הלפרין\\source\\repos\\Elta_test\\TextDataParser\\TextDataParser\\Data.txt";

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
            Console.WriteLine("Parsing completed successfully.");
        }
    }
}

