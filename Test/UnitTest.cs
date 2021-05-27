using Microsoft.VisualStudio.TestTools.UnitTesting;
using PsnApiWrapperNet;
using PsnApiWrapperNet.Model;

namespace Test
{
    [TestClass]
    public class UnitTest
    {
        private static readonly string _accountId = "1882371903386905898"; // Ragowit
        private static readonly string _npsso = ""; // This needs to be refreshed with an active npsso value when you test.
        private static PAWN _psn;

        [ClassInitialize]
        public static void PAWNInitialize(TestContext testContext)
        {
            _psn = new PAWN(_npsso);
        }

        [TestMethod]
        public void TestGameList()
        {
            var result = _psn.GameList(_accountId);

            Assert.IsTrue(result.titles.Count == 10);
        }

        [TestMethod]
        public void TestPlayer()
        {
            Player result = _psn.Player(_accountId);

            Assert.AreEqual("Ragowit", result.onlineId);
        }

        [TestMethod]
        public void TestPlayerSummary()
        {
            PlayerSummary result = _psn.PlayerSummary(_accountId);

            Assert.AreEqual("1882371903386905898", result.accountId);
        }

        [TestMethod]
        public void TestPlayerTitles()
        {
            PlayerTitles result = _psn.PlayerTitles(_accountId, "CUSA02818_00,CUSA01976_00");

            Assert.IsTrue(result.titles.Count == 2);
        }

        [TestMethod]
        public void TestPlayerTrophies()
        {
            PlayerTrophies result = _psn.PlayerTrophies(_accountId, "NPWR09206_00", "default", "trophy"); // Gems of War

            Assert.IsTrue(result.trophies.Count == 18);
        }

        [TestMethod]
        public void TestPlayerTrophyGroups()
        {
            PlayerTrophyGroups result = _psn.PlayerTrophyGroups(_accountId, "NPWR09206_00", "trophy"); // Gems of War

            Assert.IsTrue(result.trophyGroups.Count >= 27);
        }

        [TestMethod]
        public void TestPlayerTrophyTitles()
        {
            PlayerTrophyTitles result = _psn.PlayerTrophyTitles(_accountId);

            Assert.IsTrue(result.trophyTitles.Count == 100);
        }

        [TestMethod]
        public void TestSearchPlayer()
        {
            SocialMetadata result = _psn.SearchPlayer("Ragowit").FirstResult();

            Assert.AreEqual("1882371903386905898", result.accountId);
            Assert.AreEqual("Ragowit", result.onlineId);
        }

        [TestMethod]
        public void TestTrophies()
        {
            TitleTrophies result = _psn.TitleTrophies("NPWR09206_00", "default", "trophy"); // Gems of War

            Assert.IsTrue(result.trophies.Count == 18);
        }

        [TestMethod]
        public void TestTrophies_PS5WithReward()
        {
            TitleTrophies result = _psn.TitleTrophies("NPWR22792_00", "default", "trophy2"); // Destruction AllStars

            Assert.AreEqual("21", result.trophies[2].trophyProgressTargetValue);
            Assert.AreEqual("Emote", result.trophies[2].trophyRewardName);
        }

        [TestMethod]
        public void TestTrophyGroups()
        {
            TitleTrophyGroups result = _psn.TitleTrophyGroups("NPWR09206_00", "trophy"); // Gems of War

            Assert.IsTrue(result.trophyGroups.Count >= 27);
        }
    }
}
