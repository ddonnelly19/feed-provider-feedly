using System;
using System.Runtime.InteropServices;
using Microsoft.Windows.Widgets.Feeds.Providers;

namespace FeedlyFeedProvider
{
    /// <summary>
    /// Implements the IFeedProvider interface for Microsoft Widgets to display Feedly feeds
    /// </summary>
    [ComVisible(true)]
    [Guid("F3C06D85-4B8A-4E9C-9F1A-2D3E5C6B7A8D")]
    [ClassInterface(ClassInterfaceType.None)]
    public class FeedProvider : IFeedProvider
    {
        private readonly FeedlyClient _feedlyClient;

        /// <summary>
        /// Initializes a new instance of the FeedProvider class
        /// </summary>
        public FeedProvider()
        {
            _feedlyClient = new FeedlyClient();
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
        /// Retrieves feed data from Feedly to display in the widget
        /// </summary>
        public FeedProviderGetFeedDataResult OnGetFeedData(FeedProviderGetFeedDataOptions options)
        {
            try
            {
                // Get feed data from Feedly
                var feedData = _feedlyClient.GetFeedDataAsync().GetAwaiter().GetResult();
                
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
