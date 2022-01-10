using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using static ChampionMasteryGg.Parser;

namespace ChampionMasteryGg
{
    public class ChampionMasteryGgClient : ChampionMasteryGgObject, IDisposable
    {
        private static readonly string VERSION = typeof(ChampionMasteryGgClient).GetTypeInfo().Assembly.GetCustomAttribute<AssemblyFileVersionAttribute>().Version;

        private readonly HttpClient _httpClient;

        public ChampionMasteryGgClient()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("User-Agent", $"ChampionMastery.GG-dotnet-client/{VERSION} (github.com/mikaeldui/ChampionMastery.GG-dotnet-client)");
        }

        public async Task<Mastery[]> GetChampionMasteriesAsync(string username, string region)
        {
            var html = await _httpClient.GetStringAsync($"https://championmastery.gg/summoner?region={region}&summoner={Uri.EscapeDataString(username)}");
            return await ParseSummonerMasteries(html);
        }

        public async Task<Highscores> GetHighscoresAsync()
        {
            var html = await _httpClient.GetStringAsync("https://championmastery.gg/highscores");
            return await ParseHighscores(html);
        }

        public void Dispose() => _httpClient.Dispose();
    }
}