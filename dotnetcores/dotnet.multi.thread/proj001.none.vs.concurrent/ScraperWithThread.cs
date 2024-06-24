namespace proj001
{
    public class ScraperWithThread : BaseScraper
    {
        public ScraperWithThread(IEnumerable<string> pageURLs) : base(pageURLs)
        {
        }

        public ScraperWithThread(IEnumerable<string> pageURLs, bool showLog) : base(pageURLs, showLog)
        {
        }

        protected override Task Process(HttpClient client)
        {
            // initialize the list of threads
            List<Thread> threads = new List<Thread>();

            // define each thread and add it to the list
            foreach (var pageURL in _pageURLs)
            {
                Thread thread = new Thread(() =>
                {
                    ProcessRequest(client, pageURL);
                });
                threads.Add(thread);
            }

            // launch each thread
            foreach (var thread in threads)
            {
                thread.Start();
            }

            // wait for all threads to complete
            foreach (var thread in threads)
            {
                thread.Join();
            }

            return Task.FromResult(0);
        }
    }
}
