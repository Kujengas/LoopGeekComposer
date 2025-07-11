using Melanchall.DryWetMidi.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopGeekComposer.Model
{
    public class MidiChunkListResult
    {
       public SequencePatternList NotePattern { get; set; }
       public List<TrackChunk> TrackChunks { get; set; }
       public MidiChunkListRequest Request { get; set; }
    }
}
