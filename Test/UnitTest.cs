using System.Linq;
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
        public void TestAuth()
        {
            var result = _psn.GetAuth();

            Assert.IsNotNull(result.access_token);
            Assert.IsTrue(result.expires_in > 0);
            Assert.IsNotNull(result.id_token);
            Assert.IsNotNull(result.refresh_token);
            Assert.IsTrue(result.refresh_token_expires_in > 0);
            Assert.AreEqual("psn:mobile.v2.core psn:clientapp", result.scope);
            Assert.AreEqual("bearer", result.token_type);
        }

        [TestMethod]
        public void TestGameList()
        {
            var result = _psn.GameListAsync(_accountId).Result;
            GameListTitle title = result.titles.First();
            Concept concept = title.concept;
            Media media = title.media;

            Assert.AreEqual(10, result.nextOffset);
            Assert.AreEqual(0, result.previousOffset);
            Assert.AreEqual(10, result.titles.Count);
            Assert.IsTrue(result.totalItemCount > 500);
            // Title
            Assert.IsNotNull(title.category);
            Assert.IsNotNull(title.firstPlayedDateTime);
            Assert.IsNotNull(title.imageUrl);
            Assert.IsNotNull(title.lastPlayedDateTime);
            Assert.IsNotNull(title.localizedImageUrl);
            Assert.IsNotNull(title.localizedName);
            Assert.IsNotNull(title.name);
            Assert.IsNotNull(title.playCount);
            Assert.IsNotNull(title.playDuration);
            Assert.IsNotNull(title.service);
            Assert.IsNotNull(title.titleId);
            // Title, Concept
            Assert.IsNotNull(title.concept);
            Assert.IsNotNull(concept.country);
            Assert.IsNotNull(concept.genres);
            Assert.IsNotNull(concept.id);
            Assert.IsNotNull(concept.language);
            Assert.IsNotNull(concept.name);
            Assert.IsNotNull(concept.titleIds);
            Assert.IsNotNull(concept.localizedName);
            Assert.IsNotNull(concept.localizedName.defaultLanguage);
            Assert.IsNotNull(concept.localizedName.metadata);
            Assert.IsNotNull(concept.media);
            Assert.IsNotNull(concept.media.audios);
            Assert.IsNotNull(concept.media.images);
            Assert.IsNotNull(concept.media.images.First().format);
            Assert.IsNotNull(concept.media.images.First().type);
            Assert.IsNotNull(concept.media.images.First().url);
            Assert.IsNotNull(concept.media.videos);
            // Title, Media
            Assert.IsNotNull(title.media);
            Assert.IsNotNull(media.audios);
            Assert.IsNotNull(media.images);
            Assert.IsNotNull(media.images.First().format);
            Assert.IsNotNull(media.images.First().type);
            Assert.IsNotNull(media.images.First().url);
            Assert.IsNotNull(media.videos);
        }

        [TestMethod]
        public void TestGameListOffset()
        {
            var result = _psn.GameListAsync(_accountId, 10).Result;

            Assert.AreEqual(20, result.nextOffset);
            Assert.AreEqual(9, result.previousOffset);
            Assert.AreEqual(10, result.titles.Count);
            Assert.IsTrue(result.totalItemCount > 500);
        }

        [TestMethod]
        public void TestGameListLimit()
        {
            var result = _psn.GameListAsync(_accountId, limit: 100).Result;

            Assert.AreEqual(100, result.nextOffset);
            Assert.AreEqual(0, result.previousOffset);
            Assert.AreEqual(100, result.titles.Count);
            Assert.IsTrue(result.totalItemCount > 500);
        }

        [TestMethod]
        public void TestGameListNextOffsetNull()
        {
            var result = _psn.GameListAsync("3342351313068632439").Result;

            Assert.IsNull(result.nextOffset);
            Assert.AreEqual(0, result.previousOffset);
            Assert.AreEqual(0, result.titles.Count);
            Assert.AreEqual(0, result.totalItemCount);
        }

        [TestMethod]
        public void TestPlayer()
        {
            Player result = _psn.PlayerAsync(_accountId).Result;
            Avatar avatar = result.avatars.First();

            Assert.IsNotNull(result.aboutMe);
            Assert.AreEqual(4, result.avatars.Count);
            Assert.AreEqual("s", avatar.size);
            Assert.AreEqual("http://static-resource.np.community.playstation.net/avatar_s/3RD/UP20461012H09_4DD355A588184C8F6198_S.png", avatar.url);
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
            Assert.IsTrue( result.earnedTrophies.bronze > 0);
            Assert.IsTrue(result.earnedTrophies.gold > 0);
            Assert.IsTrue(result.earnedTrophies.platinum > 0);
            Assert.IsTrue(result.earnedTrophies.silver > 0);
            Assert.IsTrue(result.progress >= 0);
            Assert.IsTrue(result.tier > 0);
            Assert.IsTrue(result.trophyLevel > 0);
            Assert.IsTrue(result.trophyLevelBasePoint > 0);
            Assert.IsTrue(result.trophyLevelNextPoint > 0);
            Assert.IsTrue(result.trophyPoint > 0);
        }

        [TestMethod]
        public void TestPlayerTitles()
        {
            PlayerTitles result = _psn.PlayerTitlesAsync(_accountId, "CUSA02818_00,CUSA01976_00").Result;
            Title title = result.titles.First();
            TrophyTitle trophyTitle = title.trophyTitles.First();
            Trophy rarestTrophy = trophyTitle.rarestTrophies.First();

            Assert.IsTrue(result.titles.Count == 2);
            // Title
            Assert.AreEqual("CUSA01976_00", title.npTitleId);
            Assert.AreEqual(1, title.trophyTitles.Count);
            // Trophy Title
            Assert.IsNotNull(trophyTitle.definedTrophies);
            Assert.AreEqual(31, trophyTitle.definedTrophies.bronze);
            Assert.AreEqual(7, trophyTitle.definedTrophies.gold);
            Assert.AreEqual(1, trophyTitle.definedTrophies.platinum);
            Assert.AreEqual(8, trophyTitle.definedTrophies.silver);
            Assert.IsNotNull(trophyTitle.earnedTrophies);
            Assert.AreEqual(31, trophyTitle.earnedTrophies.bronze);
            Assert.AreEqual(7, trophyTitle.earnedTrophies.gold);
            Assert.AreEqual(1, trophyTitle.earnedTrophies.platinum);
            Assert.AreEqual(8, trophyTitle.earnedTrophies.silver);
            Assert.IsTrue(trophyTitle.hasTrophyGroups);
            Assert.IsNotNull(trophyTitle.lastUpdatedDateTime);
            Assert.AreEqual("NPWR08388_00", trophyTitle.npCommunicationId);
            Assert.AreEqual("trophy", trophyTitle.npServiceName);
            Assert.AreEqual(100, trophyTitle.progress);
            Assert.AreEqual(1, trophyTitle.rarestTrophies.Count);
            // Rarest Trophy
            Assert.IsTrue(rarestTrophy.earned);
            Assert.IsNotNull(rarestTrophy.earnedDateTime);
            Assert.AreEqual("Achieve all King Knight Feats.", rarestTrophy.trophyDetail);
            Assert.IsNotNull(rarestTrophy.trophyEarnedRate);
            Assert.IsFalse(rarestTrophy.trophyHidden);
            Assert.AreEqual("https://image.api.playstation.com/trophy/np/NPWR08388_00_00401FC091E5E04B06523C97EEA97D8B9A67D88665/7A3FD4B3B133881DDEF3E7FF8363AB0CC7AC6DE7.PNG", rarestTrophy.trophyIconUrl);
            Assert.AreEqual(46, rarestTrophy.trophyId);
            Assert.AreEqual("King Regnant", rarestTrophy.trophyName);
            Assert.AreEqual(0, rarestTrophy.trophyRare);
            Assert.AreEqual("gold", rarestTrophy.trophyType);
            // Trophy Title cont...
            Assert.AreEqual(4, trophyTitle.trophyGroupCount);
            Assert.AreEqual("Shovel Knight: Treasure Trove Trophies", trophyTitle.trophyTitleDetail);
            Assert.AreEqual("https://image.api.playstation.com/trophy/np/NPWR08388_00_00401FC091E5E04B06523C97EEA97D8B9A67D88665/B75CB0AABDEFE660D53C5B70FE470A96EC528D66.PNG", trophyTitle.trophyTitleIconUrl);
            Assert.AreEqual("Shovel Knight: Treasure Trove", trophyTitle.trophyTitleName);
        }

        [TestMethod]
        public void TestPlayerTrophies()
        {
            Trophies result = _psn.PlayerTrophiesAsync(_accountId, "NPWR09206_00", "default", "trophy").Result; // Gems of War
            Trophy rarestTrophy = result.rarestTrophies.First();
            Trophy trophy = result.trophies.First();

            Assert.IsTrue(result.hasTrophyGroups);
            Assert.IsNotNull(result.lastUpdatedDateTime);
            // Rarest Trophy
            Assert.AreEqual(1, result.rarestTrophies.Count);
            Assert.IsTrue(rarestTrophy.earned);
            Assert.IsNotNull(rarestTrophy.earnedDateTime);
            Assert.IsNotNull(rarestTrophy.trophyEarnedRate);
            Assert.IsFalse(rarestTrophy.trophyHidden);
            Assert.AreEqual(3, rarestTrophy.trophyId);
            Assert.AreEqual(1, rarestTrophy.trophyRare);
            Assert.AreEqual("bronze", rarestTrophy.trophyType);
            // Result cont...
            Assert.AreEqual(18, result.totalItemCount);
            // Trophies
            Assert.AreEqual(18, result.trophies.Count);
            Assert.IsTrue(trophy.earned);
            Assert.IsNotNull(trophy.earnedDateTime);
            Assert.IsNotNull(trophy.trophyEarnedRate);
            Assert.IsFalse(trophy.trophyHidden);
            Assert.AreEqual(0, trophy.trophyId);
            Assert.AreEqual(2, trophy.trophyRare);
            Assert.AreEqual("bronze", trophy.trophyType);
            // Result cont...
            Assert.AreEqual("01.43", result.trophySetVersion);
        }

        [TestMethod]
        public void TestPlayerTrophiesWithProgress()
        {
            Trophies result = _psn.PlayerTrophiesAsync(_accountId, "NPWR22810_00", "001", "trophy2").Result; // Diablo IV (PS5)
            Trophy trophy = result.trophies.First();

            // Trophies
            Assert.AreEqual(10, result.trophies.Count);
            Assert.AreEqual("0", trophy.progress); // Only visible if earned != 0
            Assert.IsNull(trophy.progressedDateTime); // Only visible if earned != 0 and progress != 0
            Assert.AreEqual(0, trophy.progressRate); // Only visible if earned != 0
        }

        [TestMethod]
        public void TestPlayerTrophyGroups()
        {
            TrophyGroups result = _psn.PlayerTrophyGroupsAsync(_accountId, "NPWR09206_00", "trophy").Result; // Gems of War
            TrophyGroup trophyGroup = result.trophyGroups.First();

            Assert.IsNotNull(result.earnedTrophies);
            Assert.AreEqual(93, result.earnedTrophies.bronze);
            Assert.AreEqual(4, result.earnedTrophies.gold);
            Assert.AreEqual(0, result.earnedTrophies.platinum);
            Assert.AreEqual(23, result.earnedTrophies.silver);
            Assert.IsFalse(result.hiddenFlag);
            Assert.IsNotNull(result.lastUpdatedDateTime);
            Assert.AreEqual(100, result.progress);
            Assert.AreEqual("01.43", result.trophySetVersion);
            // Trophy Groups
            Assert.IsTrue(result.trophyGroups.Count >= 27);
            Assert.IsNotNull(trophyGroup.earnedTrophies);
            Assert.AreEqual(15, trophyGroup.earnedTrophies.bronze);
            Assert.AreEqual(0, trophyGroup.earnedTrophies.gold);
            Assert.AreEqual(0, trophyGroup.earnedTrophies.platinum);
            Assert.AreEqual(3, trophyGroup.earnedTrophies.silver);
            Assert.IsNotNull(trophyGroup.lastUpdatedDateTime);
            Assert.AreEqual(100, trophyGroup.progress);
            Assert.AreEqual("default", trophyGroup.trophyGroupId);
        }

        [TestMethod]
        public void TestPlayerTrophyTitles()
        {
            TrophyTitles result = _psn.PlayerTrophyTitlesAsync(_accountId).Result;
            TrophyTitle trophyTitle = result.trophyTitles.First();

            Assert.AreEqual(100, result.nextOffset);
            Assert.IsTrue(result.totalItemCount > 500);
            Assert.IsTrue(result.trophyTitles.Count == 100);
            Assert.IsNotNull(trophyTitle.definedTrophies);
            Assert.IsNotNull(trophyTitle.definedTrophies.bronze);
            Assert.IsNotNull(trophyTitle.definedTrophies.gold);
            Assert.IsNotNull(trophyTitle.definedTrophies.platinum);
            Assert.IsNotNull(trophyTitle.definedTrophies.silver);
            Assert.IsNotNull(trophyTitle.earnedTrophies);
            Assert.IsNotNull(trophyTitle.earnedTrophies.bronze);
            Assert.IsNotNull(trophyTitle.earnedTrophies.gold);
            Assert.IsNotNull(trophyTitle.earnedTrophies.platinum);
            Assert.IsNotNull(trophyTitle.earnedTrophies.silver);
            Assert.IsNotNull(trophyTitle.hasTrophyGroups);
            Assert.IsNotNull(trophyTitle.hiddenFlag);
            Assert.IsNotNull(trophyTitle.lastUpdatedDateTime);
            Assert.IsNotNull(trophyTitle.npCommunicationId);
            Assert.IsNotNull(trophyTitle.npServiceName);
            Assert.IsNotNull(trophyTitle.progress);
            Assert.IsNotNull(trophyTitle.trophyGroupCount);
            Assert.IsNotNull(trophyTitle.trophySetVersion);
            Assert.IsNotNull(trophyTitle.trophyTitleDetail);
            Assert.IsNotNull(trophyTitle.trophyTitleIconUrl);
            Assert.IsNotNull(trophyTitle.trophyTitleName);
            Assert.IsNotNull(trophyTitle.trophyTitlePlatform);
        }

        [TestMethod]
        public void TestPlayerTrophyTitlesOffset()
        {
            TrophyTitles result = _psn.PlayerTrophyTitlesAsync(_accountId, 100).Result;

            Assert.IsTrue(result.nextOffset == 200);
        }

        [TestMethod]
        public void TestPlayerTrophyTitlesLimit()
        {
            TrophyTitles result = _psn.PlayerTrophyTitlesAsync(_accountId, limit: 10).Result;

            Assert.IsTrue(result.nextOffset == 10);
        }

        [TestMethod]
        public void TestSearchPlayer()
        {
            UniversalSearch search = _psn.SearchPlayerAsync("Ragowit").Result;
            DomainResponse domainRespone = search.domainResponses.First();
            SearchResult searchResult = domainRespone.results.First();
            SocialMetadata socialMetadata = search.FirstResult();

            // Search
            Assert.IsFalse(search.fallbackQueried);
            Assert.AreEqual("Ragowit", search.prefix);
            Assert.AreEqual(1000, search.queryFrequency.filterDebounceMs);
            Assert.AreEqual(500, search.queryFrequency.searchDebounceMs);
            Assert.AreEqual(1, search.responseStatus.Count);
            Assert.AreEqual("200", search.responseStatus[0].status);
            Assert.AreEqual("OK", search.responseStatus[0].statusMessage);
            Assert.IsTrue(search.strandPaginationResponse.lastPage);
            Assert.AreEqual(0, search.strandPaginationResponse.offset);
            Assert.AreEqual(1, search.strandPaginationResponse.pageSize);
            // Domain Respone
            Assert.AreEqual("SocialAllAccounts", domainRespone.domain);
            Assert.AreEqual("Players with names like \"Ragowit\"", domainRespone.domainExpandedTitle);
            Assert.AreEqual("Players", domainRespone.domainTitle);
            Assert.AreEqual(1, domainRespone.domainTitleHighlight.Count);
            Assert.AreEqual("Players", domainRespone.domainTitleHighlight[0]);
            Assert.AreEqual("msgid_players", domainRespone.domainTitleMessageId);
            Assert.AreEqual("", domainRespone.next);
            Assert.IsTrue(domainRespone.totalResultCount > 0);
            Assert.IsFalse(domainRespone.zeroState);
            // Search Result
            Assert.IsNotNull(searchResult.id);
            Assert.IsNotNull(searchResult.relevancyScore);
            Assert.IsTrue(searchResult.score > 0);
            Assert.AreEqual("social", searchResult.type);
            Assert.IsNotNull(searchResult.socialMetadata);
            // Social Metadata
            Assert.AreEqual("1882371903386905898", socialMetadata.accountId);
            Assert.AreEqual("CUSTOMER", socialMetadata.accountType);
            Assert.IsNotNull(socialMetadata.avatarUrl);
            Assert.AreEqual("SE", socialMetadata.country);
            Assert.IsNotNull(socialMetadata.highlights);
            Assert.AreEqual(2, socialMetadata.highlights.onlineId.Count);
            Assert.AreEqual("Ragowit", socialMetadata.highlights.onlineId[1]);
            Assert.IsFalse(socialMetadata.isOfficiallyVerified);
            Assert.IsTrue(socialMetadata.isPsPlus);
            Assert.AreEqual("en", socialMetadata.language);
            Assert.AreEqual("Ragowit", socialMetadata.onlineId);
            Assert.AreEqual("", socialMetadata.verifiedUserName);
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
            Trophies result = _psn.TitleTrophiesAsync("NPWR09206_00", "default", "trophy").Result; // Gems of War
            Trophy trophy = result.trophies.First();

            Assert.IsTrue(result.hasTrophyGroups);
            Assert.AreEqual(18, result.totalItemCount);
            Assert.AreEqual("01.43", result.trophySetVersion);
            Assert.AreEqual(18, result.trophies.Count);
            Assert.AreEqual("Reach level 10", trophy.trophyDetail);
            Assert.AreEqual("default", trophy.trophyGroupId);
            Assert.IsFalse(trophy.trophyHidden);
            Assert.AreEqual("https://image.api.playstation.com/trophy/np/NPWR09206_00_002D0CF328AD1FE829D381416C244CC970B185FE18/42FED7ADD7AAD7926B49C8744077264A7544753F.PNG", trophy.trophyIconUrl);
            Assert.AreEqual(0, trophy.trophyId);
            Assert.AreEqual("Double Digits", trophy.trophyName);
            Assert.AreEqual("bronze", trophy.trophyType);
        }

        [TestMethod]
        public void TestTrophies_PS5WithReward()
        {
            Trophies result = _psn.TitleTrophiesAsync("NPWR22792_00", "default", "trophy2").Result; // Destruction AllStars
            Trophy trophy = result.trophies[2];

            Assert.IsFalse(result.hasTrophyGroups);
            Assert.AreEqual(29, result.totalItemCount);
            Assert.AreEqual("01.00", result.trophySetVersion);
            Assert.AreEqual(29, result.trophies.Count);
            Assert.AreEqual("Earn all star objectives in Ultimo Barricados' Series, Mutual Respect", trophy.trophyDetail);
            Assert.AreEqual("default", trophy.trophyGroupId);
            Assert.IsFalse(trophy.trophyHidden);
            Assert.AreEqual("https://psnobj.prod.dl.playstation.net/psnobj/NPWR22792_00/ad453705-a024-4035-9a7c-b99895279cde.png", trophy.trophyIconUrl);
            Assert.AreEqual(2, trophy.trophyId);
            Assert.AreEqual("Ultimate Respect", trophy.trophyName);
            Assert.AreEqual("21", trophy.trophyProgressTargetValue);
            Assert.AreEqual("https://psnobj.prod.dl.playstation.net/psnobj/NPWR22792_00/074f6644-baaa-43d7-a767-0b948c121021.png", trophy.trophyRewardImageUrl);
            Assert.AreEqual("Emote", trophy.trophyRewardName);
            Assert.AreEqual("gold", trophy.trophyType);
        }

        [TestMethod]
        public void TestTitleTrophyGroups()
        {
            TrophyGroups result = _psn.TitleTrophyGroupsAsync("NPWR09206_00", "trophy").Result; // Gems of War
            TrophyGroup trophyGroup = result.trophyGroups.First();

            Assert.IsNotNull(result.definedTrophies);
            Assert.AreEqual(93, result.definedTrophies.bronze);
            Assert.AreEqual(4, result.definedTrophies.gold);
            Assert.AreEqual(0, result.definedTrophies.platinum);
            Assert.AreEqual(23, result.definedTrophies.silver);
            Assert.AreEqual("NPWR09206_00", result.npCommunicationId);
            Assert.AreEqual("trophy", result.npServiceName);
            Assert.AreEqual("01.43", result.trophySetVersion);
            Assert.AreEqual("This is the trophy pack for Gems of War", result.trophyTitleDetail);
            Assert.AreEqual("https://image.api.playstation.com/trophy/np/NPWR09206_00_002D0CF328AD1FE829D381416C244CC970B185FE18/B6C850B2ECA4133D191505D6F6856AAA9F27BEEC.PNG", result.trophyTitleIconUrl);
            Assert.AreEqual("Gems of War", result.trophyTitleName);
            Assert.AreEqual("PS4", result.trophyTitlePlatform);
            Assert.IsTrue(result.trophyGroups.Count >= 27);
            Assert.IsNotNull(trophyGroup.definedTrophies);
            Assert.AreEqual(15, trophyGroup.definedTrophies.bronze);
            Assert.AreEqual(0, trophyGroup.definedTrophies.gold);
            Assert.AreEqual(0, trophyGroup.definedTrophies.platinum);
            Assert.AreEqual(3, trophyGroup.definedTrophies.silver);
            Assert.AreEqual("This is the trophy pack for Gems of War", trophyGroup.trophyGroupDetail);
            Assert.AreEqual("https://image.api.playstation.com/trophy/np/NPWR09206_00_002D0CF328AD1FE829D381416C244CC970B185FE18/B6C850B2ECA4133D191505D6F6856AAA9F27BEEC.PNG", trophyGroup.trophyGroupIconUrl);
            Assert.AreEqual("default", trophyGroup.trophyGroupId);
            Assert.AreEqual("Gems of War", trophyGroup.trophyGroupName);
        }
    }
}
