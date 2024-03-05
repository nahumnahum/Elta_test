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
                LogError("An error occurred while parsing the file: " + ex.Message);
            }
        }

        private async Task<string[]> ReadFileAsync(string filePath)
        {
            try
            {
                return await File.ReadAllLinesAsync(filePath);
            }
            catch (Exception ex)
            {
                LogError("An error occurred while reading the file: " + ex.Message);
                return null;
            }
        }

        private async Task ParseDataAsync(string[] lines)
        {
            if (lines.Length == 0)
            {
                Console.WriteLine("The file is empty.");
                return;
            }

            for(int i = 0; i < lines.Length; ++i)
            {
                int lineNum = i + 1;
                try
                {
                    if (string.IsNullOrWhiteSpace(lines[i]))
                    {
                        Console.WriteLine("Line " + lineNum + ": empty line encountered. Skipping...");
                        continue;
                    }

                    string[] data = lines[i].Split(',');

                    int id;
                    double locationLat, locationLon, speed;

                    if (!int.TryParse(data[0], out id))
                    {
                        
                        LogError("Failed to parse ID from line: " + lineNum);
                        continue;
                    }
                    if (!double.TryParse(data[1], out locationLat) ||
                        !double.TryParse(data[2], out locationLon) ||
                        !double.TryParse(data[3], out speed))
                    {
                        LogError("Failed to parse location or speed from line: " + lineNum);
                        continue;
                    }

                    Track track = new Track(id, locationLat, locationLon, speed);
                    ParsedData.AddTrack(track);
                }
                catch (Exception ex)
                {
                    LogError("An error occurred while parsing line: " + lineNum + ". " + ex.Message);
                }
            }
        }

        private void LogError(string message)
        {
            Console.WriteLine("Error: " + message);
        }

        protected virtual void OnParsingCompleted(ParsedDataEventArgs e)
        {
            ParsingCompleted?.Invoke(this, e);
        }
    }
}
