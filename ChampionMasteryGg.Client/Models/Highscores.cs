using System.Diagnostics;

namespace ChampionMasteryGg
{
    [DebuggerDisplay("Total Points = {TotalPoints.Count} Total Level = {TotalLevel.Count} Champions = {Champions.Count}")]
    public sealed class Highscores
    {
        internal Highscores(PointsHighscoresCollection totalPoints, LevelHighscoresCollection totalLevel, ChampionHighscoresDictionary champions)
        {
            TotalPoints = totalPoints;
            TotalLevel = totalLevel;
            Champions = champions;
        }

        public PointsHighscoresCollection TotalPoints { get; }

        public LevelHighscoresCollection TotalLevel { get; }

        public ChampionHighscoresDictionary Champions { get; }

        /// <summary>
        /// Includes the negative ID's -1 and -2. 0 can't be used.
        /// </summary>
        public IHighscoresCollection<IHighscore> this[int championId]
        {
            get 
            {
                if (championId == -1)
                    return (IHighscoresCollection<IHighscore>) TotalPoints;

                if (championId == -2)
                    return (IHighscoresCollection<IHighscore>) TotalLevel;

                if (championId < -2 || championId == 0)
                    throw new ChampionMasteryGgException("Bad index!", new IndexOutOfRangeException());

                return (IHighscoresCollection<IHighscore>) Champions.Single(c => c.Key.Id == championId).Value; 
            }
        }

        /// <summary>
        /// Includes the negative ID's -1 and -2.
        /// </summary>
        public IHighscore this[int championId, int positionIndex] => this[championId][positionIndex];
    }
}