namespace proj001
{
    public class ScraperWithTask : BaseScraper
    {
        public ScraperWithTask(IEnumerable<string> pageURLs) : base(pageURLs)
        {
        }

        public ScraperWithTask(IEnumerable<string> pageURLs, bool showLog) : base(pageURLs, showLog)
        {
        }

        protected override Task Process(HttpClient client)
        {
            // initialize the list of tasks
            List<Task> tasks = new List<Task>();

            // define each task and add it to the list
            foreach (var pageURL in _pageURLs)
            {
                tasks.Add(Task.Run(() => ProcessRequest(client, pageURL)));
            }

            // wait for tasks to complete...
            foreach (var task in tasks)
            {
                task.Wait();
            }

            return Task.FromResult(0);
        }
    }
}
