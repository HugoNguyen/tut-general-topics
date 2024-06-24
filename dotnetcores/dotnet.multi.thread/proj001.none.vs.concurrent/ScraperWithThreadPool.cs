namespace proj001
{
    public class ScraperWithThreadPool : BaseScraper
    {
        public ScraperWithThreadPool(IEnumerable<string> pageURLs) : base(pageURLs)
        {
        }

        public ScraperWithThreadPool(IEnumerable<string> pageURLs, bool showLog) : base(pageURLs, showLog)
        {
        }

        protected override Task Process(HttpClient client)
        {
            // initialize the list of threads
            List<Thread> threads = new List<Thread>();

            CountdownEvent countdownEvent = new CountdownEvent(_pageURLs.Count());

            foreach (var pageURL in _pageURLs)
            {
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    ProcessRequest(client, pageURL);
                    // signal that this task is completed
                    countdownEvent.Signal();
                });
            }

            // wait for all threads to terminate
            countdownEvent.Wait();

            return Task.FromResult(0);
        }
    }
}
