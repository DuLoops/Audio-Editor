using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp
{
    public class WaveHeader
    {
        public int chunkID;
        public int chunkSize;
        public int format;
        public int subchunk1ID;
        public int subchunk1Size;
        public short audioFormat;
        public short numChannels;
        public int sampleRate;
        public int byteRate;
        public short blockAligh;
        public short bps;
        public int subchunk2ID;
        public int subchunk2Size;

        public void reset()
        {
            chunkID        = 0;
            chunkSize      = 0;
            format         = 0;
            subchunk1ID    = 0;
            subchunk1Size  = 0;
            audioFormat    = 0;
            numChannels    = 0;
            sampleRate     = 0;
            byteRate       = 0;
            blockAligh     = 0;
            bps            = 0;
            subchunk2ID    = 0;
            subchunk2Size  = 0;
        }
    }

}
