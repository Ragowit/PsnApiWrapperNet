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
        public void TestGameListOffset()
        {
            var result = _psn.GameListAsync(_accountId, 10).Result;

            Assert.IsFalse(result.previousOffset == 0);
        }

        [TestMethod]
        public void TestGameListLimit()
        {
            var result = _psn.GameListAsync(_accountId, limit: 100).Result;

            Assert.IsTrue(result.titles.Count == 100);
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

            Assert.IsNotNull(result.aboutMe);
            Assert.AreEqual(4, result.avatars.Count);
            Assert.IsNull(result.error);
            Assert.IsFalse(result.isMe);
            Assert.IsFalse(result.isOfficiallyVerified);
            Assert.IsTrue(result.isPlus);
            Assert.AreEqual(2, result.languages.Count);
            Assert.AreEqual("Ragowit", result.onlineId);
        }

        [TestMethod]
        public void TestPlayerWithError()
        {
            Player result = _psn.PlayerAsync("5524975455069480019").Result;

            Assert.IsNull(result.aboutMe);
            Assert.IsNull(result.avatars);
            Assert.AreEqual(2281485, result.error.code);
            Assert.AreEqual("Resource not found (account='5524975455069480019')", result.error.message);
            Assert.IsNotNull(result.error.referenceId);
            Assert.IsFalse(result.isMe);
            Assert.IsFalse(result.isOfficiallyVerified);
            Assert.IsFalse(result.isPlus);
            Assert.IsNull(result.languages);
            Assert.IsNull(result.onlineId);
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
        public void TestPlayerTrophyTitlesOffset()
        {
            PlayerTrophyTitles result = _psn.PlayerTrophyTitlesAsync(_accountId, 100).Result;

            Assert.IsTrue(result.nextOffset == 200);
        }

        [TestMethod]
        public void TestPlayerTrophyTitlesLimit()
        {
            PlayerTrophyTitles result = _psn.PlayerTrophyTitlesAsync(_accountId, limit: 10).Result;

            Assert.IsTrue(result.nextOffset == 10);
        }

        [TestMethod]
        public void TestSearchPlayer()
        {
            SocialMetadata result = _psn.SearchPlayerAsync("Ragowit").Result.FirstResult();

            Assert.AreEqual("1882371903386905898", result.accountId);
            Assert.IsNotNull(result.avatarUrl);
            Assert.AreEqual("SE", result.country);
            Assert.IsNotNull(result.highlights);
            Assert.IsFalse(result.isOfficiallyVerified);
            Assert.IsTrue(result.isPsPlus);
            Assert.AreEqual("en", result.language);
            Assert.AreEqual("Ragowit", result.onlineId);
        }

        [TestMethod]
        public void TestSearchPlayerWithNullCountry()
        {
            SocialMetadata result = _psn.SearchPlayerAsync("Bugs-DK38").Result.FirstResult();

            Assert.AreEqual("7410023915507426860", result.accountId);
            Assert.IsNull(result.avatarUrl);
            Assert.IsNull(result.country);
            Assert.IsNull(result.language);
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
