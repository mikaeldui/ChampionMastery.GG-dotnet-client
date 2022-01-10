using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionMasteryGg
{
    internal static class HtmlExtensions
    {
        public static IEnumerable<HtmlNode> ChildNodes(this HtmlNode node, string name) =>
            node.ChildNodes.Where(n => n.Name == name);
    }
}
