using System.Net.Http.Headers;
using System.Net;

namespace Example.Tests
{
    public class FakeHandler : HttpClientHandler
    {
        private readonly HttpStatusCode _statusCode;
        private readonly string _content;
        public Uri RequestedUri { get; private set; }
        public HttpRequestHeaders Headers { get; private set; }

        public FakeHandler(HttpStatusCode statusCode, string content)
        {
            _statusCode = statusCode;
            _content = content;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            RequestedUri = request.RequestUri;
            Headers = request.Headers;
            return await Task.FromResult(SendMessage());
        }

        private HttpResponseMessage SendMessage()
        {
            return new HttpResponseMessage(_statusCode) { Content = new StringContent(_content) };
        }
    }
}
