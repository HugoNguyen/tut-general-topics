namespace proj001
{
    internal class Program
    {
        public static List<string> pageURLs = new List<string> {
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
            };

        static async Task Main(string[] args)
        {
            // URLs of the pages to visit
            
            Console.WriteLine($"WithoutConcurrent: {await new ScraperWithoutConcurrent(pageURLs).Run()}s");
            Console.WriteLine($"WithThread: {await new ScraperWithThread(pageURLs).Run()}s");
            Console.WriteLine($"WithThreadPool: {await new ScraperWithThreadPool(pageURLs).Run()}s");
            Console.WriteLine($"WithTask: {await new ScraperWithTask(pageURLs).Run()}s");
            Console.WriteLine($"WithTaskV2: {await new ScraperWithTaskV2(pageURLs).Run()}s");
        }

    }
}
