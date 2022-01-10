using HtmlAgilityPack;
using System.Collections;
using System.Collections.ObjectModel;
using System.Net;
using static ChampionMasteryGg.Parser;

namespace ChampionMasteryGg
{
    public class Highscores
    {
        public Highscores(PointHighscoreReadOnlyCollection totalPoints, LevelHighscoreReadOnlyCollection totalLevel, ChampionHighscoreReadOnlyDictionary champions)
        {
            TotalPoints = totalPoints;
            TotalLevel = totalLevel;
            Champions = champions;
        }

        internal Highscores(PointsHighscore[] totalPoints, LevelHighscore[] totalLevel, ChampionHighscoreReadOnlyDictionary champions) : this(new PointHighscoreReadOnlyCollection(totalPoints), new LevelHighscoreReadOnlyCollection(totalLevel), champions)
        {
        }

        public PointHighscoreReadOnlyCollection TotalPoints { get; set; }

        public LevelHighscoreReadOnlyCollection TotalLevel { get; set; }

        public ChampionHighscoreReadOnlyDictionary Champions { get; set; }
    }
}