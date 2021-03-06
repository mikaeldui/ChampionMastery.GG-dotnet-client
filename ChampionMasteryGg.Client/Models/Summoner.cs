using HtmlAgilityPack;
using System.Diagnostics;
using System.Net;

namespace ChampionMasteryGg
{
    [DebuggerDisplay("Name = {Name} Region = {Region}")]
    public struct Summoner : IChampionMasteryGgObject, IEquatable<Summoner>
    {
        public Summoner(string region, string name)
        {
            Region = region;
            Name = name;
        }

        public string Region { get; set; }

        public string Name { get; set; }

        internal static Summoner FromLinkNode(HtmlNode linkNode)
        {
            var link = linkNode.Attributes["href"].Value;

            string region = link.AfterLast('=');
            string name = WebUtility.UrlDecode(link.Between("summoner=", "&region"));

            return new(region, name);
        }

        #region Equality

        public bool Equals(Summoner other) => Name.Equals(other.Name) && Region.Equals(other.Region);

        public override bool Equals(object? obj) => obj != null && GetType() == obj.GetType() && Equals((Champion)obj);

        public override int GetHashCode()
        {
            int hashCode = 1937696043;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Region);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            return hashCode;
        }

        public override string ToString() => $"{Name} ({Region})";

        public static bool operator ==(Summoner c1, Summoner c2) => c1.Equals(c2);

        public static bool operator !=(Summoner c1, Summoner c2) => !c1.Equals(c2);

        #endregion Equality
    }
}