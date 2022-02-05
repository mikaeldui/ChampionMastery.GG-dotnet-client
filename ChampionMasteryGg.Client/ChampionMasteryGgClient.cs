using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using ChampionMasteryGg.Parsers;
using System.Net.Http.Json;

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8603 // Possible null reference return.
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.

namespace ChampionMasteryGg
{
    public class ChampionMasteryGgClient : ChampionMasteryGgObject, IDisposable
    {
        private static readonly string USER_AGENT;

        private static (Highscores Highscores, DateTime Downloaded)? _highscores;
        private static (LevelHighscoresCollection Highscores, DateTime Downloaded)? _totalLevelHighscores;
        private static readonly Dictionary<int, (PointsHighscoresCollection Highscores, DateTime Downloaded)> _championHighscores = new();

        /// <summary>
        /// Change this if you're using a proxy. Default is "https://championmastery.gg/".
        /// </summary>
        public static string BaseAddress { get; set; } = "https://championmastery.gg/";

        /// <summary>
        /// If you're using a proxy and is returning JSON complying to the schema of this library then you can set this to <see cref="ChampionMasteryGgEncoding.Json"/>.
        /// </summary>
        public static ChampionMasteryGgEncoding Encoding { get; set; } = ChampionMasteryGgEncoding.Html;

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
            _httpClient.BaseAddress = new Uri(BaseAddress);
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
                    Highscores highscores = null;
                    switch (Encoding)
                    {
                        case ChampionMasteryGgEncoding.Html:
                            var html = await _httpClient.GetStringAsync("highscores");
                            highscores = await HighscoresParser.ParseAsync(html);
                            break;
                        case ChampionMasteryGgEncoding.Json:
                            highscores = await _httpClient.GetFromJsonAsync<Highscores>("highscores");
                            break;
                    }
                    _highscores = (highscores, DateTime.Now);
                    return highscores;
                }
            }
            catch (Exception ex)
            {
                throw new ChampionMasteryGgException($"Something went wrong getting the highscores.", ex);
            }
        }

        public async Task<PointsHighscoresCollection> GetTotalPointsHighscoresAsync() => await GetHighscoresAsync(-1);

        public async Task<LevelHighscoresCollection> GetTotalLevelHighscoresAsync()
        {
            try
            {
                if (_totalLevelHighscores != null && (_totalLevelHighscores.Value.Downloaded - DateTime.Now).Hours < 24)
                    return _totalLevelHighscores.Value.Highscores;
                else
                {
                    LevelHighscoresCollection highscores = null;
                    switch (Encoding)
                    {
                        case ChampionMasteryGgEncoding.Html:
                            var html = await _httpClient.GetStringAsync("champion?champion=-2");
                            highscores = await LevelHighscoresParser.ParseAsync(html);
                            break;
                        case ChampionMasteryGgEncoding.Json:
                            highscores = await _httpClient.GetFromJsonAsync<LevelHighscoresCollection>("champion?champion=-2");
                            break;
                    }
                    _totalLevelHighscores = (highscores, DateTime.Now);
                    return highscores;
                }
            }
            catch (Exception ex)
            {
                throw new ChampionMasteryGgException($"Something went wrong getting the total level highscores.", ex);
            }
        }

        public async Task<PointsHighscoresCollection> GetHighscoresAsync(int championId)
        {
            try
            {
                if (_championHighscores.TryGetValue(championId, out var cachedHighscore) && (cachedHighscore.Downloaded - DateTime.Now).Hours < 24)
                    return cachedHighscore.Highscores;
                else
                {
                    PointsHighscoresCollection highscores = null;
                    switch (Encoding)
                    {
                        case ChampionMasteryGgEncoding.Html:
                            var html = await _httpClient.GetStringAsync($"champion?champion={championId}");
                            highscores = await PointsHighscoresParser.ParseAsync(html);
                            break;
                        case ChampionMasteryGgEncoding.Json:
                            highscores = await _httpClient.GetFromJsonAsync<PointsHighscoresCollection>($"champion?champion={championId}");
                            break;
                    }
                    _championHighscores[championId] = (highscores, DateTime.Now);
                    return highscores;
                }
            }
            catch (Exception ex)
            {
                throw new ChampionMasteryGgException($"Something went wrong getting the highscores.", ex);
            }
        }

        public async Task<PointsHighscoresCollection> GetHighscoresAsync(Champion champion) => await GetHighscoresAsync(champion.Id);

        public void Dispose() => _httpClient.Dispose();
    }
}

#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
#pragma warning restore CS8603 // Possible null reference return.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
