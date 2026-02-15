using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace FeedlyFeedProvider
{
    /// <summary>
    /// Parser for OPML (Outline Processor Markup Language) files
    /// </summary>
    public class OpmlParser
    {
        /// <summary>
        /// Parses an OPML file and returns a list of feed URLs
        /// </summary>
        /// <param name="opmlFilePath">Path to the OPML file</param>
        /// <returns>List of feed information</returns>
        public static List<FeedInfo> ParseOpmlFile(string opmlFilePath)
        {
            var feeds = new List<FeedInfo>();

            if (!File.Exists(opmlFilePath))
            {
                throw new FileNotFoundException($"OPML file not found: {opmlFilePath}");
            }

            try
            {
                var doc = XDocument.Load(opmlFilePath);
                var outlines = doc.Descendants("outline");

                foreach (var outline in outlines)
                {
                    var xmlUrl = outline.Attribute("xmlUrl")?.Value;
                    
                    // Only process outline elements that have an xmlUrl (actual feeds)
                    if (!string.IsNullOrEmpty(xmlUrl))
                    {
                        var feed = new FeedInfo
                        {
                            Title = outline.Attribute("title")?.Value ?? 
                                   outline.Attribute("text")?.Value ?? 
                                   "Untitled Feed",
                            XmlUrl = xmlUrl,
                            HtmlUrl = outline.Attribute("htmlUrl")?.Value ?? string.Empty,
                            Type = outline.Attribute("type")?.Value ?? "rss"
                        };

                        feeds.Add(feed);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error parsing OPML file: {ex.Message}", ex);
            }

            return feeds;
        }
    }

    /// <summary>
    /// Represents information about a feed from an OPML file
    /// </summary>
    public class FeedInfo
    {
        public string Title { get; set; } = string.Empty;
        public string XmlUrl { get; set; } = string.Empty;
        public string HtmlUrl { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }
}
