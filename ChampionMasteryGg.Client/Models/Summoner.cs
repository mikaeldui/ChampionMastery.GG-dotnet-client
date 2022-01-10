using HtmlAgilityPack;
using System.Net;

namespace ChampionMasteryGg
{
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

            return new Summoner(region, name);
        }

        #region Equality

        public bool Equals(Summoner other) => Name.Equals(other.Name) && Region.Equals(other.Region);

        public override bool Equals(object obj) => obj != null && GetType() == obj.GetType() && Equals((Champion)obj);

        public override int GetHashCode() => (Region + Name).GetHashCode();

        public override string ToString() => $"Name ({Region})";

        public static bool operator ==(Summoner c1, Summoner c2) => c1.Equals(c2);

        public static bool operator !=(Summoner c1, Summoner c2) => !c1.Equals(c2);

        #endregion Equality
    }
}