namespace ChampionMasteryGg
{
    public class LevelHighscore : IHighscore
    {
        internal LevelHighscore(Summoner summoner, int level)
        {
            Summoner = summoner;
            Level = level;
        }

        public Summoner Summoner { get; }

        public int Level { get; }

        int IHighscore.Points => Level;
    }
}