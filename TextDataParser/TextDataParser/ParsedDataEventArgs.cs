using System;

namespace TextDataParser
{
    public class ParsedDataEventArgs : EventArgs
    {
        public IParsedData ParsedData { get; }

        public ParsedDataEventArgs(IParsedData parsedData)
        {
            ParsedData = parsedData;
        }
    }
}
