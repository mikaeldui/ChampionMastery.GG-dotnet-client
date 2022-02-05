using System.Diagnostics;

namespace ChampionMasteryGg
{
    [DebuggerDisplay("Summoner = {Summoner} Level = {Level}")]
    public struct LevelHighscore : IHighscore, IEquatable<LevelHighscore>
    {
        public LevelHighscore(Summoner summoner, int level)
        {
            Summoner = summoner;
            Level = level;
        }

        public Summoner Summoner { get; }

        public int Level { get; }

        int IHighscore.Score => Level;

        public bool Equals(LevelHighscore other) => Summoner.Equals(other.Summoner) && Level.Equals(other.Level);

        public override bool Equals(object? obj) => obj != null && GetType() == obj.GetType() && Equals((LevelHighscore)obj);

        public override int GetHashCode()
        {
            int hashCode = -666246286;
            hashCode = hashCode * -1521134295 + Summoner.GetHashCode();
            hashCode = hashCode * -1521134295 + Level.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(LevelHighscore c1, LevelHighscore c2) => c1.Equals(c2);

        public static bool operator !=(LevelHighscore c1, LevelHighscore c2) => !c1.Equals(c2);
    }
}