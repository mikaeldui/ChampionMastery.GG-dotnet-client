using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChampionMasteryGg.Client.Tests
{
    [TestClass]
    public class ConstructorTests
    {
        [TestMethod]
        public void ConstructClient()
        {
            _ = new ChampionMasteryGgClient();
        }
    }
}