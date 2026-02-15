using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FeedlyFeedProvider
{
    /// <summary>
    /// Client for interacting with the Feedly API
    /// </summary>
    public class FeedlyClient
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private readonly string _feedlyApiUrl = "https://cloud.feedly.com/v3";
        private string? _accessToken;

        /// <summary>
        /// Initializes a new instance of the FeedlyClient
        /// </summary>
        public FeedlyClient()
        {
            _httpClient.DefaultRequestHeaders.Remove("User-Agent");
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "FeedlyFeedProvider/1.0");
        }

        /// <summary>
        /// Gets feed data as JSON. Returns demo data if not authenticated.
        /// </summary>
        public async Task<string> GetFeedDataAsync()
        {
            // For demo purposes, create a sample feed without authentication
            // In production, you would authenticate with Feedly and fetch real feeds
            var feedContent = new
            {
                title = "Feedly Feed",
                description = "Latest articles from Feedly",
                items = new[]
                {
                    new
                    {
                        id = "1",
                        title = "Welcome to Feedly Feed Provider",
                        summary = "This is a sample feed item from your Feedly integration",
                        published = DateTime.UtcNow.ToString("o"),
                        author = "Feedly",
                        url = "https://feedly.com"
                    },
                    new
                    {
                        id = "2",
                        title = "Configure Your Feedly API Token",
                        summary = "To get real feeds, configure your Feedly API token in the config.json file",
                        published = DateTime.UtcNow.AddHours(-1).ToString("o"),
                        author = "Feedly",
                        url = "https://feedly.com"
                    }
                }
            };

            return JsonConvert.SerializeObject(feedContent);
        }

        /// <summary>
        /// Fetches stream contents from Feedly API
        /// </summary>
        /// <param name="streamId">The stream ID to fetch</param>
        /// <param name="count">Number of items to retrieve</param>
        public async Task<string> GetStreamContentsAsync(string streamId, int count = 10)
        {
            if (string.IsNullOrEmpty(_accessToken))
            {
                throw new InvalidOperationException("Not authenticated. Please set access token.");
            }

            var url = $"{_feedlyApiUrl}/streams/contents?streamId={Uri.EscapeDataString(streamId)}&count={count}";
            
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("Authorization", $"OAuth {_accessToken}");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Gets the user's Feedly subscriptions
        /// </summary>
        public async Task<string> GetSubscriptionsAsync()
        {
            if (string.IsNullOrEmpty(_accessToken))
            {
                throw new InvalidOperationException("Not authenticated. Please set access token.");
            }

            var url = $"{_feedlyApiUrl}/subscriptions";
            
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("Authorization", $"OAuth {_accessToken}");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Sets the Feedly API access token for authentication
        /// </summary>
        /// <param name="accessToken">The OAuth access token</param>
        public void SetAccessToken(string accessToken)
        {
            _accessToken = accessToken;
        }
    }
}
