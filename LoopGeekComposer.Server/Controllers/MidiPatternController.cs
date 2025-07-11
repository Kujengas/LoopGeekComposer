using LoopGeekComposer.Model;
using LoopGeekComposer.Services;
using Melanchall.DryWetMidi.MusicTheory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoopGeekComposer.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MidiPatternController : ControllerBase
    {

        private readonly ILogger<MidiPatternController> _logger;

        public MidiPatternController(ILogger<MidiPatternController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "GenerateMidiPattern")]
        public MidiChunkListResult Post(MidiChunkListRequest request) {

            if (request == default) { 
                var progression = CulomoSequencingService.GenerateChordProgression(new ChordProgressionRequest { RootNote = "G5", ChordQuality = Melanchall.DryWetMidi.MusicTheory.ChordQuality.Minor });
                request = new MidiChunkListRequest { isChords = true, Progression = progression.Progression, SeqType = (int)SequenceType.Up, Rhythm = CulomoSequencingService.GenerateRandomSequence(64, progression.Progression.Count), NoteLength = 48, SelectTypePerSegment = false };
            }

            if (request.ChordList.Count > 0)
            {
                request.Progression = new List<Chord>();

                request.ChordList.ForEach(x =>
                {
                    request.Progression.Add(Chord.Parse(x));
                });
                request.Rhythm = CulomoSequencingService.GenerateRandomSequence(64, request.Progression.Count);
            }

            var result = CulomoSequencingService.GenerateMidiChunkList(request);

            return result;
        }

    }
}
