namespace proj001
{
    public class ScraperWithoutConcurrent : BaseScraper
    {
        public ScraperWithoutConcurrent(IEnumerable<string> pageURLs) : base(pageURLs)
        {
        }

        public ScraperWithoutConcurrent(IEnumerable<string> pageURLs, bool showLog) : base(pageURLs, showLog)
        {
        }

        protected override Task Process(HttpClient client)
        {
            // perform the requests sequentially
            foreach (var pageURL in _pageURLs)
            {
                ProcessRequest(client, pageURL);
            }

            return Task.FromResult(0);
        }
    }
}
