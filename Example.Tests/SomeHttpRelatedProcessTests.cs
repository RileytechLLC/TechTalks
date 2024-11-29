using System.Net;
using Example.App;
using NUnit.Framework;
using Should;

namespace Example.Tests
{
    [TestFixture]
    public class ExampleTests
    {
        [Test]
        public async Task FakeOutSomeResponseData()
        {
            var fakeHandler = new FakeHandler(HttpStatusCode.OK, jsonResult); //what do you want the "api" to respond with
            var someProcess = new SomeHttpRelatedProcess("someApiKey", fakeHandler);

            var result = await someProcess.GetSomeData();

            fakeHandler.Headers.GetValues("Api_Key").First().ShouldEqual("someApiKey"); //did we send the right api key?
            fakeHandler.RequestedUri.ToString().ShouldEqual("https://something.somewhere.com/"); //did we call the right url?

            result.Count.ShouldEqual(6); //did we get the right number of things back?
            result[3].Label.ShouldEqual(" - M550i xDrive"); //pick an arbitrary label
            result[3].Value.ShouldEqual("BMWM550IXD"); //and value
            //...
        }

        private string jsonResult = @"[
        {
            ""label"": "" - M235i xDrive"",
            ""value"": ""BMWM235IXD""
        },
        {
            ""label"": "" - M240i xDrive"",
            ""value"": ""BMWM240IXD""
        },
        {
            ""label"": "" - 335xi"",
            ""value"": ""335XI""
        },
        {
            ""label"": "" - M550i xDrive"",
            ""value"": ""BMWM550IXD""
        },
        {
            ""label"": "" - ALPINA B7 xDrive"",
            ""value"": ""ALPINAB7XD""
        },
        {
            ""label"": ""X7"",
            ""value"": ""BMWX7""
        }
        ]";
    }
}
