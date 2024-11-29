using Newtonsoft.Json;

namespace Example.App
{
    public class SomeHttpRelatedProcess(string apiKey, HttpClientHandler handler) // see below for an program.cs example on how to DI this
    {
        public async Task<List<Payload>> GetSomeData() 
        {
            var client = new HttpClient(handler);
            var request = new HttpRequestMessage(HttpMethod.Get, new Uri("https://Something.Somewhere.com"));
            request.Headers.Add("Api_Key", apiKey);

            var response = await client.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();

            var labelValues = JsonConvert.DeserializeObject<List<Payload>>(responseContent);

            return labelValues;
        }
    }

    public class Payload
    {
        public string Label { get; set; }
        public string Value { get; set; }
    }

    //public class ExampleProgramCs
    //{
    //    ... lots of stuff building up your DI container
    //
    //    var apiKey = builder.Configuration["Api_Key"];
    //    builder.Services.AddScoped(service => new SomeHttpRelatedProcess(apiKey, new HttpClientHandler()));
    //}
}
