namespace ChampionMasteryGg
{
    public interface IHighscore : IChampionMasteryGgObject
    {
        public Summoner Summoner { get; }

        public int Points { get; }
    }
}