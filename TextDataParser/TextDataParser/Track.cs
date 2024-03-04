using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextDataParser
{
    public class Track
    {
        public int Id { get; set; }
        public double LocationLat { get; set; }
        public double LocationLon { get; set; }
        public double Speed { get; set; }

        public Track()
        {}

        public Track(int id, double locationLat, double locationLon, double speed)
        {
            Id = id;
            LocationLat = locationLat;
            LocationLon = locationLon;
            Speed = speed;
        }

        public override string ToString()
        {
            return $"Track ID: {Id}, Location: ({LocationLat}, {LocationLon}), Speed: {Speed}";
        }
    }
}

