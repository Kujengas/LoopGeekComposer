using LoopGeekComposer.Model;
using LoopGeekComposer.Model.Extensions;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;
using Melanchall.DryWetMidi.MusicTheory;
using System.Linq;
using System.Text;

namespace LoopGeekComposer.Services
{
    public class CulomoSequencingService
    {

        public string Generate()
        {
            StringBuilder sbResults = new StringBuilder();
            var progressionObject = GenerateChordProgression(new ChordProgressionRequest());

            sbResults.AppendLine(progressionObject.GenerationResults.ToString());

            string path = @"E:\ProductionFiles\MidiFiles\PV\" + DateTime.Now.Year + "." + DateTime.Now.Month + "." + DateTime.Now.Day + "-" + progressionObject.ProgressionDescription + "\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }


            /*
            progression.First().GetInversions().Dump();
            progression.First().ResolveNotes(Octave.Get(4)).Dump();
            */

            //SplitInParts

            //Generate Chords
            List<Tuple<int, int>> seqPairs = new List<Tuple<int, int>> { new Tuple<int, int>(8, 384), new Tuple<int, int>(16, 192), new Tuple<int, int>(32, 96), new Tuple<int, int>(64, 48) };
            //var currentSeqPair = seqPairs.OrderBy(x=>Guid.NewGuid()).First();
            var currentSeqPair = seqPairs.GetShuffled().First();
            string seq = GenerateRandomSequence(currentSeqPair.Item1, progressionObject.Progression.Count);//chordList.Count);
            sbResults.AppendLine("Sequenence 1 (chords):" + seq);
            sbResults.AppendLine("MidiFile:" + GenerateMidiFile(seq, currentSeqPair.Item2, progressionObject.Progression, true, progressionObject.ProgressionDescription, path, "", progressionObject.DefaultSeqType, progressionObject.SelectTypePerSegment));

            seq = seq.ShuffleSegments(4);
            sbResults.AppendLine("Sequenence 1 remixed (chords):" + seq);
            sbResults.AppendLine("MidiFile:" + GenerateMidiFile(seq, currentSeqPair.Item2, progressionObject.Progression, true, progressionObject.ProgressionDescription, path, "", progressionObject.DefaultSeqType, progressionObject.SelectTypePerSegment));

            //Generate Melodies
            //currentSeqPair = seqPairs.OrderBy(x=>Guid.NewGuid()).First();
            currentSeqPair = seqPairs.GetShuffled().First();
            seq = GenerateRandomSequence(currentSeqPair.Item1, Convert.ToInt32(Math.Pow(2, GetRandomNumberList(1, 4).First())));
            sbResults.AppendLine("Sequenence 2:" + seq);
            sbResults.AppendLine("MidiFile:" + GenerateMidiFile(seq, currentSeqPair.Item2, progressionObject.Progression, false, progressionObject.ProgressionDescription, path, "", progressionObject.DefaultSeqType, progressionObject.SelectTypePerSegment));

            seq = seq.ShuffleSegments(4);
            sbResults.AppendLine("Sequenence 2 Remixed:" + seq);
            sbResults.AppendLine("MidiFile:" + GenerateMidiFile(seq, currentSeqPair.Item2, progressionObject.Progression, false, progressionObject.ProgressionDescription, path, "", progressionObject.DefaultSeqType, progressionObject.SelectTypePerSegment));

            //currentSeqPair = seqPairs.OrderBy(x=>Guid.NewGuid()).First();
            currentSeqPair = seqPairs.GetShuffled().First();
            seq = GenerateRandomSequence(currentSeqPair.Item1, Convert.ToInt32(Math.Pow(2, GetRandomNumberList(1, 4).First())));
            sbResults.AppendLine("Sequenence 3:" + seq);
            sbResults.AppendLine("MidiFile:" + GenerateMidiFile(seq, currentSeqPair.Item2, progressionObject.Progression, false, progressionObject.ProgressionDescription, path, "", progressionObject.DefaultSeqType, progressionObject.SelectTypePerSegment));

            seq = seq.ShuffleSegments(4);
            sbResults.AppendLine("Sequenence 3 Remixed:" + seq);
            sbResults.AppendLine("MidiFile:" + GenerateMidiFile(seq, currentSeqPair.Item2, progressionObject.Progression, false, progressionObject.ProgressionDescription, path, "", progressionObject.DefaultSeqType, progressionObject.SelectTypePerSegment));

            //currentSeqPair = seqPairs.OrdefrBy(x=>Guid.NewGuid()).First();
            currentSeqPair = seqPairs.GetShuffled().First();
            seq = GenerateRandomSequence(currentSeqPair.Item1, Convert.ToInt32(Math.Pow(2, GetRandomNumberList(1, 4).First())));
            sbResults.AppendLine("Sequenence 4:" + seq);
            sbResults.AppendLine("MidiFile:" + GenerateMidiFile(seq, currentSeqPair.Item2, progressionObject.Progression, false, progressionObject.ProgressionDescription, path, "", progressionObject.DefaultSeqType, progressionObject.SelectTypePerSegment));

            seq = seq.ShuffleSegments(4);
            sbResults.AppendLine("Sequenence 4 Remixed:" + seq);
            sbResults.AppendLine("MidiFile:" + GenerateMidiFile(seq, currentSeqPair.Item2, progressionObject.Progression, false, progressionObject.ProgressionDescription, path, "", progressionObject.DefaultSeqType, progressionObject.SelectTypePerSegment));

            sbResults.AppendLine("Progression Preset:  " + progressionObject.ProgressionPreset.ToString());

            Console.WriteLine(sbResults.ToString());
            File.AppendAllText(path + "MidiFileGenerationOutputResults.txt", sbResults.ToString());
            return sbResults.ToString();

        }

        public static ProgressionResult GenerateChordProgression(ChordProgressionRequest request)
        {
            var prgObj = new ProgressionResult();
            prgObj.Progression = new List<Chord>();

        

            if (request.ChordList.Count == 0 )
            {
                request.ChordList = new List<String>();

                //presetChordList = new List<String> { "Am", "F", "Am", "C" };
            }

            //SequenceType seqType = (SequenceType)GetRandomNumberList(0,8).First();
            //int seqType = GetRandomNumberList(0,8).First();
            prgObj.DefaultSeqType = -1;
            //int seqType = (int)SequenceType.DownUp;
            prgObj.SelectTypePerSegment = false;
            request.ChordList.ForEach(x =>
            {
                prgObj.Progression.Add(Chord.Parse(x));
            });


            var n = GetRandomNumberList(200, 1000).First().ToString();

            //initial chord when no preset is supplied
            int rndChord = GetRandomNumberList(1, 12).First();
           /* if (!string.IsNullOrWhiteSpace(rootNote))
            {
                rndChord = //convert root Note to chord
            }*/                                                                                                      
            
            int rndChordRefresher = GetRandomNumberList(200, 1000).First();

            if (prgObj.Progression.Count == 0)
            {
                var chordList = new List<int> { 1, Convert.ToInt32(n[0]), Convert.ToInt32(n[1]), Convert.ToInt32(n[2]) };
                var tempList = chordList;
                if (rndChordRefresher % 8 == 0)
                {
                    /*chordList.AddRange(chordList.OrderBy(x => Guid.NewGuid()).Take(2).ToList());
                    chordList.AddRange(chordList.OrderBy(x => Guid.NewGuid()).Take(2).ToList());*/


                    chordList.AddRange(GetRandomNumberList(1, 8).Take(4).ToList());
                    //chordList.AddRange(tempList.GetShuffled().Take(4).ToList());
                    //chordList.AddRange(tempList.GetShuffled().Take(2).ToList());
                }
                else if (rndChordRefresher % 6 == 0)
                {
                    //chordList.AddRange(chordList.OrderBy(x => Guid.NewGuid()).ToList());
                    chordList.AddRange(tempList.GetShuffled());
                }
                else if (rndChordRefresher % 5 == 0)
                {
                    chordList.AddRange(chordList);
                }


                chordList = chordList.GetShuffled(1).ToList();


                List<ChordQuality> majorMinor = new List<Melanchall.DryWetMidi.MusicTheory.ChordQuality> { ChordQuality.Major, ChordQuality.Minor };

                prgObj.RootQuality = (request.ChordQuality != null) ? request.ChordQuality.Value : majorMinor.GetShuffled().First();
                prgObj.RootNote = String.IsNullOrWhiteSpace(request.RootNote) ? ((NoteName)rndChord).ToString() : Note.Parse(request.RootNote).NoteName.ToString();

                //progression = CreateProgression((NoteName)rndChord, majorMinor.OrderBy(x => Guid.NewGuid()).First(), chordList);
                prgObj.Progression = CreateProgression(Note.Parse(prgObj.RootNote+"0").NoteName, prgObj.RootQuality, chordList);
            }

            var progPreset = new StringBuilder();
            progPreset.Append("{");
            prgObj.ChordList = new List<String>();
            prgObj.Progression.ForEach(x =>
            {

                progPreset.Append("\"" + x.GetNames().First() + "\",");
                prgObj.ChordList.Add(x.GetNames().First());
                /*progressionPreset.Append("{");
                x..ToList().ForEach(note => {
                progressionPreset.Append("\"" + note+"\",");
                });
                progressionPreset.Remove(progressionPreset.Length-1,1);
                progressionPreset.Append("},");*/
            });
            progPreset.Remove(progPreset.Length - 1, 1);
            progPreset.Append("}");

            prgObj.ProgressionPreset = progPreset.ToString();


            Console.WriteLine("Progression Preset: " + prgObj.ProgressionPreset);
            prgObj.ProgressionDescription = string.Empty;
            foreach (var c in prgObj.Progression)
            {
                prgObj.ProgressionDescription += string.IsNullOrWhiteSpace(prgObj.ProgressionDescription) ? c.GetNames().First().Replace("#", "(Sharp)") : "-" + c.GetNames().First().Replace("#", "(Sharp)");
            }

            var sbResults = new StringBuilder();
            sbResults.AppendLine("Random Number:" + n);
            sbResults.AppendLine("Progression:" + prgObj.ProgressionDescription);
            sbResults.AppendLine("RootNote:" + prgObj.RootNote);

            prgObj.GenerationResults = sbResults.ToString();
            return prgObj;
        }

        public static List<int> GetRandomNumberList(int min, int max, int multiple = 1)
        {
            List<int> numberpool = new List<int>();
            for (int i = min; i < max; i += multiple)
            {
                numberpool.Add(i);
            }
            numberpool.Shuffle();
            return numberpool;//numberpool.OrderBy(x=> Guid.NewGuid()).ToList();
        }

        public static List<Chord> CreateProgression(NoteName rootNote, ChordQuality quality, List<int> chords)
        {
            var progression = new List<Chord>();
            int[] majorProgressionChords = { 0, 2, 4, 5, 7, 9, 11 };
            int[] majorProgressionQualities = { 0, 1, 1, 0, 0, 1, 3 };

            int[] minorProgressionChords = { 0, 2, 3, 5, 7, 8, 10 };
            int[] minorProgressionQualities = { 1, 3, 0, 1, 1, 0, 0 };


            foreach (int i in chords)
            {
                int currentChord = i - 1;
                switch (quality)
                {
                    case ChordQuality.Major:
                        progression.Add(CreateChord((NoteName)(((int)rootNote + majorProgressionChords[currentChord % 7]) % 12), (ChordQuality)majorProgressionQualities[currentChord % 7]));
                        break;
                    case ChordQuality.Minor:
                        progression.Add(CreateChord((NoteName)(((int)rootNote + minorProgressionChords[currentChord % 7]) % 12), (ChordQuality)minorProgressionQualities[currentChord % 7]));
                        break;
                }

            }

           /* foreach (Chord chrd in progression)
            {
                Console.WriteLine(chrd.GetNames().First());
            }*/

            return progression;
        }

        public static Chord CreateChord(NoteName name, ChordQuality quality)
        {

            Chord chord = new Melanchall.DryWetMidi.MusicTheory.Chord(name, new List<Interval> { Interval.Zero, Interval.Four, Interval.Seven });
            switch (quality)
            {

                case ChordQuality.Major:
                    chord = new Melanchall.DryWetMidi.MusicTheory.Chord(name, new List<Interval> { Interval.Zero, Interval.Four, Interval.Seven });
                    break;

                case ChordQuality.Minor:
                    chord = new Melanchall.DryWetMidi.MusicTheory.Chord(name, new List<Interval> { Interval.Zero, Interval.Three, Interval.Seven });
                    break;

                case ChordQuality.Augmented:
                    chord = new Melanchall.DryWetMidi.MusicTheory.Chord(name, new List<Interval> { Interval.Zero, Interval.Four, Interval.Eight });
                    break;

                case ChordQuality.Diminished:
                    chord = new Melanchall.DryWetMidi.MusicTheory.Chord(name, new List<Interval> { Interval.Zero, Interval.Three, Interval.Six });
                    break;

            }

            return chord;
        }

        public static string GenerateRandomSequence(int length, int segmentCount)
        {

            int standardLength = 64;

            string rhythm = string.Empty;
            if (length <= 16)
            {
                segmentCount = 2;
            }

            int segmentSize;
            segmentSize = (length / segmentCount);


            for (int i = 0; i < segmentCount; i++)
            {
                rhythm += GenerateSequence(GetRandomNumberList(Math.Min(2, segmentSize / 2), segmentSize).First(), segmentSize);
            }
            Console.WriteLine("Length:" + length + " Segments:" + segmentCount + " Rhythm: " + rhythm);


            if (length < standardLength)
            {
                int spacerSize = (standardLength / rhythm.Length) - 1;
                string expanded = string.Empty;

                foreach (char c in rhythm)
                {
                    expanded += c;
                    for (int i = 0; i < spacerSize; i++)
                    {
                        expanded += '.';
                    }
                }
                Console.WriteLine("Expanded: " + expanded);
                return expanded;
            }
            else
            {
                return rhythm;
            }
        }

        public static string GenerateSequence(int onNotes, int length)
        {
            //1000100100100100

            //generate initial array


            List<string> arr = new List<string>();
            for (int i = 0; i < length; i++)
            {
                arr.Add((i < onNotes) ? "x" : ".");
            }
            //arr.Dump();
            //move last zero to first availible spot
            while (arr.Count > 1)
            {
                int last = arr.Count - 1;
                for (int i = 0; i < arr.Count; i++)
                {
                    arr[i] = (arr[i] + arr[last - i]);
                    arr.RemoveAt(last - i);
                }
            }
            return arr[0];
        }

        public static string GenerateMidiFile(string rhythm, int noteLength, List<Chord> progression, bool chords, string progressionDescription, string path, string fileName = "", int seqType = -1, bool selectTypePerSegment = false)
        {
        
            var chunkListResult = GenerateMidiChunkList(new MidiChunkListRequest { isChords = chords, Rhythm = rhythm, Progression=progression, NoteLength = noteLength, SelectTypePerSegment = selectTypePerSegment, SeqType = seqType });
            
            return WriteMidiFile(chords, progressionDescription, path, ref fileName, chunkListResult.TrackChunks, chunkListResult.NotePattern);
        }

        public static MidiChunkListResult GenerateMidiChunkList(MidiChunkListRequest request )
        {
            var chunks = new List<TrackChunk>();
            SequencePatternList notes;

            chunks.Add(new TrackChunk(new SetTempoEvent(500000)));
            int currentChord = 0;
            /*
            int positionSpacer = 1;

            if (rhythm.Length < progression.Count)
            {
                positionSpacer = 2;
            }*/
            var type = (SequenceType)0;

            if (request.SeqType == -1)
            {
                type = (SequenceType)GetRandomNumberList(0, 8).First();
            }
            else
            {
                type = (SequenceType)request.SeqType;
            }

            notes = new SequencePatternList(request.Progression[currentChord].ResolveNotes(Octave.Get(4)).ToList().Select(x => (int)x.NoteNumber).ToList(), type);
            for (int i = 0; i < request.Rhythm.Length; i++)
            {

                if (request.Rhythm.ToCharArray()[i] == 'x')
                {
                    int noteAdd = 0;
                    decimal position = i / (request.Rhythm.ToCharArray().Length / request.Progression.Count);
                    {
                        currentChord = Convert.ToInt16(Math.Floor(position));
                        if (request.SeqType == -1 && request.SelectTypePerSegment)
                        {
                            type = (SequenceType)GetRandomNumberList(0, 8).First();
                        }

                        notes = new SequencePatternList(request.Progression[currentChord].ResolveNotes(Octave.Get(4)).ToList().Select(x => (int)x.NoteNumber).ToList(), type);
                    }



                    /*if (i == 0 || ((rhythm.ToCharArray().Length / progression.Count) % i) == 0)
                    {
                        //noteAdd = progression[currentChord].ResolveNotes(Octave.Get(4)).Skip(1).ToList().First().NoteNumber;
                        noteAdd = progression[currentChord].ResolveNotes(Octave.Get(4)).Skip(1).ToList().First().NoteNumber;
                        //noteAdd.Dump();
                    }
                    else
                    {

                        //currentProgression("Current chord: " + Math.Floor(position)).Dump();
                        noteAdd = progression[currentChord].ResolveNotes(Octave.Get(4)).Skip(1).ToList().GetShuffled().First().NoteNumber;


                        //noteAdd.Dump();
                    }*/

                    if ((i == 0 || ((request.Rhythm.ToCharArray().Length / request.Progression.Count) % i) == 0) && notes.SeqType == SequenceType.Random)
                    {
                        noteAdd = request.Progression[currentChord].ResolveNotes(Octave.Get(4)).Skip(1).ToList().First().NoteNumber;
                    }
                    else
                    {
                        noteAdd = notes.GetNext();
                    }

                    if (request.isChords)
                    {
                        foreach (Note note in request.Progression[currentChord].ResolveNotes(Octave.Get(4)))
                        {
                            chunks.Add(CreateMidiTrackChunk(note.NoteNumber, i, request.NoteLength));
                        }
                    }
                    else
                    {
                        chunks.Add(CreateMidiTrackChunk(noteAdd, i, request.NoteLength));
                    }

                }
            } 
            
            return new MidiChunkListResult { Request = request, NotePattern = notes, TrackChunks = chunks };
        }

        public static string WriteMidiFile(bool chords, string progressionDescription, string path, ref string fileName, List<TrackChunk> chunks, SequencePatternList notes)
            {
                if (string.IsNullOrWhiteSpace(fileName))
                {
                    fileName = "MidiGenCode-" + (chords ? "Chords" : notes.SeqType.ToString()) + "-" + progressionDescription + "-" + Guid.NewGuid() + ".mid";
                }
                else
                {
                    fileName = (chords ? "Chords" : notes.SeqType.ToString()) + fileName;
                }
                var midiFile = new MidiFile(chunks.AsEnumerable().Merge());
                //midiFile.Chunks.Dump();
                midiFile.Write(path + fileName, true);
                return path + fileName;
            }

        public static TrackChunk CreateMidiTrackChunk(int noteAdd, int position, int noteLength = 48)
        {
            //int rootNote = 60;
            int rootNote = 0;

            return new TrackChunk(
                    new TextEvent(""),
                    new NoteOnEvent((SevenBitNumber)(rootNote + noteAdd), (SevenBitNumber)127) { DeltaTime = 48 * position },
                    new NoteOffEvent((SevenBitNumber)(rootNote + noteAdd), (SevenBitNumber)0)
                    {
                        DeltaTime = 48
                    });
        }

     /*
      * 
      *
      *
      
        //Euclid experiment

        int euclid(int m, int k)
        {
            var createRhythResult = createRhythm(k, m);
            Console.WriteLine(createRhythResult);
            Console.WriteLine("M = " + m + ", K = " + k);
            //Console.WriteLine(createRhythm(m,k));
            if (k == 0)
            {

                return m;
            }
            else
            {

                return euclid(k, m % k);
            }
        }

        string createRhythm(int offNotes, int onNotes)
        {

            StringBuilder sb = new StringBuilder();



            int onTemp = onNotes;
            int offTemp = offNotes;
            int totalNotes = onNotes + offNotes;
            int remainder = totalNotes % onNotes;
            int quo = totalNotes / onNotes;
            sb.Append("remainder:" + remainder);
            sb.Append("quo:" + quo);


            while (onTemp > 0)
            {
                if (totalNotes % onTemp < quo)
                {
                    sb.Append(".");
                }
                else
                {
                    sb.Append("x");
                }
                //sb.Append("x");
                onTemp--;
            }



            return sb.ToString();
        }
   

    */

 }
  
}
