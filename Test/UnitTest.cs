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
            var result = _psn.GameListAsync(_accountId).Result;

            Assert.IsTrue(result.titles.Count == 10);
        }

        [TestMethod]
        public void TestGameListNextOffsetNull()
        {
            var result = _psn.GameListAsync("3342351313068632439").Result;

            Assert.IsNull(result.nextOffset);
        }

        [TestMethod]
        public void TestPlayer()
        {
            Player result = _psn.PlayerAsync(_accountId).Result;

            Assert.AreEqual("Ragowit", result.onlineId);
        }

        [TestMethod]
        public void TestPlayerSummary()
        {
            PlayerSummary result = _psn.PlayerSummaryAsync(_accountId).Result;

            Assert.AreEqual("1882371903386905898", result.accountId);
        }

        [TestMethod]
        public void TestPlayerTitles()
        {
            PlayerTitles result = _psn.PlayerTitlesAsync(_accountId, "CUSA02818_00,CUSA01976_00").Result;

            Assert.IsTrue(result.titles.Count == 2);
        }

        [TestMethod]
        public void TestPlayerTrophies()
        {
            PlayerTrophies result = _psn.PlayerTrophiesAsync(_accountId, "NPWR09206_00", "default", "trophy").Result; // Gems of War

            Assert.IsTrue(result.trophies.Count == 18);
        }

        [TestMethod]
        public void TestPlayerTrophyGroups()
        {
            PlayerTrophyGroups result = _psn.PlayerTrophyGroupsAsync(_accountId, "NPWR09206_00", "trophy").Result; // Gems of War

            Assert.IsTrue(result.trophyGroups.Count >= 27);
        }

        [TestMethod]
        public void TestPlayerTrophyTitles()
        {
            PlayerTrophyTitles result = _psn.PlayerTrophyTitlesAsync(_accountId).Result;

            Assert.IsTrue(result.trophyTitles.Count == 100);
        }

        [TestMethod]
        public void TestSearchPlayer()
        {
            SocialMetadata result = _psn.SearchPlayerAsync("Ragowit").Result.FirstResult();

            Assert.AreEqual("1882371903386905898", result.accountId);
            Assert.AreEqual("Ragowit", result.onlineId);
        }

        [TestMethod]
        public void TestTrophies()
        {
            TitleTrophies result = _psn.TitleTrophiesAsync("NPWR09206_00", "default", "trophy").Result; // Gems of War

            Assert.IsTrue(result.trophies.Count == 18);
        }

        [TestMethod]
        public void TestTrophies_PS5WithReward()
        {
            TitleTrophies result = _psn.TitleTrophiesAsync("NPWR22792_00", "default", "trophy2").Result; // Destruction AllStars

            Assert.AreEqual("21", result.trophies[2].trophyProgressTargetValue);
            Assert.AreEqual("Emote", result.trophies[2].trophyRewardName);
        }

        [TestMethod]
        public void TestTrophyGroups()
        {
            TitleTrophyGroups result = _psn.TitleTrophyGroupsAsync("NPWR09206_00", "trophy").Result; // Gems of War

            Assert.IsTrue(result.trophyGroups.Count >= 27);
        }
    }
}
