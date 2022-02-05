using System.Diagnostics;

namespace ChampionMasteryGg
{
    [DebuggerDisplay("Summoner = {Summoner} Points = {Points}")]
    public struct PointsHighscore : IHighscore, IEquatable<PointsHighscore>
    {
        public PointsHighscore(Summoner summoner, int points)
        {
            Summoner = summoner;
            Points = points;
        }

        public Summoner Summoner { get; set; }

        public int Points { get; set; }

        int IHighscore.Score => Points;

        public bool Equals(PointsHighscore other) => Summoner.Equals(other.Summoner) && Points.Equals(other.Points);

        public override bool Equals(object? obj) => obj != null && GetType() == obj.GetType() && Equals((PointsHighscore)obj);

        public override int GetHashCode()
        {
            int hashCode = 1088761563;
            hashCode = hashCode * -1521134295 + Summoner.GetHashCode();
            hashCode = hashCode * -1521134295 + Points.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(PointsHighscore c1, PointsHighscore c2) => c1.Equals(c2);

        public static bool operator !=(PointsHighscore c1, PointsHighscore c2) => !c1.Equals(c2);

    }
}