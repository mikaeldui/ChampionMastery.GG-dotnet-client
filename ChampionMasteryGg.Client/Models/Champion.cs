using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.WebUtility;

namespace ChampionMasteryGg
{
    public struct Champion : IChampionMasteryGgObject, IEquatable<Champion>
    {
        public Champion(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        internal static Champion FromLinkNode(HtmlNode linkNode)
        {
            var id = linkNode.Attributes["href"].Value.LastDigits();

            string name;
            if (linkNode.Descendants("strong").Any())
                name = linkNode.Descendants("strong").First().InnerText;
            else
                name = linkNode.InnerText;

            name = HtmlDecode(name);

            return new Champion(id, name);
        }

        #region Equality

        public bool Equals(Champion other) => Name.Equals(other.Name) && Id.Equals(other.Id);

        public override bool Equals(object obj) => obj != null && GetType() == obj.GetType() && Equals((Champion)obj);

        public override int GetHashCode() => (Id + Name).GetHashCode();

        public override string ToString() => Name ?? base.ToString();

        public static bool operator == (Champion c1, Champion c2) => c1.Equals(c2);

        public static bool operator != (Champion c1, Champion c2) => !c1.Equals(c2);

        #endregion Equality
    }
}
