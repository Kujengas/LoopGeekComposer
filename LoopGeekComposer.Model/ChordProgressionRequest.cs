using Melanchall.DryWetMidi.MusicTheory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopGeekComposer.Model
{
    public class ChordProgressionRequest
    {
        public string RootNote { get; set; } = string.Empty;
        public ChordQuality? ChordQuality { get; set; } = null;

        public List<String> ChordList { get; set; } = new List<string>();
   
    }
}
