using ManiaPlanetSharp;
using ManiaPlanetSharp.GameBox;
using ManiaPlanetSharp.GameBox.MetadataProviders;
using System.Net.Http;

namespace ParserTest
{
    public class Tests
    {
        HttpClient client = new HttpClient();

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task ParseTMXMaps()
        {
            string[] mapIds = new[] { "144661", "143909", "181804" };

            foreach (var mapId in mapIds)
            {
                using var message = new HttpRequestMessage(HttpMethod.Get, $"https://trackmania.exchange/maps/download/{mapId}");
                message.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.0.0 Safari/537.36");
                message.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");

                var httpResponseMessage = await client.SendAsync(message);
                using var fstream = httpResponseMessage.Content.ReadAsStream();

                var gbxFile = GameBoxFile.Parse(fstream);
                var provider = new MapMetadataProvider(gbxFile);

                Console.WriteLine(provider.Name);
                Console.WriteLine(provider.Environment);

                provider = null;
                gbxFile = null;
            }

            Assert.Pass();
        }
    }
}