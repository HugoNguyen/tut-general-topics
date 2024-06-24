namespace proj020
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // AsynchronousStreamWithIEnumarable.Run();
            // AsynchronousStreamWithAsynEnumarable.Run().Wait();
            // CancelAsynchronousStreamWithBreak.Run().Wait();
            // AsynChronousSteamWithCancelToken.Run().Wait();
            CancelAsynChronousSteamThroughEnumeratorCancellation.Run().Wait();

            Console.ReadKey();
        }
    }
}
