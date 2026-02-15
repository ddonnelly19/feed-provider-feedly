using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Windows.Widgets.Feeds.Providers;
using Newtonsoft.Json;

namespace FeedlyFeedProvider
{
    /// <summary>
    /// Implements the IFeedProvider interface for Microsoft Widgets to display feeds from OPML file
    /// </summary>
    [ComVisible(true)]
    [Guid("F3C06D85-4B8A-4E9C-9F1A-2D3E5C6B7A8D")]
    [ClassInterface(ClassInterfaceType.None)]
    public class FeedProvider : IFeedProvider
    {
        private readonly List<FeedInfo> _feeds;

        /// <summary>
        /// Initializes a new instance of the FeedProvider class
        /// </summary>
        public FeedProvider()
        {
            // Get the path to the OPML file (in the same directory as the executable)
            var opmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "feedls.opml");
            
            try
            {
                _feeds = OpmlParser.ParseOpmlFile(opmlPath);
                Console.WriteLine($"Loaded {_feeds.Count} feeds from OPML file");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading OPML file: {ex.Message}");
                _feeds = new List<FeedInfo>();
            }
        }

        /// <summary>
        /// Called when a feed is enabled by the user
        /// </summary>
        public void OnFeedEnabled(FeedProviderEnableOptions options)
        {
            Console.WriteLine($"Feed enabled: {options.FeedId}");
        }

        /// <summary>
        /// Called when a feed is disabled by the user
        /// </summary>
        public void OnFeedDisabled(FeedProviderDisableOptions options)
        {
            Console.WriteLine($"Feed disabled: {options.FeedId}");
        }

        /// <summary>
        /// Retrieves feed data from OPML file to display in the widget
        /// </summary>
        public FeedProviderGetFeedDataResult OnGetFeedData(FeedProviderGetFeedDataOptions options)
        {
            try
            {
                // Create feed data structure from OPML feeds
                var feedContent = new
                {
                    title = "OPML Feeds",
                    description = "Feeds loaded from feedls.opml",
                    items = _feeds.Select((feed, index) => new
                    {
                        id = index.ToString(),
                        title = feed.Title,
                        summary = $"Feed URL: {feed.XmlUrl}",
                        published = DateTime.UtcNow.ToString("o"),
                        author = "OPML Feed",
                        url = !string.IsNullOrEmpty(feed.HtmlUrl) ? feed.HtmlUrl : feed.XmlUrl
                    }).ToArray()
                };

                var feedData = JsonConvert.SerializeObject(feedContent);
                
                // Note: ContentUri can accept either a URI or JSON content
                // For feed providers, JSON content is often returned directly
                return new FeedProviderGetFeedDataResult
                {
                    Status = FeedProviderGetFeedDataStatus.Success,
                    ContentUri = feedData
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting feed data: {ex.Message}");
                return new FeedProviderGetFeedDataResult
                {
                    Status = FeedProviderGetFeedDataStatus.Failed
                };
            }
        }

        /// <summary>
        /// Handles custom query requests from the widget
        /// </summary>
        public FeedProviderCustomQueryResult OnGetCustomQuery(FeedProviderCustomQueryOptions options)
        {
            // Return empty result for custom queries
            return new FeedProviderCustomQueryResult
            {
                Status = FeedProviderCustomQueryStatus.Success,
                QueryResult = string.Empty
            };
        }

        /// <summary>
        /// Called when the user interacts with the feed widget
        /// </summary>
        public void OnInvoked(FeedProviderInvokeOptions options)
        {
            Console.WriteLine($"Feed invoked: {options.CustomCommand}");
        }
    }
}
