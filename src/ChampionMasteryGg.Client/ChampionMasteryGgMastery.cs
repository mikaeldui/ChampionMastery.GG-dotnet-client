using System;

namespace ChampionMasteryGg
{
    public struct ChampionMasteryGgChampionMastery
    {
        public ChampionMasteryGgChampionMastery(string champion, int level, int points, bool isChestEarned, DateTime lastPlayed, IChampionMasteryGgChampionMasteryProgress progress)
        {
            if (progress == null)
                throw new ArgumentNullException(nameof(progress));

            Champion = champion;
            Level = level;
            Points = points;
            IsChestEarned = isChestEarned;
            LastPlayed = lastPlayed;
            Progress = progress;
        }

        public string Champion { get; }
        public int Level { get; }
        public int Points { get; }
        public bool IsChestEarned { get; }
        public DateTime LastPlayed { get; }
        public IChampionMasteryGgChampionMasteryProgress Progress { get; }
        public int? PointsToNextLevel
        {
            get
            {
                if (Progress is ChampionMasteryGgChampionMasteryProgressPoints p)
                    return p.Total - p.Has;

                return null;
            }
        }
    }
}