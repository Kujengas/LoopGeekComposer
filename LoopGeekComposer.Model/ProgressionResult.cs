using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Melanchall.DryWetMidi.MusicTheory;

namespace LoopGeekComposer.Model
{
    public class ProgressionResult
    {
        public string RootNote { get; set; }
        public ChordQuality RootQuality { get; set; }
        public List<Chord> Progression { get; set; }
        public List<String> ChordList { get; set; }
        public int DefaultSeqType { get; set; } = -1;
        public List<SequenceType> SeqTypes { get; set; }
        public bool SelectTypePerSegment { get; set; }
        public string ProgressionPreset { get; set; }
        public string ProgressionDescription { get; set; }
        public string GenerationResults { get; set; }
    }
}
