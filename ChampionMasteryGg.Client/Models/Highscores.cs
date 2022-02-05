using System.ComponentModel;
using System.Diagnostics;

namespace ChampionMasteryGg
{
    [DebuggerDisplay("Total Points = {TotalPoints.Count} Total Level = {TotalLevel.Count} Champions = {Champions.Count}")]
    public class Highscores
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Highscores()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
        }

        public Highscores(PointsHighscoresCollection totalPoints, LevelHighscoresCollection totalLevel, ChampionHighscoresDictionary champions)
        {
            TotalPoints = totalPoints;
            TotalLevel = totalLevel;
            Champions = champions;
        }

        public PointsHighscoresCollection TotalPoints { get; set; }

        public LevelHighscoresCollection TotalLevel { get; set; }

        public ChampionHighscoresDictionary Champions { get; set; }

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