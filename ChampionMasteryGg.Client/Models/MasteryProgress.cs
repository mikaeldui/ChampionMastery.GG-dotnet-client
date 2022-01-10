using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionMasteryGg
{
    public interface IMasteryProgress : IChampionMasteryGgObject
    {
        string ToShortString();
    }

    public struct MasteryProgressMastered : IMasteryProgress
    {
        public override string ToString() => "Mastered";

        public string ToShortString() => ToString();
    }

    public struct MasteryProgressTokens : IMasteryProgress
    {
        public MasteryProgressTokens(int has, int total)
        {
            Has = has;
            Total = total;
        }

        public int Has { get; }
        public int Total { get; }

        public override string ToString() => $"{Has}/{Total} tokens";

        public string ToShortString() => ToString();
    }

    public struct MasteryProgressPoints : IMasteryProgress
    {
        public MasteryProgressPoints(int has, int total)
        {
            Has = has;
            Total = total;
        }

        public int Has { get; }
        public int Total { get; }

        public override string ToString() => $"{Has}/{Total} points ({(float)Has / Total:P2})";

        public string ToShortString() => $"{(float)Has / Total:P2}";
    }
}
