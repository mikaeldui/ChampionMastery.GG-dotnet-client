using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionMasteryGg.Client.Tests
{
    [TestClass]
    public class HighscoreTests
    {
        [TestMethod]
        public async Task GetHighscore()
        {
            using var client = new ChampionMasteryGgClient();
            var highscore = await client.GetHighscoresAsync();

            Assert.AreEqual(highscore.TotalPoints.Count, 3);
        }
    }
}
