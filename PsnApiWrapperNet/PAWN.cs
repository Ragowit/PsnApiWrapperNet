using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;
using PsnApiWrapperNet.Model;

namespace PsnApiWrapperNet
{
    // PSN API Documentation (unofficial): https://github.com/andshrew/PlayStation-Trophies
    // Get your npsso: https://ca.account.sony.com/api/v1/ssocookie

    public class PAWN
    {
        private readonly HttpClient _httpClient;
        private readonly string _token;

        public PAWN(string npsso)
        {
            try
            {
                _httpClient = new HttpClient()
                {
                    BaseAddress = new Uri("https://m.np.playstation.com")
                };
                _token = Authenticate(npsso);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static string GetCode(string npsso)
        {
            try
            {
                var httpClient = new HttpClient()
                {
                    BaseAddress = new Uri("https://ca.account.sony.com")
                };

                var requestParams = new List<string>
                {
                    "access_type=offline",
                    "client_id=09515159-7237-4370-9b40-3806e67c0891",
                    "response_type=code",
                    "scope=psn:mobile.v2.core psn:clientapp",
                    "redirect_uri=com.scee.psxandroid.scecompcall://redirect"
                };
                using HttpRequestMessage requestGet = new(
                    HttpMethod.Get,
                    "api/authz/v3/oauth/authorize" + "?" + string.Join("&", requestParams)
                    );
                requestGet.Headers.Add("Cookie", $"npsso={npsso}");
                using HttpResponseMessage responseGet = httpClient.Send(requestGet);
                var query = HttpUtility.ParseQueryString(responseGet.Headers.Location.Query);

                return query["code"];
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<string> GetAccessTokenAsync(string code)
        {
            try
            {
                var httpClient = new HttpClient()
                {
                    BaseAddress = new Uri("https://ca.account.sony.com")
                };

                using HttpRequestMessage requestPost = new(
                    HttpMethod.Post,
                    "api/authz/v3/oauth/token"
                    );
                requestPost.Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "code", code },
                    { "grant_type", "authorization_code" },
                    { "redirect_uri", "com.scee.psxandroid.scecompcall://redirect" },
                    { "token_format", "jwt" }
                });
                requestPost.Headers.Add("Authorization", "Basic MDk1MTUxNTktNzIzNy00MzcwLTliNDAtMzgwNmU2N2MwODkxOnVjUGprYTV0bnRCMktxc1A=");
                using HttpResponseMessage responsePost = httpClient.Send(requestPost);
                Auth auth = (await responsePost.Content.ReadFromJsonAsync<Auth>());

                return auth.access_token;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private string Authenticate(string npsso)
        {
            try
            {
                var code = GetCode(npsso);
                var token = GetAccessTokenAsync(code).Result;

                return token;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GameList> GameListAsync(string accountId, int offset = 0, int limit = 10)
        {
            try
            {
                using HttpRequestMessage request = new(
                    HttpMethod.Get,
                    $"api/gamelist/v2/users/{accountId}/titles"
                    + "?"
                    + $"offset={offset}"
                    + "&"
                    + $"limit={limit}"
                    );
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                using HttpResponseMessage response = _httpClient.Send(request);

                return await response.Content.ReadFromJsonAsync<GameList>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Player> PlayerAsync(string accountId)
        {
            try
            {
                using HttpRequestMessage request = new(
                    HttpMethod.Get,
                    $"api/userProfile/v1/internal/users/{accountId}/profiles"
                    );
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                using HttpResponseMessage response = _httpClient.Send(request);

                return await response.Content.ReadFromJsonAsync<Player>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PlayerSummary> PlayerSummaryAsync(string accountId)
        {
            try
            {
                using HttpRequestMessage request = new(
                    HttpMethod.Get,
                    $"api/trophy/v1/users/{accountId}/trophySummary"
                    );
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                using HttpResponseMessage response = _httpClient.Send(request);

                return await response.Content.ReadFromJsonAsync<PlayerSummary>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PlayerTitles> PlayerTitlesAsync(string accountId, string npTitleIds)
        {
            try
            {
                using HttpRequestMessage request = new(
                    HttpMethod.Get,
                    $"api/trophy/v1/users/{accountId}/titles/trophyTitles"
                    + "?"
                    + $"npTitleIds={npTitleIds}"
                    );
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                using HttpResponseMessage response = _httpClient.Send(request);

                return await response.Content.ReadFromJsonAsync<PlayerTitles>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PlayerTrophies> PlayerTrophiesAsync(string accountId, string npCommunicationId, string groupId, string npServiceName)
        {
            try
            {
                using HttpRequestMessage request = new(
                    HttpMethod.Get,
                    $"api/trophy/v1/users/{accountId}/npCommunicationIds/{npCommunicationId}/trophyGroups/{groupId}/trophies"
                    + "?"
                    + $"npServiceName={npServiceName}"
                    );
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                using HttpResponseMessage response = _httpClient.Send(request);

                return await response.Content.ReadFromJsonAsync<PlayerTrophies>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PlayerTrophyGroups> PlayerTrophyGroupsAsync(string accountId, string npCommunicationId, string npServiceName)
        {
            try
            {
                using HttpRequestMessage request = new(
                    HttpMethod.Get,
                    $"api/trophy/v1/users/{accountId}/npCommunicationIds/{npCommunicationId}/trophyGroups"
                    + "?"
                    + $"npServiceName={npServiceName}"
                    );
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                using HttpResponseMessage response = _httpClient.Send(request);

                return await response.Content.ReadFromJsonAsync<PlayerTrophyGroups>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PlayerTrophyTitles> PlayerTrophyTitlesAsync(string accountId, int offset = 0)
        {
            try
            {
                using HttpRequestMessage request = new(
                    HttpMethod.Get,
                    $"api/trophy/v1/users/{accountId}/trophyTitles"
                    );
                request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "offset", offset.ToString() }
                });
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                using HttpResponseMessage response = _httpClient.Send(request);

                return await response.Content.ReadFromJsonAsync<PlayerTrophyTitles>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UniversalSearch> SearchPlayerAsync(string name)
        {
            try
            {
                using HttpRequestMessage request = new(
                    HttpMethod.Get,
                    $"api/search/v1/universalSearch"
                    + "?"
                    + "searchDomains=SocialAllAccounts"
                    + "&"
                    + "countryCode=us"
                    + "&"
                    + "languageCode=en"
                    + "&"
                    + "age=69"
                    + "&"
                    + "pageSize=50"
                    + "&"
                    + $"searchTerm={name}"
                    );
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                using HttpResponseMessage response = _httpClient.Send(request);

                return await response.Content.ReadFromJsonAsync<UniversalSearch>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TitleTrophies> TitleTrophiesAsync(string npCommunicationId, string groupId, string npServiceName)
        {
            try
            {
                using HttpRequestMessage request = new(
                    HttpMethod.Get,
                    $"api/trophy/v1/npCommunicationIds/{npCommunicationId}/trophyGroups/{groupId}/trophies"
                    + "?"
                    + $"npServiceName={npServiceName}"
                    );
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                using HttpResponseMessage response = _httpClient.Send(request);

                return await response.Content.ReadFromJsonAsync<TitleTrophies>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TitleTrophyGroups> TitleTrophyGroupsAsync(string npCommunicationId, string npServiceName)
        {
            try
            {
                using HttpRequestMessage request = new(
                    HttpMethod.Get,
                    $"api/trophy/v1/npCommunicationIds/{npCommunicationId}/trophyGroups"
                    + "?"
                    + $"npServiceName={npServiceName}"
                    );
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                using HttpResponseMessage response = _httpClient.Send(request);

                return await response.Content.ReadFromJsonAsync<TitleTrophyGroups>();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
