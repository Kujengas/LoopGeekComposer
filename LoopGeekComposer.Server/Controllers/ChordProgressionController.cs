using Microsoft.AspNetCore.Mvc;
using LoopGeekComposer.Model;
using LoopGeekComposer.Services;
using Melanchall.DryWetMidi.MusicTheory;

namespace LoopGeekComposer.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChordProgressionController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<ChordProgressionController> _logger;

        public ChordProgressionController(ILogger<ChordProgressionController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetInitialChordProgression")]
        public ProgressionResult Get()
        {
            //Rest in Power Jimmy Hendrix for "Just G"
            return CulomoSequencingService.GenerateChordProgression(new ChordProgressionRequest { RootNote = "G4", ChordQuality = Melanchall.DryWetMidi.MusicTheory.ChordQuality.Major });

        }

        [HttpPost(Name = "GenerateChordProgression")]
        public ProgressionResult Post(string rootNoteName, string chordQuality )
        {

            ChordQuality selectedQuality = ChordQuality.Major;
            switch (chordQuality.ToLower()){
                case "major":
                    selectedQuality = ChordQuality.Major;
                    break;
                case "minor":
                    selectedQuality = ChordQuality.Minor;
                    break;
                case "deminished":
                    selectedQuality = ChordQuality.Diminished;
                    break;
                case "augmented":
                    selectedQuality = ChordQuality.Augmented;
                    break;
                default:
                    break;
            }

            //Rest in Power Jimmy Hendrix for "Just G"
            return CulomoSequencingService.GenerateChordProgression(new ChordProgressionRequest { RootNote = rootNoteName.ToUpper().Replace("#", "Sharp")+"0", ChordQuality = selectedQuality });

        }
    }
}
