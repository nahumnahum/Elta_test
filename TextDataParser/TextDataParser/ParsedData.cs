using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextDataParser
{
    public class ParsedData : IParsedData
    {
        private List<Track> tracks;

        public ParsedData()
        {
            tracks = new List<Track>();
        }

        public void AddTrack(Track track)
        {
            tracks.Add(track);
        }

        public void ReadWhenReady()
        {
            foreach (var track in tracks)
            {
                Console.WriteLine(track.ToString());
            }
        }
    }
}

