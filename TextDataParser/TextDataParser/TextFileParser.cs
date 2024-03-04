using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextDataParser
{
    public class TextFileParser
    {
        public event EventHandler<ParsedDataEventArgs> ParsingCompleted;

        private string FilePath;

        private IParsedData ParsedData;

        public TextFileParser(string filePath, IParsedData parsedData)
        {
            FilePath = filePath;
            ParsedData = parsedData;
        }

        public async Task Run()
        {
            try
            {
                string[] lines = await ReadFileAsync(FilePath);
                await ParseDataAsync(lines);
                OnParsingCompleted(new ParsedDataEventArgs(ParsedData));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while parsing the file: {ex.Message}");
            }
        }

        private async Task<string[]> ReadFileAsync(string filePath)
        {
            return await File.ReadAllLinesAsync(filePath);
        }

        private async Task ParseDataAsync(string[] lines)
        {
            foreach (var line in lines)
            {
                string[] data = line.Split(',');
                int id = int.Parse(data[0]);
                double locationLat = double.Parse(data[1]);
                double locationLon = double.Parse(data[2]);
                double speed = double.Parse(data[3]);
                Track track = new Track(id, locationLat, locationLon, speed);
                ParsedData.AddTrack(track);
            }
        }

        protected virtual void OnParsingCompleted(ParsedDataEventArgs e)
        {
            ParsingCompleted?.Invoke(this, e);
        }

        public class ParsedDataEventArgs : EventArgs
        {
            public IParsedData ParsedData { get; }

            public ParsedDataEventArgs(IParsedData parsedData)
            {
                ParsedData = parsedData;
            }
        }
    }
}