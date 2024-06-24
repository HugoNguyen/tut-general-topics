
using System.Diagnostics;

namespace proj001
{
    public abstract class BaseScraper : IScraper
    {
        protected bool _showLog;
        protected readonly IEnumerable<string> _pageURLs;
        protected BaseScraper(IEnumerable<string> pageURLs)
        {
            _pageURLs = pageURLs;
        }

        protected BaseScraper(IEnumerable<string> pageURLs, bool showLog) : this(pageURLs)
        {
            _showLog = showLog;
        }

        public async Task<double> Run()
        {
            // to measure the time required by the script
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // initialize the common HTTP client to make
            // all the requests
            HttpClient client = new HttpClient();

            // perform the requests
            await Process(client);

            // dispose the HTTP client
            client.Dispose();

            // get the elapsed time in seconds
            stopwatch.Stop();
            double elapsedTimeS = stopwatch.ElapsedMilliseconds / 1000.0;
            return elapsedTimeS;
        }

        protected abstract Task Process(HttpClient client);

        protected void ProcessRequest(HttpClient client, string pageURL)
        {
            var response = client.GetAsync(pageURL).Result;
            if (_showLog)
            {
                Console.WriteLine($"Request to '{pageURL}' completed with status code '{response.StatusCode}'!");
            }
        }

    }
}
