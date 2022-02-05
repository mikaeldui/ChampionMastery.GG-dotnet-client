using System.Diagnostics;
using System.Text.Json.Serialization;
using System.Linq;
using System.ComponentModel;

namespace ChampionMasteryGg
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IHighscoresCollection<THighscores> : IChampionMasterGgCollection<THighscores>
        where THighscores : IHighscore
    {

    }

    [EditorBrowsable(EditorBrowsableState.Never), DebuggerDisplay("Highscores = {Count}")]
    public abstract class HighscoresCollection<THighscore> : ChampionMasteryGgCollection<THighscore>, IHighscoresCollection<THighscore>
        where THighscore : IHighscore
    {
        public HighscoresCollection(IList<THighscore> list) : base(list)
        {
        }

        public int IndexOf(Summoner summoner) => this.IndexOf(this.Single(h => h.Summoner == summoner));

        public int IndexOf(string summonerName, string region) => this.IndexOf(this.Single(h => h.Summoner.Name == summonerName && h.Summoner.Region == region));

        public virtual THighscore this[Summoner summoner] => this.Single(h => h.Summoner == summoner);

        public virtual THighscore this[string summonerName, string region] => this.Single(h => h.Summoner.Name == summonerName && h.Summoner.Region == region);
    }
}