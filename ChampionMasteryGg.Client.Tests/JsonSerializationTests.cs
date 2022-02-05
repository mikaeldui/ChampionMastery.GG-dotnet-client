using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChampionMasteryGg
{
    [TestClass]
    public class JsonSerializationTests
    {
        [TestMethod]
        public void Highscores()
        {
            using ChampionMasteryGgClient client = new();
            Task<Highscores>? highscores = client.GetHighscoresAsync();
            Assert.IsNotNull(highscores);

            var json = JsonSerializer.Serialize(highscores);
            Assert.IsNotNull(json);
            Assert.IsFalse(string.IsNullOrWhiteSpace(json));
        }
    }
}
