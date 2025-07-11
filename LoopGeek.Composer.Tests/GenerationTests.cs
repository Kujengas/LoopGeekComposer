namespace LoopGeek.Composer.Tests
{
    using LoopGeekComposer.Services;
    using LoopGeekComposer.Model;
    using LoopGeekComposer.Model.Extensions;

    [TestClass]
    public sealed class GenerationTests
    {
        [TestMethod]
        public void TestProgressionGeneration()
        {
            //var progression = CulomoSequencingService.GenerateChordProgression(new ChordProgressionRequest { RootNote = null, ChordQuality = null, ChordList = { "C#m", "C#", "C#m", "C#" } });
            var progression = CulomoSequencingService.GenerateChordProgression(new ChordProgressionRequest { RootNote = "CSharp0", ChordQuality = Melanchall.DryWetMidi.MusicTheory.ChordQuality.Minor });
            Assert.IsTrue(progression.ChordList.Contains("C#m"));
           
            //Console.Write(progression.GenerationResults);
        }
        [TestMethod]
        public void TestRythmGeneration() {

            var pattern = CulomoSequencingService.GenerateSequence(9, 16);
            Assert.AreEqual(pattern, "x.xxx.x.x.x.x.x.");
        }

        [TestMethod]
        public void TestMidiChunkGeneration() {

            var progression = CulomoSequencingService.GenerateChordProgression(new ChordProgressionRequest { RootNote = "CSharp0", ChordQuality = Melanchall.DryWetMidi.MusicTheory.ChordQuality.Minor });

            var request = new MidiChunkListRequest { isChords = true, Progression = progression.Progression, SeqType = (int)SequenceType.Down, Rhythm = CulomoSequencingService.GenerateRandomSequence(64, progression.Progression.Count), NoteLength = 48, SelectTypePerSegment = false };
            Assert.IsTrue(progression.ChordList.Contains("C#m"));

            var result = CulomoSequencingService.GenerateMidiChunkList(request);
            Assert.AreEqual(result.TrackChunks[1].Events[1].EventType, Melanchall.DryWetMidi.Core.MidiEventType.NoteOn);

        }
    }
}
