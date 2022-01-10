using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionMasteryGg
{
    public interface IChampionMasteryGgChampionMasteryProgress
    {
        string ToShortString();
    }

    public struct ChampionMasteryGgChampionMasteryProgressMastered : IChampionMasteryGgChampionMasteryProgress
    {
        public override string ToString() => "Mastered";

        public string ToShortString() => ToString();
    }

    public struct ChampionMasteryGgChampionMasteryProgressTokens : IChampionMasteryGgChampionMasteryProgress
    {
        public ChampionMasteryGgChampionMasteryProgressTokens(int has, int total)
        {
            Has = has;
            Total = total;
        }

        public int Has { get; }
        public int Total { get; }

        public override string ToString() => $"{Has}/{Total} tokens";

        public string ToShortString() => ToString();
    }

    public struct ChampionMasteryGgChampionMasteryProgressPoints : IChampionMasteryGgChampionMasteryProgress
    {
        public ChampionMasteryGgChampionMasteryProgressPoints(int has, int total)
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
