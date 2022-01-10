namespace ChampionMasteryGg
{
    public class PointsHighscore : IHighscore
    {
        internal PointsHighscore(Summoner summoner, int points)
        {
            Summoner = summoner;
            Points = points;
        }

        public Summoner Summoner { get; }

        public int Points { get; }
    }
}