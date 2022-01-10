using System;

namespace ChampionMasteryGg
{
    public struct Mastery : IChampionMasteryGgObject
    {
        public Mastery(Champion champion, int level, int points, bool isChestEarned, DateTime lastPlayed, IMasteryProgress progress)
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

        public Champion Champion { get; }
        public int Level { get; }
        public int Points { get; }
        public bool IsChestEarned { get; }
        public DateTime LastPlayed { get; }
        public IMasteryProgress Progress { get; }
        public int? PointsToNextLevel
        {
            get
            {
                if (Progress is MasteryProgressPoints p)
                    return p.Total - p.Has;

                return null;
            }
        }
    }
}