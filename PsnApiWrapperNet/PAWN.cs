using System;
using System.Linq;
using System.Text.Json;
using System.Web;
using PsnApiWrapperNet.Model;
using RestSharp;
using RestSharp.Authenticators;

namespace PsnApiWrapperNet
{
    // PSN API Documentation (unofficial): https://github.com/andshrew/PlayStation-Trophies
    // Get your npsso: https://ca.account.sony.com/api/v1/ssocookie

    public class PAWN
    {
        private static readonly RestClient _restClientAuth = new("https://ca.account.sony.com/api");
        private static readonly RestClient _restClientBase = new("https://m.np.playstation.net/api/");
        private static readonly string _version = "0.1.0";

        public PAWN(string npsso)
        {
            _restClientAuth.FollowRedirects = false;
            _restClientAuth.AddDefaultHeader("User-Agent", $"PSN API Wrapper .NET ~ {_version}");
            _restClientAuth.AddDefaultHeader("Cookie", $"npsso={npsso}");
            _restClientAuth.AddDefaultHeader("Authorization", "Basic YWM4ZDE2MWEtZDk2Ni00NzI4LWIwZWEtZmZlYzIyZjY5ZWRjOkRFaXhFcVhYQ2RYZHdqMHY=");

            try
            {
                _restClientBase.Authenticator = new JwtAuthenticator(Authenticate());
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static string Authenticate()
        {
            try
            {
                var requestGet = new RestRequest("authz/v3/oauth/authorize")
                    .AddParameter("client_id", "ac8d161a-d966-4728-b0ea-ffec22f69edc")
                    .AddParameter("redirect_uri", "com.playstation.PlayStationApp://redirect")
                    .AddParameter("response_type", "code")
                    .AddParameter("scope", "psn:mobile.v1 psn:clientapp");
                var responseGet = _restClientAuth.Get(requestGet);
                var location = ((string)responseGet.Headers.First(x => x.Name == "Location").Value).Replace("com.playstation.playstationapp://redirect/", "");
                var code = HttpUtility.ParseQueryString(location).Get("code");

                var requestPost = new RestRequest("authz/v3/oauth/token", DataFormat.Json)
                    .AddParameter("application/x-www-form-urlencoded", $"grant_type=authorization_code&redirect_uri=com.playstation.PlayStationApp://redirect&code={code}", ParameterType.RequestBody);
                var responsePost = _restClientAuth.Post(requestPost);

                return JsonSerializer.Deserialize<Auth>(responsePost.Content).access_token;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public GameList GameList(string accountId, int offset = 0)
        {
            try
            {
                var request = new RestRequest($"gamelist/v2/users/{accountId}/titles")
                    .AddParameter("offset", offset);
                var response = _restClientBase.Get(request);

                return JsonSerializer.Deserialize<GameList>(response.Content);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Player Player(string accountId)
        {
            try
            {
                var request = new RestRequest($"userProfile/v1/internal/users/{accountId}/profiles");
                var response = _restClientBase.Get(request);

                return JsonSerializer.Deserialize<Player>(response.Content);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public PlayerSummary PlayerSummary(string accountId)
        {
            try
            {
                var request = new RestRequest($"trophy/v1/users/{accountId}/trophySummary");
                var response = _restClientBase.Get(request);

                return JsonSerializer.Deserialize<PlayerSummary>(response.Content);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public PlayerTitles PlayerTitles(string accountId, string npTitleIds)
        {
            try
            {
                var request = new RestRequest($"trophy/v1/users/{accountId}/titles/trophyTitles")
                    .AddParameter("npTitleIds", npTitleIds);
                var response = _restClientBase.Get(request);

                return JsonSerializer.Deserialize<PlayerTitles>(response.Content);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public PlayerTrophies PlayerTrophies(string accountId, string npCommunicationId, string groupId, string npServiceName)
        {
            try
            {
                var request = new RestRequest($"trophy/v1/users/{accountId}/npCommunicationIds/{npCommunicationId}/trophyGroups/{groupId}/trophies")
                    .AddParameter("npServiceName", npServiceName);
                var response = _restClientBase.Get(request);

                return JsonSerializer.Deserialize<PlayerTrophies>(response.Content);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public PlayerTrophyGroups PlayerTrophyGroups(string accountId, string npCommunicationId, string npServiceName)
        {
            try
            {
                var request = new RestRequest($"trophy/v1/users/{accountId}/npCommunicationIds/{npCommunicationId}/trophyGroups")
                    .AddParameter("npServiceName", npServiceName);
                var response = _restClientBase.Get(request);

                return JsonSerializer.Deserialize<PlayerTrophyGroups>(response.Content);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public PlayerTrophyTitles PlayerTrophyTitles(string accountId, int offset = 0)
        {
            try
            {
                var request = new RestRequest($"trophy/v1/users/{accountId}/trophyTitles")
                    .AddParameter("offset", offset);
                var response = _restClientBase.Get(request);

                return JsonSerializer.Deserialize<PlayerTrophyTitles>(response.Content);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public UniversalSearch SearchPlayer(string name)
        {
            try
            {
                var request = new RestRequest("search/v1/universalSearch")
                    .AddParameter("searchDomains", "SocialAllAccounts")
                    .AddParameter("countryCode", "us")
                    .AddParameter("languageCode", "en")
                    .AddParameter("age", 69)
                    .AddParameter("pageSize", 50)
                    .AddParameter("searchTerm", name);
                var response = _restClientBase.Get(request);

                return JsonSerializer.Deserialize<UniversalSearch>(response.Content);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Trophies Trophies(string npCommunicationId, string groupId, string npServiceName)
        {
            try
            {
                var request = new RestRequest($"trophy/v1/npCommunicationIds/{npCommunicationId}/trophyGroups/{groupId}/trophies")
                    .AddParameter("npServiceName", npServiceName);
                var response = _restClientBase.Get(request);

                return JsonSerializer.Deserialize<Trophies>(response.Content);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public TrophyGroups TrophyGroups(string npCommunicationId, string npServiceName)
        {
            try
            {
                var request = new RestRequest($"trophy/v1/npCommunicationIds/{npCommunicationId}/trophyGroups")
                    .AddParameter("npServiceName", npServiceName);
                var response = _restClientBase.Get(request);

                return JsonSerializer.Deserialize<TrophyGroups>(response.Content);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
