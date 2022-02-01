using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionMasteryGg.Client.Tests
{
    [TestClass]
    public class HighscoresTests
    {
        [TestMethod]
        public async Task GetHighscores()
        {
            using var client = new ChampionMasteryGgClient();
            var highscore = await client.GetHighscoresAsync();

            Assert.AreEqual(highscore.TotalPoints.Count, 3);
        }

        [TestMethod]
        public async Task GetPointsHighscores()
        {
            using ChampionMasteryGgClient client = new();
            var levelHighscores = await client.GetTotalPointsHighscoresAsync();
            Assert.IsNotNull(levelHighscores);
            Assert.AreEqual(50, levelHighscores.Count);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(levelHighscores.First().Summoner.Name));
            Assert.AreEqual(8, levelHighscores.First().Points.ToString().Length);
        }


        [TestMethod]
        public async Task GetLevelHighscores()
        {
            using ChampionMasteryGgClient client = new();
            var levelHighscores = await client.GetTotalLevelHighscoresAsync();
            Assert.IsNotNull(levelHighscores);
            Assert.AreEqual(50, levelHighscores.Count);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(levelHighscores.First().Summoner.Name));
            Assert.AreEqual(4, levelHighscores.First().Level.ToString().Length);
        }


        [TestMethod]
        public async Task GetChampionHighscores()
        {
            using ChampionMasteryGgClient client = new();
            var levelHighscores = await client.GetHighscoresAsync(1);
            Assert.IsNotNull(levelHighscores);
            Assert.AreEqual(50, levelHighscores.Count);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(levelHighscores.First().Summoner.Name));
            Assert.AreEqual(8, levelHighscores.First().Points.ToString().Length);
        }
    }
}
