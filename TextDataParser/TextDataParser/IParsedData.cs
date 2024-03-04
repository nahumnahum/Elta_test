using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextDataParser
{
    public interface IParsedData
    {
        public void AddTrack(Track track);
        void ReadWhenReady();
    }

}
