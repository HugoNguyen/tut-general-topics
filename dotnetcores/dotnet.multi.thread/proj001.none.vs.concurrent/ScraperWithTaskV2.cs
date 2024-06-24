namespace proj001
{
    public class ScraperWithTaskV2 : BaseScraper
    {
        public ScraperWithTaskV2(IEnumerable<string> pageURLs) : base(pageURLs)
        {
        }

        public ScraperWithTaskV2(IEnumerable<string> pageURLs, bool showLog) : base(pageURLs, showLog)
        {
        }

        protected override async Task Process(HttpClient client)
        {
            // initialize the list of tasks
            List<Task> tasks = new List<Task>();

            // define each task and add it to the list
            foreach (var pageURL in _pageURLs)
            {
                tasks.Add(Task.Run(() => ProcessRequest(client, pageURL)));
            }

            // wait for tasks to complete...
            await Task.WhenAll(tasks);
        }
    }
}
