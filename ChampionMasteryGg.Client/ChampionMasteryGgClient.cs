using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using ChampionMasteryGg.Parsers;

namespace ChampionMasteryGg
{
    public class ChampionMasteryGgClient : ChampionMasteryGgObject, IDisposable
    {
        private static readonly string USER_AGENT;

        private static (Highscores Highscores, DateTime Downloaded)? _highscores;
        private static (HighscoresCollection<LevelHighscore> Highscores, DateTime Downloaded)? _totalLevelHighscores;
        private static readonly Dictionary<int, (HighscoresCollection<PointsHighscore> Highscores, DateTime Downloaded)> _championHighscores = new();

        static ChampionMasteryGgClient()
        {
            var client = UserAgent.From(typeof(ChampionMasteryGgClient).GetTypeInfo().Assembly);

            try
            {
                var entryAssembly = Assembly.GetEntryAssembly();
                if (entryAssembly != null)
                    client.DependentProduct = UserAgent.From(entryAssembly);
            }
            catch { }

            USER_AGENT = client.ToString();
        }

        private readonly HttpClient _httpClient;

        public ChampionMasteryGgClient()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("User-Agent", USER_AGENT);
        }

        public async Task<Highscores> GetHighscoresAsync()
        {
            try
            {
                if (_highscores != null && (_highscores.Value.Downloaded - DateTime.Now).Hours < 24)
                    return _highscores.Value.Highscores;
                else
                {
                    var html = await _httpClient.GetStringAsync("https://championmastery.gg/highscores");
                    var highscores = await HighscoresParser.ParseAsync(html);
                    _highscores = (highscores, DateTime.Now);
                    return highscores;
                }
            }
            catch (Exception ex)
            {
                throw new ChampionMasteryGgException($"Something went wrong getting the highscores.", ex);
            }
        }

        public async Task<HighscoresCollection<PointsHighscore>> GetTotalPointsHighscoresAsync() => await GetHighscoresAsync(-1);

        public async Task<HighscoresCollection<LevelHighscore>> GetTotalLevelHighscoresAsync()
        {
            try
            {
                if (_totalLevelHighscores != null && (_totalLevelHighscores.Value.Downloaded - DateTime.Now).Hours < 24)
                    return _totalLevelHighscores.Value.Highscores;
                else
                {
                    var html = await _httpClient.GetStringAsync("https://championmastery.gg/champion?champion=-2");
                    var highscores = await LevelHighscoresParser.ParseAsync(html);
                    _totalLevelHighscores = (highscores, DateTime.Now);
                    return highscores;
                }
            }
            catch (Exception ex)
            {
                throw new ChampionMasteryGgException($"Something went wrong getting the total level highscores.", ex);
            }
        }

        public async Task<HighscoresCollection<PointsHighscore>> GetHighscoresAsync(int championId)
        {
            try
            {
                if (_championHighscores.TryGetValue(championId, out var cachedHighscore) && (cachedHighscore.Downloaded - DateTime.Now).Hours < 24)
                    return cachedHighscore.Highscores;
                else
                {
                    var html = await _httpClient.GetStringAsync($"https://championmastery.gg/champion?champion={championId}");
                    var highscores = await PointsHighscoresParser.ParseAsync(html);
                    _championHighscores[championId] = (highscores, DateTime.Now);
                    return highscores;
                }
            }
            catch (Exception ex)
            {
                throw new ChampionMasteryGgException($"Something went wrong getting the highscores.", ex);
            }
        }

        public async Task<HighscoresCollection<PointsHighscore>> GetHighscoresAsync(Champion champion) => await GetHighscoresAsync(champion.Id);

        public void Dispose() => _httpClient.Dispose();
    }
}