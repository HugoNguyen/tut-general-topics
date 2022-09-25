namespace bookstoreapp.blazor.server.ui.Services.Base
{
    public partial interface IClient
    {
        public HttpClient HttpClient { get; }
    }
}
