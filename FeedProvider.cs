using System;
using System.Runtime.InteropServices;
using Microsoft.Windows.Widgets.Feeds.Providers;

namespace FeedlyFeedProvider
{
    [ComVisible(true)]
    [Guid("F3C06D85-4B8A-4E9C-9F1A-2D3E5C6B7A8D")]
    [ClassInterface(ClassInterfaceType.None)]
    public class FeedProvider : IFeedProvider
    {
        private readonly FeedlyClient _feedlyClient;

        public FeedProvider()
        {
            _feedlyClient = new FeedlyClient();
        }

        public void OnFeedEnabled(FeedProviderEnableOptions options)
        {
            Console.WriteLine($"Feed enabled: {options.FeedId}");
        }

        public void OnFeedDisabled(FeedProviderDisableOptions options)
        {
            Console.WriteLine($"Feed disabled: {options.FeedId}");
        }

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

        public FeedProviderCustomQueryResult OnGetCustomQuery(FeedProviderCustomQueryOptions options)
        {
            // Return empty result for custom queries
            return new FeedProviderCustomQueryResult
            {
                Status = FeedProviderCustomQueryStatus.Success,
                QueryResult = string.Empty
            };
        }

        public void OnInvoked(FeedProviderInvokeOptions options)
        {
            Console.WriteLine($"Feed invoked: {options.CustomCommand}");
        }
    }
}
