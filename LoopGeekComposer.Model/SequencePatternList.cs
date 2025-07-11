using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoopGeekComposer.Model.Extensions;

namespace LoopGeekComposer.Model
{

 
    public class SequencePatternList
    {
        public List<int> Notes { get; set; }
        public SequenceType SeqType { get; set; }
        private int _stepNumber = 0;
        bool isDown;


        public SequencePatternList(List<int> notes, SequenceType type)
        {
            Notes = notes;
            SeqType = type;
        }

        public int GetNext()
        {
            int returnNote = 0;

            switch (SeqType)
            {
                case SequenceType.Up:
                    returnNote = Notes[_stepNumber % (Notes.Count)];
                    _stepNumber++;
                    return returnNote;
                    break;

                case SequenceType.Down:
                    returnNote = Notes[(Notes.Count - 1) - (_stepNumber % (Notes.Count))];
                    _stepNumber++;
                    return returnNote;
                    break;
                case SequenceType.HalfUp:
                    returnNote = Notes[_stepNumber % (Notes.Count - 1)];
                    _stepNumber++;
                    return returnNote;
                    break;

                case SequenceType.HalfDown:
                    returnNote = Notes[(Notes.Count - 1) - (_stepNumber % (Notes.Count - 1))];
                    _stepNumber++;
                    return returnNote;
                    break;
                case SequenceType.UpDown:
                    if (isDown)
                    {
                        returnNote = Notes[(Notes.Count - 1) - (_stepNumber % (Notes.Count - 1))];
                        _stepNumber++;
                    }
                    else
                    {
                        returnNote = Notes[_stepNumber % (Notes.Count - 1)];
                        _stepNumber++;
                    }
                    if (_stepNumber % (Notes.Count - 1) == 0)
                    {
                        isDown = !isDown;
                    }
                    return returnNote;
                    break;
                case SequenceType.DownUp:
                    if (_stepNumber == 0)
                    {
                        isDown = true;
                    }
                    if (isDown)
                    {
                        returnNote = Notes[(Notes.Count - 1) - (_stepNumber % (Notes.Count - 1))];
                        _stepNumber++;
                    }
                    else
                    {
                        returnNote = Notes[_stepNumber % (Notes.Count - 1)];
                        _stepNumber++;
                    }
                    if (_stepNumber % (Notes.Count - 1) == 0)
                    {
                        isDown = !isDown;
                    }
                    return returnNote;
                    break;
                case SequenceType.AlternateIn:
                    if (isDown)
                    {
                        returnNote = Notes[(Notes.Count - 1) - (_stepNumber % (Notes.Count))];
                        _stepNumber++;
                    }
                    else
                    {
                        returnNote = Notes[_stepNumber % (Notes.Count)];
                    }
                    isDown = !isDown;
                    return returnNote;
                    break;
                case SequenceType.AlternateOut:
                    int centerNote = (int)Math.Floor((decimal)((Notes.Count - 1) / 2));

                    if (isDown)
                    {
                        returnNote = Notes[(centerNote) - (_stepNumber % (centerNote + 1))];
                        _stepNumber++;
                    }
                    else
                    {
                        returnNote = Notes[((centerNote) + (_stepNumber % (centerNote + 1)))];
                    }

                    isDown = !isDown;


                    return returnNote;
                    break;
                case SequenceType.Random:
                    return Notes.GetShuffled().First();
                    break;
            }

            return Notes.GetShuffled().First();
        }
    }
}
