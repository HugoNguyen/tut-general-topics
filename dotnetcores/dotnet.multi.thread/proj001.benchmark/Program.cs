using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace proj001.benchmark
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<Benchmark>();
        }

        [MemoryDiagnoser]
        public class Benchmark
        {
            public string[] _pageUrls = [
                "https://learn.microsoft.com/en-us/copilot/security/microsoft-security-copilot",
                "https://learn.microsoft.com/en-us/azure/security/fundamentals/shared-responsibility-ai",
                "https://learn.microsoft.com/en-us/security/ai-red-team/",
                "https://learn.microsoft.com/en-us/training/paths/get-started-azure-ai/",
                "https://learn.microsoft.com/en-us/shows/learn-live/microsoft-copilot-for-security/",
                "https://learn.microsoft.com/en-us/copilot/security/microsoft-security-copilot",
                "https://learn.microsoft.com/en-us/azure/security/fundamentals/shared-responsibility-ai",
                "https://learn.microsoft.com/en-us/security/ai-red-team/",
                "https://learn.microsoft.com/en-us/training/paths/get-started-azure-ai/",
                "https://learn.microsoft.com/en-us/shows/learn-live/microsoft-copilot-for-security/",
                "https://learn.microsoft.com/en-us/copilot/security/microsoft-security-copilot",
                "https://learn.microsoft.com/en-us/azure/security/fundamentals/shared-responsibility-ai",
                "https://learn.microsoft.com/en-us/security/ai-red-team/",
                "https://learn.microsoft.com/en-us/training/paths/get-started-azure-ai/",
                "https://learn.microsoft.com/en-us/shows/learn-live/microsoft-copilot-for-security/",
                "https://learn.microsoft.com/en-us/copilot/security/microsoft-security-copilot",
                "https://learn.microsoft.com/en-us/azure/security/fundamentals/shared-responsibility-ai",
                "https://learn.microsoft.com/en-us/security/ai-red-team/",
                "https://learn.microsoft.com/en-us/training/paths/get-started-azure-ai/",
                "https://learn.microsoft.com/en-us/shows/learn-live/microsoft-copilot-for-security/",
                "https://learn.microsoft.com/en-us/copilot/security/microsoft-security-copilot",
                "https://learn.microsoft.com/en-us/azure/security/fundamentals/shared-responsibility-ai",
                "https://learn.microsoft.com/en-us/security/ai-red-team/",
                "https://learn.microsoft.com/en-us/training/paths/get-started-azure-ai/",
                "https://learn.microsoft.com/en-us/shows/learn-live/microsoft-copilot-for-security/",
                "https://learn.microsoft.com/en-us/copilot/security/microsoft-security-copilot",
                "https://learn.microsoft.com/en-us/azure/security/fundamentals/shared-responsibility-ai",
                "https://learn.microsoft.com/en-us/security/ai-red-team/",
                "https://learn.microsoft.com/en-us/training/paths/get-started-azure-ai/",
                "https://learn.microsoft.com/en-us/shows/learn-live/microsoft-copilot-for-security/",
                "https://learn.microsoft.com/en-us/copilot/security/microsoft-security-copilot",
                "https://learn.microsoft.com/en-us/azure/security/fundamentals/shared-responsibility-ai",
                "https://learn.microsoft.com/en-us/security/ai-red-team/",
                "https://learn.microsoft.com/en-us/training/paths/get-started-azure-ai/",
                "https://learn.microsoft.com/en-us/shows/learn-live/microsoft-copilot-for-security/",
                "https://learn.microsoft.com/en-us/copilot/security/microsoft-security-copilot",
                "https://learn.microsoft.com/en-us/azure/security/fundamentals/shared-responsibility-ai",
                "https://learn.microsoft.com/en-us/security/ai-red-team/",
                "https://learn.microsoft.com/en-us/training/paths/get-started-azure-ai/",
                "https://learn.microsoft.com/en-us/shows/learn-live/microsoft-copilot-for-security/"
            ];

            public bool showLog = false;

            [Benchmark]
            public void WithoutConcurrent()
            {
                var scraper = new ScraperWithoutConcurrent(_pageUrls, showLog);
                scraper.Run().GetAwaiter();
            }

            [Benchmark]
            public async Task WithThread()
            {
                var scraper = new ScraperWithThread(_pageUrls, showLog);
                await scraper.Run();
            }

            [Benchmark]
            public async Task WithThreadPool()
            {
                var scraper = new ScraperWithThreadPool(_pageUrls, showLog);
                await scraper.Run();
            }

            [Benchmark]
            public async Task WithTask()
            {
                var scraper = new ScraperWithTask(_pageUrls, showLog);
                await scraper.Run();
            }

            [Benchmark]
            public async Task WithTaskV2()
            {
                var scraper = new ScraperWithTaskV2(_pageUrls, showLog);
                await scraper.Run();
            }
        }
    }
}
