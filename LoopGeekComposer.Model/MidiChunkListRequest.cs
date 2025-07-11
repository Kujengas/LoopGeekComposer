using Melanchall.DryWetMidi.MusicTheory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopGeekComposer.Model
{
    public class MidiChunkListRequest
    {
        public string Rhythm { get; set; } = string.Empty;
        public int NoteLength { get; set; }
        public List<Chord> Progression { get; set; }
        public List<String> ChordList { get; set; } = new List<string>();
        public bool isChords {  get; set; }
        public int SeqType {  get; set; } 
        public bool SelectTypePerSegment { get; set; }
    }
}
